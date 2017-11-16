using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageSharingWithAuth.Models;

using System.IO;
using System.Web.Script.Serialization;

namespace ImageSharingWithAuth.Controllers
{
    public class HomeController : BaseController
    {
        
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index(string id = "Stranger")
        {
            CheckAda();

            ViewBag.Title = "Welcome";

            ApplicationUser user = GetLoggedInUser();
            if (user == null)
            {
                ViewBag.Id = id;
            }
            else
            {
                ViewBag.Id = user.UserName;
            }
            return View();
        }

        public ActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View("Error");
        }
    }
}