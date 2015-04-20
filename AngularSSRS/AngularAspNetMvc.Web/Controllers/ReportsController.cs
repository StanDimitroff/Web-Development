using System.Web.Mvc;

namespace AngularAspNetMvc.Web.Controllers
{
    public class ReportsController : Controller
    {

        public ActionResult Index()
        {
            return PartialView("_Index");
        }

        public ActionResult ViewReport()
        {
            return PartialView("_View");
        }

        public ActionResult ActiveFlagDropDown()
        {
            return PartialView("_ActiveFlagDropDown");
        }
	}
}