using App.Core.Domain;
using App.Services.Generals;
using App.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
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

        public ActionResult List()
        {
            return View();
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
                abonoPorCliente.ListaCliente.Add(new SelectListItem() { Text = "Seleccione...", Disabled=true, Selected=true, Value = "" });

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
            DateTime fecha = DateTime.Today.Date;
            fecha = fecha.Date.AddMonths(1);            
            abonoPorCliente.FechaVencimiento = fecha.ToShortDateString();

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
                    Cliente = cliente ?? null,
                    Abono = abono ?? null,
                    FechaVencimiento = DateTime.Parse(model.FechaVencimiento),
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
                AbonoId = abonoPorCliente.Abono.Id,
                FechaVencimiento = abonoPorCliente.FechaVencimiento.Value.ToShortDateString(),
                FechaIngreso = abonoPorCliente.FechaIngreso.Value.ToShortDateString(),
                Precio = abonoPorCliente.Precio.ToString().Replace(",", "."),
                Pagado = abonoPorCliente.Pagado,
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
            abonoPorCliente.FechaVencimiento = DateTime.Parse(model.FechaVencimiento);
            abonoPorCliente.Precio = double.Parse(model.Precio.Replace(".", ","));
            abonoPorCliente.Pagado = model.Pagado;

            return Json(new { Success = true, Url = "/AbonoPorCliente/List", Message = "Se guardo correctamente " + abonoPorCliente.Cliente.Nombre + " " + abonoPorCliente.Cliente.Apellido }, JsonRequestBehavior.AllowGet);

        }



        [HttpGet]
        public string InputPrecio(int id)
        {
            var abono = _abonoService.GetByIdByUser(id);

            return abono.Precio.ToString();
        }

        public ActionResult ListaAbonosAsignados()
        {
            var asignaciones = _serviceAbonoPorCliente.GetAllByUser();
            var lista = new List<AbonoPorClienteListModel>();

            foreach (var item in asignaciones)
            {
                lista.Add(new AbonoPorClienteListModel()
                {
                    Id = item.Id,
                    Cliente = item.Cliente.Nombre + " " + item.Cliente.Apellido,
                    Dni = item.Cliente.Dni,
                    Abono = item.Abono.Nombre,
                    FechaIngreso = item.FechaIngreso.Value.ToShortDateString(),
                    FechaVencimiento = item.FechaVencimiento.Value.ToShortDateString(),
                    Precio = item.Precio.ToString(),
                    Pagado = item.Pagado == true ? "<span class=\"center chip green white-text\">Sí</span>" : "<span class=\"center chip red white-text\">No</span>",
                    Estado = item.FechaVencimiento < DateTime.UtcNow.AddHours(-3) ? "<span class=\"center chip red white-text\">Vencido</span>" : "<span class=\"center chip green white-text\">Disponible</span>",
                    Vencido = item.FechaVencimiento < DateTime.UtcNow.AddHours(-3) ? true : false,
                    Opciones = "<a style=\"margin - right: 5px;\" class=\"btn-floating waves-effect waves-light yellow\" title=\"Renovar\" onclick=\"Renovar('"+item.Id+"')\" id=\"'" + item.Id + "'\"><i class=\"mdi-action-assignment\"></i></a>"
                    + "<a style=\"margin - right: 5px;\" class=\"btn-floating waves-effect waves-light teal\" title=\"Editar\" onclick=\"Editar('" + item.Id + "')\" id=\"'" + item.Id + "'\"><i class=\"mdi-editor-border-color\"></i></a>"
                }) ;
            }

            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.Url.Query);
            string sEcho = nvc["sEcho"].ToString();
            string sSearch = nvc["sSearch"].ToString();
            int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
            int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);

            //iSortCol gives your Column numebr of for which sorting is required
            int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
            //provides your sort order (asc/desc)
            string sortOrder = nvc["sSortDir_0"].ToString();

            var Count = lista.Count();

            var newList = lista.OrderByDescending(x => x.FechaVencimiento).Skip(iDisplayStart).Take(10);

            //Search query when sSearch is not empty
            if (sSearch != "" && sSearch != null) //If there is search query
            {

                newList = lista.Where(a => a.Cliente.ToLower().Contains(sSearch.ToLower())
                                  || a.Abono.ToLower().Contains(sSearch.ToLower())
                                  || a.FechaVencimiento.ToLower().Contains(sSearch.ToLower())
                                  || a.Precio.ToLower().Contains(sSearch.ToLower())
                                  || a.Pagado.ToLower().Contains(sSearch.ToLower())
                                  || a.Estado.ToLower().Contains(sSearch.ToLower())
                                  || a.FechaIngreso.ToLower().Contains(sSearch.ToLower())
                                  )
                                  .ToList();

                Count = newList.Count();
                // Call SortFunction to provide sorted Data, then Skip using iDisplayStart  
                newList = SortFunction(iSortCol, sortOrder, newList.ToList()).Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }
            else
            {
                //get data from database
                newList = lista.ToList(); //speficiy conditions if there is any using .Where(Condition)                             


                // Call SortFunction to provide sorted Data, then Skip using iDisplayStart  
                newList = SortFunction(iSortCol, sortOrder, newList.ToList()).Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }


            var abonoPorClientePage = new JqueryDatatableParam<AbonoPorClienteListModel>(newList, Count, Count, sEcho);

            //lista = lista.OrderByDescending(x => x.FechaVencimiento).ToList();

            return Json(abonoPorClientePage, JsonRequestBehavior.AllowGet);
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
                FechaVencimiento = fechaVencimiento.ToShortDateString(),
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

        private List<AbonoPorClienteListModel> SortFunction(int iSortCol, string sortOrder, List<AbonoPorClienteListModel> list)
        {

            //Sorting for String columns
            if (iSortCol == 1 || iSortCol == 0 || iSortCol == 2 || iSortCol == 3|| iSortCol == 4 || iSortCol == 5 || iSortCol == 6)
            {
                Func<AbonoPorClienteListModel, string> orderingFunction = (c => iSortCol == 0 ? c.Cliente : iSortCol == 1 ? c.Abono : iSortCol == 2 ? c.Precio : iSortCol == 3 ? c.FechaIngreso : iSortCol == 4 ? c.FechaVencimiento : iSortCol == 5? c.Pagado : iSortCol == 6 ? c.Estado : c.Cliente); // compare the sorting column

                if (sortOrder == "desc")
                {
                    list = list.OrderByDescending(orderingFunction).ToList();
                }
                else
                {
                    list = list.OrderBy(orderingFunction).ToList();

                }
            }


            return list;
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