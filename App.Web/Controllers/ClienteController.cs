using App.Core.Domain;
using App.Services;
using App.Services.Generals;
using App.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
    public class ClienteController : BaseController
    {
        private readonly IGeneralService<Cliente> _clienteService;
        private readonly IGeneralService<AbonoPorCliente> _abonoPorClienteService;
        // GET: Cliente

        public ClienteController(IGeneralService<Cliente> clienteService, IGeneralService<AbonoPorCliente> abonoPorClienteService)
        {
            _clienteService = clienteService;
            _abonoPorClienteService = abonoPorClienteService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public JsonResult Table(int offset, int limit, string search)
        {
            var model = new List<ClienteModel>();
            var lista = new List<Cliente>();

            //Si pongo  el nombre de otro alumno que está en la base de datos, me lo devuelve

            if (search != string.Empty && search != null)
            {
                lista = _clienteService.GetByCriteria(x => x.Nombre.Contains(search) ||
                x.Apellido.Contains(search) ||
                x.Dni.ToString().Contains(search));
            }
            else
            {
                lista = _clienteService.GetAllByUser().OrderBy(x => x.Nombre).ToList();
            }

            foreach (var item in lista.Skip(offset).Take(limit))
            {
                model.Add(new ClienteModel()
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    Apellido = item.Apellido,
                    Dni = item.Dni.ToString(),
                    Telefono = item.Telefono?.ToString() ?? "",
                    Email = item.Email,
                    FechaNacimiento = item.FechaNacimiento,
                });
            }

            return Json(new { rows = model, total = lista.Count() }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Create()
        {
            ViewBag.Controller = "Nuevo cliente";

            var cliente = new ClienteModel();

            return View(cliente);
        }

        [HttpPost]
        public ActionResult Create(ClienteModel model, string guardar)
        {
            switch (guardar)
            {
                case "guardar":
                    var existe = _clienteService.GetByCriteria(x => x.Dni == model.Dni);
                    if (existe.Count == 0)
                    {
                        var cliente = new Cliente()
                        {
                            Nombre = model.Nombre,
                            Apellido = model.Apellido,
                            Direccion = model.Direccion,
                            Dni = model.Dni,
                            Telefono = model.Telefono,
                            Email = model.Email,
                            FechaNacimiento = model.FechaNacimiento,
                        };

                        cliente.CreatedDate = DateTime.UtcNow.Date;
                        _clienteService.Create(cliente);

                        SuccessNotification("Guardado Correctamente");
                        return RedirectToAction("List");
                    }
                    else
                    {
                        ErrorNotification("Ya existe un cliente con ese DNI");
                        return RedirectToAction("Create");
                    }

                case "guardarAsignar":
                    existe = _clienteService.GetByCriteria(x => x.Dni == model.Dni);
                    if (existe.Count == 0)
                    {
                        var cliente = new Cliente()
                        {
                            Nombre = model.Nombre,
                            Apellido = model.Apellido,
                            Direccion = model.Direccion,
                            Dni = model.Dni,
                            Telefono = model.Telefono,
                            Email = model.Email,
                            FechaNacimiento = model.FechaNacimiento
                        };
                        _clienteService.Create(cliente);
                        SuccessNotification("Guardado Correctamente");
                        return RedirectToAction("Create", "AbonoPorCliente", new { clienteId = cliente.Id });
                    }
                    else
                    {
                        ErrorNotification("Ya existe un cliente con ese DNI");
                        return RedirectToAction("Create");
                    }
                default:
                    ErrorNotification("Ya existe un cliente con ese DNI");
                    return RedirectToAction("Create");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cliente = _clienteService.GetByIdByUser(id);

            var model = new ClienteModel()
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Direccion = cliente.Direccion,
                Dni = cliente.Dni.ToString(),
                Telefono = cliente.Telefono,
                Email = cliente.Email,
                FechaNacimiento = cliente.FechaNacimiento,
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ClienteModel model)
        {
            var existe = _clienteService.GetByCriteria(x => x.Dni == model.Dni && x.Id != model.Id);
            var cliente = _clienteService.GetByIdByUser(model.Id);


            if (existe.Count == 0)
            {

                cliente.Nombre = model.Nombre;
                cliente.Apellido = model.Apellido;
                cliente.Direccion = model.Direccion;
                cliente.Dni = model.Dni;
                cliente.Telefono = model.Telefono;
                cliente.Email = model.Email;
                cliente.FechaNacimiento = model.FechaNacimiento;

                _clienteService.Update(cliente);

                SuccessNotification("Editado Correctamente");

                return RedirectToAction("List");
            }
            else
            {
                ErrorNotification("Ya existe un cliente con ese DNI");

                return RedirectToAction("Edit", new { id = model.Id });
            }
        }

        [HttpGet]
        public ActionResult ListaClientes()
        {
            var clientes = _clienteService.GetByCriteriaByUser(x => x.DeletedDate == null).ToList();
            var clientesModel = new List<ClienteModel>();
            foreach (var item in clientes)
            {
                clientesModel.Add(new ClienteModel
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    Apellido = item.Apellido,
                    Dni = item.Dni,
                    Telefono = item.Telefono,
                    Email = item.Email,
                    Direccion = item.Direccion,
                    FechaNacimiento = item.FechaNacimiento,
                    TieneAbono = _abonoPorClienteService.GetByCriteriaByUser(x => x.Cliente.Id == item.Id).Any()
                });
            }
            

            return Json(clientesModel, JsonRequestBehavior.AllowGet);
        }
    }
}