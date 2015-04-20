using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularAspNetMvc.Web.Controllers
{
    public class ContactTypesController : Controller
    {

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