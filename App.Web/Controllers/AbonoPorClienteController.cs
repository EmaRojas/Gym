using App.Core.Domain;
using App.Services.Generals;
using App.Web.Models;
using App.Web.Security;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
    [AutorizarAcceso]

    public class AbonoPorClienteController : BaseController
    {
        private readonly IGeneralService<AbonoPorCliente> _serviceAbonoPorCliente;
        private readonly IGeneralService<Cliente> _clienteService;
        private readonly IGeneralService<Abono> _abonoService;
        // GET: AbonoPorCliente
        public AbonoPorClienteController(IGeneralService<AbonoPorCliente> serviceAbonoPorCliente, IGeneralService<Cliente> clienteService, IGeneralService<Abono> abonoService)
        {
            _serviceAbonoPorCliente = serviceAbonoPorCliente;
            _clienteService = clienteService;
            _abonoService = abonoService;
        }

        public ActionResult List(int id = 0)
        {
            var model = new AbonoPorClienteModel();
            model.ClienteId = id;
            return View(model);
        }

        public JsonResult Table(int offset, int limit, string search, int clienteId = 0, string rPagado = "", string rVencimiento = "")
        {
            var abonoPorClientes = new List<AbonoPorCliente>();
            var model = new List<AbonoPorClienteListModel>();
            if (clienteId != 0)
            {
                abonoPorClientes = _serviceAbonoPorCliente.GetByCriteria(x => x.Cliente.Id == clienteId);
            }
            else
            {
                abonoPorClientes = _serviceAbonoPorCliente.GetAllByUser();
            }

            var listaOrdenada = abonoPorClientes.OrderByDescending(x => x.FechaVencimiento);

            foreach (var item in listaOrdenada.Skip(offset).Take(limit))
            {
                model.Add(new AbonoPorClienteListModel()
                {
                    Id = item.Id,
                    Dni = item.Cliente.Dni,
                    Cliente = item.Cliente != null ? item.Cliente.Nombre + " " + item.Cliente.Apellido : "",
                    Abono = item.Abono != null ? item.Abono.Nombre : "",
                    FechaIngreso = item.FechaIngreso?.ToString("dd/MM/yyyy") ?? "",
                    FechaVencimiento = item.FechaVencimiento?.ToString("dd/MM/yyyy") ?? "",
                    Precio = item.Precio.ToString(),
                    Pagado = item.Pagado == true ? "<span class=\"m-r-10 tag tag-pill tag-success tag-sm\">Sí</span>" : "<span class=\"m-r-10 tag tag-pill tag-danger tag-sm\">No</span>",
                    Ficho = item.Ficho,
                    Estado = item.FechaVencimiento < DateTime.UtcNow.AddHours(-3) ? "<span class=\"m-r-10 tag tag-pill tag-danger tag-sm\">Vencido</span>" : "<span class=\"m-r-10 tag tag-pill tag-success tag-sm\">Disponible</span>",
                    Vencido = item.FechaVencimiento < DateTime.UtcNow.AddHours(-3) ? true : false,
                    PendientePago = item?.PendientePego
                });
            }

            return Json(new { rows = model.OrderByDescending(x => DateTime.ParseExact(x.FechaVencimiento, "dd/MM/yyyy", null)).ToList(), total = abonoPorClientes.Count() }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Create(int id = 0)
        {
            var abonoPorCliente = new AbonoPorClienteModel();

            if (id != 0)
            {
                var cliente = _clienteService.GetByIdByUser(id);
                abonoPorCliente.ListaCliente.Add(new SelectListItem() { Text = cliente.Nombre + " " + cliente.Apellido, Selected = true, Value = cliente.Id.ToString() });              

            }
            else
            {
                abonoPorCliente.ListaCliente.Add(new SelectListItem() { Text = "Seleccione...", Value = "default" });

                foreach (var cliente in _clienteService.GetAllByUser().OrderBy(x => x.Nombre))
                {
                    abonoPorCliente.ListaCliente.Add(new SelectListItem() { Text = cliente.Nombre + " " + cliente.Apellido, Value = cliente.Id.ToString() });
                }

            }

            abonoPorCliente.ListaAbono.Add(new SelectListItem() { Text = "Seleccione...", Value = "default" });

            foreach (var abono in _abonoService.GetAllByUser().OrderBy(x => x.Nombre))
            {
                abonoPorCliente.ListaAbono.Add(new SelectListItem() { Text = abono.Nombre, Value = abono.Id.ToString() });
            }
            DateTime fecha = DateTime.UtcNow.AddHours(-3).Date;
            fecha = fecha.Date.AddMonths(1);            
            abonoPorCliente.FechaVencimiento = fecha.ToString("dd/MM/yyyy");

            return View(abonoPorCliente);
        }

        [HttpPost]
        public ActionResult Create(AbonoPorClienteModel model)
        {

            try
            {
                var consulta = _serviceAbonoPorCliente.GetByCriteriaByUser(x => x.FechaVencimiento > DateTime.UtcNow.AddHours(-3) && x.Cliente.Id == model.ClienteId);

                if (consulta.Count >= 2)
                {
                    return Json(new { Success = false, Url = "/AbonoPorCliente/List", Message = "El cliente ya tiene un abono pendiente de impactar"}, JsonRequestBehavior.AllowGet);
                }
                else if (consulta.Where(x => x.FechaIngreso.Value.Month == x.FechaVencimiento.Value.Month).Any())
                {
                    return Json(new { Success = false, Url = "/AbonoPorCliente/List", Message = "El cliente ya tiene un abono para ese mes" }, JsonRequestBehavior.AllowGet);
                }

                var serviceAbonoPorAlumno = _serviceAbonoPorCliente.GetByCriteria(x => x.Cliente.Id == model.ClienteId).LastOrDefault();
                var cliente = _clienteService.GetByIdByUser(model.ClienteId);
                var abono = _abonoService.GetByIdByUser(model.AbonoId);


                var abonoPorAlumno = new AbonoPorCliente()
                {
                    PendientePego = model.PendientePago,
                    Cliente = cliente ?? null,
                    Abono = abono ?? null,
                    FechaVencimiento = DateTime.ParseExact(model.FechaVencimiento, "dd/MM/yyyy",null),
                    FechaIngreso = DateTime.UtcNow.AddHours(-3),
                    Precio = double.Parse(model.Precio.Replace(".", ",")),
                    Pagado = model.Pagado
                };
                _serviceAbonoPorCliente.Create(abonoPorAlumno);

                SuccessNotification("Guardado Correctamente");

                return Json(new { Success = true, Url = "/AbonoPorCliente/List", Message = "Se asigno correctamente el abono al cliente " + cliente.Nombre +" " + cliente.Apellido }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                return Json(new { Message = "Error al asignar abono al cliente"}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var abonoPorCliente = _serviceAbonoPorCliente.GetByIdByUser(id);

            var model = new AbonoPorClienteModel()
            {
                Id = abonoPorCliente.Id,
                ClienteId = abonoPorCliente.Cliente.Id,
                AbonoId = abonoPorCliente.Abono != null ? abonoPorCliente.Abono.Id : 0,
                FechaVencimiento = abonoPorCliente.FechaVencimiento.Value.ToString("dd/MM/yyyy"),
                FechaIngreso = abonoPorCliente.FechaIngreso.Value.ToString("dd/MM/yyyy"),
                Precio = abonoPorCliente.Precio.ToString().Replace(",", "."),
                Pagado = abonoPorCliente.Pagado,
                PendientePago = abonoPorCliente?.PendientePego,
            };

            model.ListaCliente.Add(new SelectListItem() { Text = abonoPorCliente.Cliente.Nombre + " " + abonoPorCliente.Cliente.Apellido, Value = abonoPorCliente.Cliente.Id.ToString() });

            foreach (var abono in _abonoService.GetAllByUser().OrderBy(x => x.Nombre))
            {
                model.ListaAbono.Add(new SelectListItem() { Text = abono.Nombre, Value = abono.Id.ToString() });
            }

            return View(model);
        }

        public ActionResult Edit(AbonoPorClienteModel model)
        {
            var abonoPorCliente = _serviceAbonoPorCliente.GetByIdByUser(model.Id);
            if (abonoPorCliente.Cliente?.Id != model.ClienteId)
            {
                ErrorNotification("No se puede modificar el cliente.");
            }
            if (abonoPorCliente.Abono?.Id != model.AbonoId)
            {
                ErrorNotification("No se puede modificar el abono.");
            }

            var abono = _abonoService.GetByIdByUser(model.AbonoId);

            abonoPorCliente.Abono = abono ?? null;
            abonoPorCliente.FechaVencimiento = DateTime.ParseExact(model.FechaVencimiento, "dd/MM/yyyy",null);
            abonoPorCliente.Precio = double.Parse(model.Precio.Replace(".", ","));
            abonoPorCliente.Pagado = model.Pagado;
            abonoPorCliente.PendientePego = model.PendientePago;

            return Json(new { Success = true, Url = "/AbonoPorCliente/List", Message = "Se guardo correctamente " + abonoPorCliente.Cliente.Nombre + " " + abonoPorCliente.Cliente.Apellido }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public string InputPrecio(int id)
        {
            var abono = _abonoService.GetByIdByUser(id);

            return abono.Precio.ToString();
        }

        [HttpGet]
        public ActionResult Renovar(int id)
        {
            ViewBag.Controller = "Renovar Abono";

            var abonoPorCliente = _serviceAbonoPorCliente.GetByIdByUser(id);
            var fechaVencimiento = DateTime.UtcNow.AddHours(-3);
            var fecaIngreso = DateTime.UtcNow.AddHours(-3);

            if (DateTime.UtcNow.Date >= abonoPorCliente.FechaVencimiento.Value.Date)
            {
                fechaVencimiento = DateTime.UtcNow.AddHours(-3).AddMonths(1);                
            }
            else
            {
                ErrorNotification("El cliente ya tiene un abono activo");
                return RedirectToAction("List");
            }

            var model = new AbonoPorClienteModel()
            {
                ClienteId = abonoPorCliente.Cliente.Id,
                AbonoId = abonoPorCliente.Abono.Id,
                FechaVencimiento = fechaVencimiento.ToString("dd/MM/yyyy"),
                //FechaIngreso = fecaIngreso.tosho,
                //Precio = abonoPorAlumno.Precio.ToString().Replace(",", "."),
                Precio = abonoPorCliente.Abono.Precio.ToString().Replace(",", "."),
                Pagado = false,
            };

            model.ListaCliente.Add(new SelectListItem() { Text = abonoPorCliente.Cliente.Nombre + " " + abonoPorCliente.Cliente.Apellido, Value = abonoPorCliente.Cliente.Id.ToString() });

            foreach (var abono in _abonoService.GetAllByUser().OrderBy(x => x.Nombre))
            {
                model.ListaAbono.Add(new SelectListItem() { Text = abono.Nombre, Value = abono.Id.ToString() });
            }

            return View(model);
        }

        //public ActionResult Delete(int id)
        //{
        //    var abonoPorCliente = _serviceAbonoPorCliente.GetByIdByUser(id);

        //    if (abonoPorCliente != null)
        //    {


        //        if (abonoPorAlumno.Pago != null) _pagoService.Delete(abonoPorAlumno.Pago);
        //        _abonoPorAlumnoService.Delete(abonoPorAlumno);

        //        SuccessNotification("Eliminado Correctamente");
        //    }

        //    return RedirectToAction("List", new { id = alumnoId });
        //}
    }
}