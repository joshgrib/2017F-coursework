using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageSharingWithModel.Controllers
{
    public class BaseController : Controller
    {
        protected void CheckAda()
        {
            HttpCookie cookie = Request.Cookies.Get("ImageSharing");
            if (cookie != null)
            {
                ViewBag.isADA = "true".Equals(cookie["ADA"]);
            }
            else
            {
                ViewBag.isADA = false;
            }
        }
    }
}