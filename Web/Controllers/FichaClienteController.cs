using nXs.Web.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace nXs.Web.Controllers
{
    [Authorize]

    public class FichaClienteController : BaseController
    {
        private GymEntities context;
        public string userId;

        public FichaClienteController()
        {
            context = new GymEntities();
        }

        // GET: FichaCliente
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("_CreateFicha");
        }

        [HttpPost]
        public ActionResult ValidarFicha(int IdCliente)
        {
            var userId = UserId();
            var existFicha = context.FichaClientes.Where(x => x.Id == IdCliente && x.Activo == true && x.UserId == userId).Any();

            return Json(new { exist = existFicha });
        }

        public int UserId()
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            userId = reqCookies["user_id"].ToString();
            return int.Parse(userId);
        }


    }
}