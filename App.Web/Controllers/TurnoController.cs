using App.Core.Domain;
using App.Services.Generals;
using App.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
    public class TurnoController : BaseController
    {
        private readonly IGeneralService<Turno> _turnoService;
        private readonly IGeneralService<AbonoPorCliente> _abonoPorClienteService;

        public TurnoController(IGeneralService<Turno> turnoService, IGeneralService<AbonoPorCliente> abonoPorClienteService)
        {
            _turnoService = turnoService;
            _abonoPorClienteService = abonoPorClienteService;
        }

        public ActionResult List()
        {
            return RedirectToAction("List", "AbonoPorCliente");
        }

        // GET: Turno
        public ActionResult Create(int id)
        {
            var turnos = _turnoService.GetByCriteriaByUser(x => x.AbonoPorCliente.Id == id);

            var abonoPorCliente = _abonoPorClienteService.GetByIdByUser(id);
            TurnoPorClienteModel turnoPorCliente = new TurnoPorClienteModel();
            turnoPorCliente.AbonoPorAlumnoId = id;
            turnoPorCliente.Cliente = abonoPorCliente.Cliente.Nombre + " " + abonoPorCliente.Cliente.Apellido;
            turnoPorCliente.Abono = abonoPorCliente.Abono.Nombre;
            turnoPorCliente.FechaVencimiento = DateTime.ParseExact(abonoPorCliente.FechaVencimiento.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);

            if (turnos.Count != 0)
            {
                foreach (var item in turnos)
                {
                    turnoPorCliente.ListaTurnos.Add(new TurnoModel
                    {
                        Dia = item.Dia,
                        HoraInicial = item.HoraInicio,
                        HoraFinal = item.HoraFin,
                    });
                }
            }

            return View(turnoPorCliente);
        }

        [HttpPost]
        public ActionResult Create(TurnoPorClienteModel model)
        {
            var turnos = _turnoService.GetByCriteriaByUser(x => x.AbonoPorCliente.Id == model.AbonoPorAlumnoId);

            foreach (var item in turnos)
            {
                _turnoService.Delete(item);
            }

            foreach (var item in model.ListaTurnos)
            {
                var turno = new Turno();

                turno.AbonoPorCliente = _abonoPorClienteService.GetById(model.AbonoPorAlumnoId);
                turno.Dia = item.Dia;
                turno.HoraInicio = item.HoraInicial;
                turno.HoraFin = item.HoraFinal;
                _turnoService.Create(turno);
            }

            SuccessNotification("Guardado Correctamente");

            return RedirectToAction("List", "AbonoPorCliente");
        }

        public PartialViewResult AddTableRow()
        {
            var turno = new TurnoModel();

            turno.HoraInicialId = Guid.NewGuid().ToString();
            turno.HoraFinId = Guid.NewGuid().ToString();


            return PartialView("_tableRow", turno);
        }

        public ActionResult ConsultaTurno()
        {
            return View();
        }

        public JsonResult Table(int offset, int limit, string search, string fecha = "")
        {
            var model = new TurnoPorClienteModel();
            var lista = new List<Turno>();

            //Si pongo  el nombre de otro alumno que está en la base de datos, me lo devuelve

            lista = _turnoService.GetByCriteriaByUser(x => x.AbonoPorCliente.FechaVencimiento >= DateTime.UtcNow.AddHours(-3) && x.Dia.ToLower() == DateTime.UtcNow.AddHours(-3).ToString("dddd", new CultureInfo("es-ES")).ToLower()).OrderBy(x => x.AbonoPorCliente.Cliente.Nombre).ToList();

            //if (search != string.Empty && search != null)
            //{
            //    lista = _turnoService.GetByCriteria(x => x.AbonoPorCliente.Cliente.Nombre.Contains(search) ||
            //    x.AbonoPorCliente.Cliente.Apellido.Contains(search) ||
            //    x.AbonoPorCliente.Abono.Nombre.ToString().Contains(search) ||
            //    x.HoraInicio.Contains(search) ||
            //    x.HoraFin.Contains(search));
            //}
            //else
            //{
            //    if (fecha != string.Empty)
            //    {
            //        var fechaParse = DateTime.ParseExact(fecha, "dd/MM/yyyy", null);
            //        lista = _turnoService.GetByCriteriaByUser(x => x.AbonoPorCliente.FechaVencimiento >= DateTime.UtcNow.AddHours(-3)
            //        && x.AbonoPorCliente.FechaVencimiento.Value.ToString("dddd", new CultureInfo("es-ES")) == x.Dia).OrderBy(x => x.AbonoPorCliente.Cliente.Nombre).ToList();
            //    }
            //    else
            //    {
            //        lista = _turnoService.GetByCriteriaByUser(x => x.AbonoPorCliente.FechaVencimiento >= DateTime.UtcNow.AddHours(-3) && x.Dia.ToLower() == DateTime.UtcNow.AddHours(-3).ToString("dddd", new CultureInfo("es-ES")).ToLower()).OrderBy(x => x.AbonoPorCliente.Cliente.Nombre).ToList();
            //    }
            //}

            foreach (var item in lista.Skip(offset).Take(limit))
            {
                model.ListaTurnos.Add(new TurnoModel()
                {
                    Id = item.Id,
                    Dia = item.Dia,
                    HoraInicial = item.HoraInicio,
                    HoraFinal = item.HoraFin,
                    Cliente = item.AbonoPorCliente.Cliente.Nombre + " " + item.AbonoPorCliente.Cliente.Apellido,
                    Abono = item.AbonoPorCliente.Abono.Nombre
                });
            }

            return Json(new { rows = model.ListaTurnos, total = lista.Count() }, JsonRequestBehavior.AllowGet);
        }


    }
}