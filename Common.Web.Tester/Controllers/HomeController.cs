using System.Web.Mvc;

namespace Xciles.Common.Web.Tester.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
