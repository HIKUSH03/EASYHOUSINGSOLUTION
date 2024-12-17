using System.Web.Mvc;

namespace EasyHousingClient.Controllers
{
    public class SecurityController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserName"] == null)
            {
                filterContext.Result = new RedirectResult("/Auth/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}