using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetMVCBaseProject.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: UserInfo
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}