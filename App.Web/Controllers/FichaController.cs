using App.Core.Domain;
using App.Services.Generals;
using App.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
    public class FichaController : BaseController
    {
        private IGeneralService<Cliente> _serviceCliente { get; set; }
        private IGeneralService<FichaCliente> _serviceFicha{ get; set; }

        public FichaController(IGeneralService<Cliente> serviceCliente, IGeneralService<FichaCliente> serviceFicha)
        {
            _serviceCliente = serviceCliente;
            _serviceFicha = serviceFicha;
        }
        // GET: Ficha
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            var cliente = _serviceCliente.GetByIdByUser(id);
            var ficha = _serviceFicha.GetByCriteriaByUser(x => x.Cliente.Id == id).FirstOrDefault();
            var model = new FichaModel();

            if (ficha != null)
            {
                model = new FichaModel()
                {
                    Id = ficha.Id,
                    NombreCompleto = String.Format("{0} {1}", cliente.Nombre, cliente.Apellido),
                    Telefono = cliente.Telefono,
                    ClienteId = cliente.Id,
                    Edad = CalcularEdad(cliente.FechaNacimiento),
                    Altura = ficha.Altura,
                    Peso = ficha.Peso,
                    GrupoSanguineo = ficha.GrupoSanguineo,
                    Medico = ficha.Medico,
                    PColumna = ficha.PColumna,
                    DetalleColumna = ficha.DetalleColumna,
                    ECardiaca = ficha.ECardiaca,
                    DetalleCardiaca = ficha.DetalleCardiaca,
                    LRecientes = ficha.LRecientes,
                    DetalleLesion = ficha.DetalleLesion,
                    ObjPersonal = ficha.ObjPersonal,
                    Observacion = ficha.Observacion,
                };
            }
            else
            {
                model = new FichaModel()
                {
                    NombreCompleto = String.Format("{0} {1}", cliente.Nombre, cliente.Apellido),
                    Telefono = cliente.Telefono,
                    ClienteId = cliente.Id,
                    Edad = CalcularEdad(cliente.FechaNacimiento)
                };
            }
    
            return View(model);
            //return Json(new { ficha = model, View = View(model) }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Create(FichaModel model)
        {
            try
            {
                var ficha = _serviceFicha.GetByCriteriaByUser(x => x.Cliente.Id == model.ClienteId).FirstOrDefault();
                var cliente = _serviceCliente.GetByIdByUser(model.ClienteId);
                if (ficha != null)
                {
                    ficha.Altura = model.Altura;
                    ficha.Peso = model.Peso;
                    ficha.Medico = model.Medico;
                    ficha.GrupoSanguineo = model.GrupoSanguineo;
                    ficha.PColumna = model.PColumna;
                    ficha.DetalleColumna = model.PColumna ? model.DetalleColumna : String.Empty;
                    ficha.ECardiaca = model.ECardiaca;
                    ficha.DetalleCardiaca = model.ECardiaca ? model.DetalleCardiaca : String.Empty;
                    ficha.LRecientes = model.LRecientes;
                    ficha.DetalleLesion = model.LRecientes ? model.DetalleLesion : String.Empty;
                    ficha.ObjPersonal = model.ObjPersonal;
                    ficha.Observacion = model.Observacion;

                    _serviceFicha.Update(ficha);

                    SuccessNotification("Se modifico correctamente la ficha de " + model.NombreCompleto);
                }
                else
                {
                    ficha = new FichaCliente();
                    ficha.Cliente = cliente;
                    ficha.Altura = model.Altura;
                    ficha.Peso = model.Peso;
                    ficha.Medico = model.Medico;
                    ficha.GrupoSanguineo = model.GrupoSanguineo;
                    ficha.PColumna = model.PColumna;
                    ficha.DetalleColumna = model.DetalleColumna;
                    ficha.ECardiaca = model.ECardiaca;
                    ficha.DetalleCardiaca = model.DetalleCardiaca;
                    ficha.LRecientes = model.LRecientes;
                    ficha.DetalleLesion = model.DetalleLesion;
                    ficha.ObjPersonal = model.ObjPersonal;
                    ficha.Observacion = model.Observacion;
                    _serviceFicha.Create(ficha);

                }

                return Json(new { Success = true, Url = "/Cliente/List", Message = "Se guardo correctamente la ficha de " + model.NombreCompleto }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { Success = true, Url = "/Cliente/List", Message = "Error al creal la ficha de " + model.NombreCompleto }, JsonRequestBehavior.AllowGet);

            }
        }

        public string CalcularEdad(string nacimiento)
        {
            var fechaN = DateTime.Parse(nacimiento);

            int edad = DateTime.Today.AddTicks(-fechaN.Ticks).Year - 1;

            return edad.ToString();
        }

    }
}