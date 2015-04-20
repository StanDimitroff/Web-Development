using System.Web.Mvc;

namespace AngularAspNetMvc.Web.Controllers
{
    public class ContactsController : Controller
    {
        //
        // GET: /Contacts/
        public ActionResult Index()
        {
            return PartialView("_Index");
        }

        public ActionResult Edit()
        {
            return PartialView("_Edit");
        }
	}
}