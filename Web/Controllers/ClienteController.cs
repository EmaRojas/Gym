using nXs.Web.DataBase;
using nXs.Web.Models.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace nXs.Web.Controllers
{
    [Authorize]
    public class ClienteController : BaseController
    {
        private GymEntities context;
        public string userId;
        public ClienteController()
        {
            context = new GymEntities();
        }
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cliente/Details/5
        public ActionResult List()
        {
            return View();
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return PartialView("_ClienteCreate");
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(ClienteModel cliente)
        {
            try
            {
                HttpCookie reqCookies = Request.Cookies["userInfo"];
                userId = reqCookies["user_id"].ToString();

                if (context.Clientes.Where(x => x.Dni == cliente.Dni && x.DeletedDate == null && x.UserId == userId).Any())
                {
                    return Json(new { estado = false, message = "El cliente ya existe" }, JsonRequestBehavior.AllowGet);

                }
                var clienteEntity = new Clientes()
                {
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    FechaNacimiento = DateTime.Parse(cliente.FechaNacimiento),
                    Dni = cliente.Dni,
                    Telefono = cliente.Telefono,
                    UserId = userId
                };


                context.Clientes.Add(clienteEntity);
                context.SaveChanges();


                return Json(new { estado = true, message = "Se creo correctamente el cliente" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { estado = false, message = "Error, al guardar el cliente" }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(ClienteModel cliente)
        {
            return PartialView("_ClienteEdit");
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ListaClientes()
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            userId = reqCookies["user_id"].ToString();

            var clientes = context.Clientes.Where(x => x.DeletedDate == null && x.UserId == userId).ToList();

            return Json(clientes, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult ObtenerCliente(ClienteModel model)
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            userId = reqCookies["user_id"].ToString();

            var cliente = context.Clientes.Where(x => x.Id == model.Id && x.UserId == userId);

            return Json(cliente, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult EliminarCliente(ClienteModel model)
        {
            try
            {
                HttpCookie reqCookies = Request.Cookies["userInfo"];
                userId = reqCookies["user_id"].ToString();

                var cliente = context.Clientes.Where(x => x.Id == model.Id && x.UserId == userId).FirstOrDefault();
                if (cliente != null)
                {
                    cliente.DeletedDate = DateTime.Now.Date;
                    context.SaveChanges();

                }
                return Json(new { estado = true, message = "Se eliminó correctamente el cliente " }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                return Json(new { estado = false, message = "Error, al eliminar el cliente" }, JsonRequestBehavior.AllowGet);
            }
        }


        public int UserId()
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            userId = reqCookies["user_id"].ToString();
            return int.Parse(userId);
        }
    }
}
