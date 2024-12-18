using System.Web.Mvc;

namespace EasyHousingClient.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
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

        // destroy session to logout
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Auth");
        }
    }
}