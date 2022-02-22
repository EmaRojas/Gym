using App.Services.Contract;
using App.Web.Security;
using System.Web.Mvc;

namespace App.Web.Controllers
{
    [AutorizarAcceso]

    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(
            IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List", "Cliente");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}