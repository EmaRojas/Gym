using App.Core.Domain;
using App.Services;
using App.Services.Generals;
using App.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MercadoPago.Common;
using MercadoPago.DataStructures.Preference;
using MercadoPago.Resources;
using System.Net;
using App.Web.Helpers;

namespace App.Web.Controllers
{
    [LoggedAttribute]
    public class SuscripcionController : Controller
    {
        private readonly IGeneralService<PagosMercadoPago> _pagosUsuariosService;
        private readonly IGeneralService<User> _usuarioService;

        //Credenciales
        private readonly string ClientId;
        private readonly string ClientSecret;
        private readonly string AccessToken;

        private const string returnUrl = "http://krakensoft-001-site2.itempurl.com?av=true";

        public SuscripcionController(IGeneralService<PagosMercadoPago> pagosUsuariosService, IGeneralService<User> usuarioService)
        {
            this._pagosUsuariosService = pagosUsuariosService;
            this._usuarioService = usuarioService;

            ClientId = ConfigurationManager.AppSettings["MP:ClientId"];
            ClientSecret = ConfigurationManager.AppSettings["MP:ClientSecret"];
            AccessToken = ConfigurationManager.AppSettings["MP:AccessToken"];
        }

        public ActionResult Planes()
        {
            var model = new SuscripcionModel();
            var usuario = _usuarioService.GetById(int.Parse(UserData.UserId));

            model = new SuscripcionModel
            {
                NombreCompleto = usuario.FullName,
                FechaInicio = usuario.FechaComienzo.ToShortDateString(),
                FechaVencimiento = usuario.FechaVencimiento.ToShortDateString(),
            };

            model.Mensual = Url.Action("Mensual");
            model.Semestral = Url.Action("Semestral");
            //model.Anual = Url.Action("Anual");

            return View(model);
        }

        public ActionResult Mensual()
        {
            if (string.IsNullOrEmpty(MercadoPago.SDK.ClientId))
            {
                MercadoPago.SDK.ClientId = ClientId;
                MercadoPago.SDK.ClientSecret = ClientSecret;
            }

            var preference = new Preference();

            preference.Items.Add(new Item
            {
                Title = "Software Gimnasio",
                Quantity = 1,
                CurrencyId = CurrencyId.ARS,
                UnitPrice = (float)2500.00
            });

            preference.Payer = new Payer
            {
                Name = UserData.FullName,
                Email = UserData.Email,
                Identification = new Identification
                {
                    Type = "ID",
                    Number = UserData.UserId
                }
            };

            preference.AutoReturn = AutoReturnType.approved;

            preference.BackUrls = new BackUrls { Success = returnUrl, Failure = returnUrl, Pending = returnUrl };
            preference.NotificationUrl = "http://krakensoft-001-site2.itempurl.com/MercadoPago/Notification";

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768; //TLS 1.1

            var response = preference.Save();

            return Redirect(response.InitPoint);
        }

        public ActionResult Semestral()
        {
            if (string.IsNullOrEmpty(MercadoPago.SDK.ClientId))
            {
                MercadoPago.SDK.ClientId = ClientId;
                MercadoPago.SDK.ClientSecret = ClientSecret;
            }

            var preference = new Preference();

            preference.Items.Add(new Item
            {
                Title = "Software Gimnasio",
                Quantity = 1,
                CurrencyId = CurrencyId.ARS,
                UnitPrice = (float)14000.00
            });

            preference.Payer = new Payer
            {
                Name = UserData.FullName,
                Email = UserData.Email,
                Identification = new Identification
                {
                    Type = "ID",
                    Number = UserData.UserId
                }
            };

            preference.AutoReturn = AutoReturnType.approved;

            preference.BackUrls = new BackUrls { Success = returnUrl, Failure = returnUrl, Pending = returnUrl };
            preference.NotificationUrl = "http://krakensoft-001-site2.itempurl.com/MercadoPago/Notification";

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768; //TLS 1.1

            var response = preference.Save();

            return Redirect(response.InitPoint);
        }

        public ActionResult Anual()
        {
            if (string.IsNullOrEmpty(MercadoPago.SDK.ClientId))
            {
                MercadoPago.SDK.ClientId = ClientId;
                MercadoPago.SDK.ClientSecret = ClientSecret;
            }

            var preference = new Preference();

            preference.Items.Add(new Item
            {
                Title = "Software Gimnasio",
                Quantity = 1,
                CurrencyId = CurrencyId.ARS,
                UnitPrice = (float)27500.00
            });

            preference.Payer = new Payer
            {
                Name = UserData.FullName,
                Email = UserData.Email,
                Identification = new Identification
                {
                    Type = "ID",
                    Number = UserData.UserId
                }
            };

            preference.AutoReturn = AutoReturnType.approved;

            preference.BackUrls = new BackUrls { Success = returnUrl, Failure = returnUrl, Pending = returnUrl };
            preference.NotificationUrl = "http://krakensoft-001-site2.itempurl.com/MercadoPago/Notification";

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768; //TLS 1.1

            var response = preference.Save();

            return Redirect(response.InitPoint);
        }
    }
}