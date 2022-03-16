using App.Core.Domain;
using App.Services.Generals;
using App.Web.Models;
using App.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
    [AutorizarAcceso]

    public class AbonoController : BaseController
    {
        public IGeneralService<Abono> _serviceAbono { get; set; }
        // GET: AbonoDefault

        public AbonoController(IGeneralService<Abono> serviceAbono)
        {
            _serviceAbono = serviceAbono;
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
            var model = new List<AbonoModel>();
            var lista = new List<Abono>();

            //Si pongo  el nombre de otro alumno que está en la base de datos, me lo devuelve

            if (search != string.Empty && search != null)
            {
                lista = _serviceAbono.GetByCriteria(x => x.Nombre.Contains(search) ||
                x.Nombre.Contains(search) ||
                x.Precio.ToString().Contains(search));
            }
            else
            {
                lista = _serviceAbono.GetAllByUser().OrderBy(x => x.Nombre).ToList();
            }

            foreach (var item in lista.Skip(offset).Take(limit))
            {
                model.Add(new AbonoModel()
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    Precio = item.Precio,
                });
            }

            return Json(new { rows = model, total = lista.Count() }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Lista()
        {
            var abonos = _serviceAbono.GetByCriteriaByUser(x => x.DeletedDate == null).ToList();

            return Json(abonos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View(new AbonoModel());
        }

        [HttpPost]
        public ActionResult Create(AbonoModel model)
        {
            var abono = new Abono()
            {
                Nombre = model.Nombre,
                Precio = model.Precio,
            };

            _serviceAbono.Create(abono);
            SuccessNotification("Guardado Correctamente");
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            var abono = _serviceAbono.GetByIdByUser(id);
            var model = new AbonoModel();

            if (abono != null)
            {
                model = new AbonoModel()
                {
                    Nombre = abono.Nombre,
                    Precio = abono.Precio.ToString(),
                    Id = abono.Id
                };
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AbonoModel model)
        {
            var abono = _serviceAbono.GetByIdByUser(model.Id);
            abono.Nombre = model.Nombre;
            abono.Precio = model.Precio;

            _serviceAbono.Update(abono);
            SuccessNotification("Editado Correctamente");

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            var abono = _serviceAbono.GetByIdByUser(id);

            abono.DeletedDate = DateTime.UtcNow.AddDays(-3);
            _serviceAbono.Update(abono);

            return RedirectToAction("List");
        }
    }
}