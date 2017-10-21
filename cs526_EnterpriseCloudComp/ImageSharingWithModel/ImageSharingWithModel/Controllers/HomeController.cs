using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using System.Web.Script.Serialization;

using ImageSharingWithModel.Models;

namespace ImageSharingWithModel.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(String userID = null)
        {
            CheckAda();
            ViewBag.title = "Welcome!";

            if (userID == null)
            {
                HttpCookie cookie = Request.Cookies.Get("ImageSharing");
                if (cookie != null)
                {
                    ViewBag.userID = cookie["UserID"];
                    ViewBag.welcomeMsg = "Click 'Upload' to add one of your images, or 'Query' to search for an image.";
                }
                else
                {
                    ViewBag.userID = "stranger";
                    ViewBag.welcomeMsg = "Click 'Register' above to join the site!";
                }
            }
            else
            {
                ViewBag.userID = userID;
                ViewBag.welcomeMsg = "Click 'Upload' to add one of your images, or 'Query' to search for an image.";
            }
            return View();
        }

        public ActionResult Error(String msg = "Error information unavailable.")
        {
            CheckAda();
            ViewBag.Title = "Error";
            ViewBag.Message = msg;
            return View();
        }
    }
}

/*
 * Student name:
 * Josh Gribbon
 *
 */
