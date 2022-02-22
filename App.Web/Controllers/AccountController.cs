using App.Core.Domain;
using App.Services;
using App.Services.Generals;
using App.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace App.Web.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/AccountController")]

    public class AccountController : BaseController
    {
        private readonly IGeneralService<User> _usuarioService;
        private readonly IGeneralService<Empresa> _empresaService;

        public AccountController(IGeneralService<User> usuarioService, IGeneralService<Empresa> empresaService)
        {
            this._usuarioService = usuarioService;
            _empresaService = empresaService;
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                returnUrl = "/";
            }

            if (UserData.UserId != string.Empty)
            {
                return RedirectToAction("index", "Home");
            }

            var model = new UsuarioModel();

            ViewBag.State = "ru=" + HttpUtility.UrlEncode(returnUrl);

            return View(model);

        }

        [HttpPost]
        public ActionResult Login(UsuarioModel model)
        {
            User consult = _usuarioService.GetUser(x => x.Password == model.Password && (x.UserName == model.User || x.Email == model.User)).FirstOrDefault();
            if (consult != null)
            {
                CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                serializeModel.Id = consult.Id;
                serializeModel.FullName = consult.FullName;
                serializeModel.Email = consult.Email;
                serializeModel.Role = consult.Role;
                serializeModel.CompanyName = _empresaService.GetById(consult.EmpresaId).Nombre;
                serializeModel.ExpirationDate = consult.FechaVencimiento.ToString();

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                string userData = serializer.Serialize(serializeModel);

                string encodedCookie = string.Empty;
                byte[] encryted = System.Text.Encoding.Unicode.GetBytes(userData.ToString());
                encodedCookie = Convert.ToBase64String(encryted);
                Response.Cookies["gym"].Value = encodedCookie;
                Response.Cookies["gym"].Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Set(Response.Cookies["gym"]);


                var result = new { Success = true, Url = "/Home/Index", Message = "Ingreso Correctamente" };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new { Success = false, Message = "Error, revise el email/usuario y contraseña ingresada" };

                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Logout()
        {
            CerrarCookie();
            return RedirectToAction("Login");
        }

        private void CerrarCookie()
        {
            HttpCookie cookie = Request.Cookies["gym"];
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Set(cookie);
            Session.Abandon();
        }

        public void Registrarse()
        {
            var empresa = new Empresa
            {
                Nombre = "Oasis"
            };

            _empresaService.Create(empresa);

            var usuario = new User
            {
                UserName = "oasis",
                FullName = "Anabella Bozzana",
                Email = "",
                Password = "oasis2022",
                EmpresaId = empresa.Id,
                FechaComienzo = DateTime.UtcNow.AddHours(-3),
                FechaVencimiento = DateTime.UtcNow.AddHours(-3).AddMonths(1),
                Role = "Admin"
            };

            _usuarioService.Create(usuario);

            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.Id = usuario.Id;
            serializeModel.FullName = usuario.FullName;
            serializeModel.Email = usuario.Email;
            serializeModel.Role = usuario.Role;
            serializeModel.ExpirationDate = usuario.FechaVencimiento.ToString();
            serializeModel.CompanyName = empresa.Nombre;

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, usuario.Email, DateTime.Now, DateTime.Now.AddMonths(6), false, userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
        }

        //[HttpPost]
        //public ActionResult Registrarse(UsuarioModel model)
        //{
        //    try
        //    {
        //        var consulta = _usuarioService.GetByEmail(x => x.Email == model.Email);

        //        if (consulta.Count == 0)
        //        {
        //            if (model.Empresa != null)
        //            {
        //                var empresa = new Empresa
        //                {
        //                    Nombre = model.Empresa
        //                };

        //                _empresaService.Create(empresa);

        //                var usuario = new User
        //                {
        //                    UserName = model.User,
        //                    FullName = model.FullName,
        //                    Email = model.Email,
        //                    Password = model.Password,
        //                    EmpresaId = empresa.Id,
        //                    FechaComienzo = DateTime.UtcNow.AddHours(-3),
        //                    FechaVencimiento = DateTime.UtcNow.AddHours(-3).AddMonths(1),
        //                    Role = "Admin"
        //                };

        //                _usuarioService.Create(usuario);

        //                CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
        //                serializeModel.Id = usuario.Id;
        //                serializeModel.FullName = usuario.FullName;
        //                serializeModel.Email = usuario.Email;
        //                serializeModel.Role = usuario.Role;
        //                serializeModel.ExpirationDate = usuario.FechaVencimiento.ToString();
        //                serializeModel.CompanyName = empresa.Nombre;

        //                JavaScriptSerializer serializer = new JavaScriptSerializer();

        //                string userData = serializer.Serialize(serializeModel);

        //                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, usuario.Email, DateTime.Now, DateTime.Now.AddMonths(6), false, userData);

        //                string encTicket = FormsAuthentication.Encrypt(authTicket);
        //                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
        //                Response.Cookies.Add(faCookie);

        //                usuario.UserId = usuario.Id.ToString();
        //                _usuarioService.Update(usuario);

        //                var condicion = new Condicion()
        //                {
        //                    Nombre = "Pago de Abono de Cliente",
        //                    TipoId = 1,
        //                    UserId = usuario.UserId
        //                };

        //                _condicionService.Create(condicion);

        //            }
        //        }
        //        else
        //        {
        //            var resultError = new { Error = false, Message = "El email ya esta registrado" };
        //            return Json(resultError, JsonRequestBehavior.AllowGet);
        //        }

        //        var result = new { Success = true, Url = "/Account/Login", Message = "Se registro correctamente" };
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        var result = new { Error = false, Message = "Error al crear la cuenta" };
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }

        //}

    }
}