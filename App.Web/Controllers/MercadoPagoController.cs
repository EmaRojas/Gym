using App.Core.Domain;
using App.Services;
using App.Services.Generals;
using App.Web.Models.MercadoPago;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
    public class MercadoPagoController : BaseController
    {
        private readonly IGeneralService<PagosMercadoPago> _pagosUsuariosService;
        private readonly IGeneralService<Notificacion> _notificacionService;
        private readonly IGeneralService<User> _usuarioService;

        //Credenciales
        private readonly string ClientId;
        private readonly string ClientSecret;
        private readonly string AccessToken;

        public MercadoPagoController(IGeneralService<PagosMercadoPago> pagosUsuariosService, IGeneralService<User> usuarioService,
            IGeneralService<Notificacion> notificacionService)
        {
            this._pagosUsuariosService = pagosUsuariosService;
            this._usuarioService = usuarioService;
            this._notificacionService = notificacionService;

            ClientId = ConfigurationManager.AppSettings["MP:ClientId"];
            ClientSecret = ConfigurationManager.AppSettings["MP:ClientSecret"];
            AccessToken = ConfigurationManager.AppSettings["MP:AccessToken"];
        }

        [HttpPost]
        public async Task<ActionResult> Notification(string type)
        {
            try
            {
                var id = Request.QueryString["data.id"];

                //if (string.IsNullOrEmpty(MercadoPago.SDK.ClientId))
                //{
                //    MercadoPago.SDK.ClientId = ClientId;
                //    MercadoPago.SDK.ClientSecret = ClientSecret;
                //}

                //MercadoPago.SDK.AccessToken = ACCESS_TOKEN;

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768; //TLS 1.1

                if (!string.IsNullOrEmpty(type) && type == "payment")
                {
                    var urlPayment = "https://api.mercadopago.com/v1/payments/" + id + "?access_token=" + AccessToken;

                    using (var httpClient = new HttpClient())
                    {
                        var responsePayment = await httpClient.GetAsync(urlPayment);

                        var resultPayment = await responsePayment.Content.ReadAsStringAsync();

                        var payment = JsonConvert.DeserializeObject<PaymentResponse>(resultPayment);

                        if (payment != null && payment.order.id != null && payment.status == "approved")
                        {
                            var notificacion = new Notificacion
                            {
                                IdNotificacionMP = long.Parse(id),
                                Fecha = DateTime.UtcNow.AddHours(-3),
                                Topic = type
                            };

                            _notificacionService.Create(notificacion);

                            var urlOrder = "https://api.mercadopago.com/merchant_orders/" + payment.order.id + "?access_token=" + AccessToken;

                            var responseOrder = await httpClient.GetAsync(urlOrder);

                            var resultOrder = await responseOrder.Content.ReadAsStringAsync();

                            var order = JsonConvert.DeserializeObject<OrderResponse>(resultOrder);

                            if (order != null && order.preference_id != null)
                            {
                                var urlPreference = "https://api.mercadopago.com/checkout/preferences/" + order.preference_id + "?access_token=" + AccessToken;

                                var responsePreference = await httpClient.GetAsync(urlPreference);

                                var resultPreference = await responsePreference.Content.ReadAsStringAsync();

                                var preference = JsonConvert.DeserializeObject<PreferenceResponse>(resultPreference);

                                if (preference != null)
                                {
                                    var usuario = _usuarioService.GetUser(x => x.Id == int.Parse(preference.payer.identification.number));

                                    if (usuario != null)
                                    {
                                        var pago = new PagosMercadoPago
                                        {
                                            Usuario = usuario.FirstOrDefault(),
                                            Fecha = DateTime.UtcNow.AddHours(-3),
                                            Notificacion = notificacion,
                                            Monto = payment.transaction_amount
                                        };

                                        _pagosUsuariosService.Create(pago);

                                        UpdateUserSuscripcion(pago);
                                    }
                                    else
                                    {
                                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "user null");
                                    }
                                }
                                else
                                {
                                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "null preference");
                                }
                            }
                            else
                            {
                                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "null order");
                            }
                        }
                        else
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "null payment");
                        }
                    }
                }

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        private void UpdateUserSuscripcion(PagosMercadoPago pago)
        {
            var usuario = pago.Usuario;

            var fechaReferencia = DateTime.UtcNow.AddHours(-3);

            if (fechaReferencia.Date < usuario.FechaVencimiento.Date)
            {
                fechaReferencia = usuario.FechaVencimiento;
            }

            switch (pago.Monto)
            {
                case 2500.00:
                    usuario.FechaVencimiento = fechaReferencia.AddMonths(1);
                    break;
                case 14000.00:
                    usuario.FechaVencimiento = fechaReferencia.AddMonths(6);
                    break;
                case 27500.00:
                    usuario.FechaVencimiento = fechaReferencia.AddYears(1);
                    break;
            }

            _usuarioService.Update(usuario);
        }
    }
}