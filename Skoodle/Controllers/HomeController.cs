using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skoodle.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult SPATestMethod()
        {
            return PartialView();
        }

        public PartialViewResult IndexPartial()
        {
            return PartialView("Index");
        }
    }
}