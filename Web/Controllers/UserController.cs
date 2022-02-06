using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using  Web.Security;

namespace  Web.Controllers
{
    [WebAuthorize(Roles = "Administrador")]
    public class UserController : BaseController
    {

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Usuarios";
            ViewBag.ActionUrl = "/Api/User/TableData/";
            return View();
        }

    }
}