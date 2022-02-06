using Auth0.AspNet;
using nXs.Web.DataBase;
using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Web.Models.Usuarios;

namespace ConsultorioGym.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                returnUrl = "/account/login";
            }

            var model = new UsuarioModel();

            ViewBag.State = "ru=" + HttpUtility.UrlEncode(returnUrl);

            return View(model);
        }

        [HttpPost]
        public ActionResult Login(UsuarioModel model)
        {
            var usuario = new Usuarios();
            using (var db = new GymEntities())
            {
                usuario = db.Usuarios.Where(x => x.UserName == model.User).FirstOrDefault();
            }

            if (usuario != null)
            {
                var user = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("name", usuario.UserName),

                    new KeyValuePair<string, object>("user_id", usuario.Id),

                    new KeyValuePair<string, object>("email", usuario.Email),
                    //new KeyValuePair<string, object>("picture", profile.Picture),
                    new KeyValuePair<string, object>("fullname", usuario.NombreCompleto),
                };

                user.Add(new KeyValuePair<string, object>(ClaimTypes.Role, "Admin"));


                HttpCookie userInfo = new HttpCookie("userInfo");
                userInfo["User"] = usuario.UserName;
                userInfo["user_id"] = usuario.Id.ToString();
                userInfo["fullname"] = usuario.NombreCompleto;
                //userInfo.Expires.Add(new TimeSpan(0, 1, 0));
                Response.Cookies.Add(userInfo);
                FederatedAuthentication.SessionAuthenticationModule.CreateSessionCookie(user);

                return Json(new { estado = true, url = "/Home/Index", message = "Ingreso Correctamente" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { estado = false, message = "Error, revise el email y contraseña ingresada" }, JsonRequestBehavior.AllowGet);
            }
        }

        public void LogOut()
        {
            FederatedAuthentication.SessionAuthenticationModule.SignOut();
            FederatedAuthentication.SessionAuthenticationModule.DeleteSessionTokenCookie();
            HttpCookie userInfo = new HttpCookie("userInfo");
            RedirectToAction("Login");
        }
    }
}
