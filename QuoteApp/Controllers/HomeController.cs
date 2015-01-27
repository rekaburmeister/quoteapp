using System.Web.Mvc;
using QuoteApp.Database.Home;

namespace QuoteApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new HomeViewModel());
        }
    }
}