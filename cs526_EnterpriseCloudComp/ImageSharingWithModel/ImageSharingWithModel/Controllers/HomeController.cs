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
        public ActionResult Index(String Id = null)
        {
            CheckAda();
            ViewBag.title = "Welcome!";

            String user = GetLoggedInUser();
            if(user == null)
            {
                ViewBag.Id = Id;
            }
            else
            {
                ViewBag.Id = user;
            }
            return View();
        }

        public ActionResult Error(String errid = "Unspecified.")
        {
            CheckAda();
            if ("Details".Equals(errid))
            {
                ViewBag.Message = "Problem with Details action!";
            } else if ("EditNotFound".Equals(errid))
            {
                ViewBag.Message = "User not found!";
            }else if ("EditNotAuth".Equals(errid))
            {
                ViewBag.Message = "User not authorized!";
            }
            else
            {
                ViewBag.Message = "Unspecified error!";
            }
            return View();
        }
    }
}

/*
 * Student name:
 * Josh Gribbon
 *
 */
