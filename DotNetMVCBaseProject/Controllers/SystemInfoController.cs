using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetMVCBaseProject.Controllers
{
    public class SystemInfoController : Controller
    {
        // GET: SystemInfo
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}