using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageSharingWithModel.Controllers
{
    public class AccountController : Controller
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

        // GET: Account
        public ActionResult Index()
        {
            CheckAda();
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            CheckAda();
            HttpCookie cookie = Request.Cookies.Get("ImageSharing");
            if (cookie != null)
            {
                ViewBag.UserID = cookie["UserID"];
                ViewBag.ADA = "true".Equals(cookie["ADA"]);
            }
            return View();
        }

        [HttpPost, ActionName("Register")]
        public ActionResult RegisterPost(String UserID, Boolean ADA)
        {
            CheckAda();
            HttpCookie cookie = new HttpCookie("ImageSharing");
            cookie.Expires = DateTime.Now.AddMonths(2);
            cookie["UserID"] = UserID;
            cookie["ADA"] = ADA ? "true" : "false";
            Response.Cookies.Add(cookie);

            ViewBag.UserID = UserID;
            ViewBag.ADA = ADA;
            ViewBag.isADA = ADA;
            return View("RegisterPost");
        }
    }
}