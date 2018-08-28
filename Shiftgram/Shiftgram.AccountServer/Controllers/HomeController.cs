using System.Web.Mvc;

namespace Shiftgram.AccountServer.Controllers
{
	public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}