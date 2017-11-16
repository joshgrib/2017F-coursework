using ImageSharingWithAuth.DAL;
using ImageSharingWithAuth.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageSharingWithAuth.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        protected UserManager<ApplicationUser> UserManager { get; set; }

        public BaseController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        protected void CheckAda()
        {
            HttpCookie cookie = Request.Cookies.Get("ImageSharing");
            if (cookie != null)
            {
                if (cookie["ADA"] == "true")
                    ViewBag.isADA = true;
                else
                    ViewBag.isADA = false;
            }
            else
            {
                ViewBag.isADA = false;
            }
        }

        protected ApplicationUser GetLoggedInUser()
        {
            return UserManager.FindById(User.Identity.GetUserId());
        }

        protected IEnumerable<ApplicationUser> ActiveUsers()
        {
            return ApplicationDbContext.Users.Where(u => u.Active);
        }

        protected IEnumerable<Image> ApprovedImages(IEnumerable<Image> images)
        {
            return images.Where(i => i.Approved);
        }

        protected IEnumerable<Image> ApprovedImages()
        {
            return ApplicationDbContext.Images.Where(i => i.Approved);
        }

        //protected string GetLoggedInUser()
        //{
        //    HttpCookie cookie = Request.Cookies.Get("ImageSharing");
        //    if (cookie != null && cookie["Userid"] != null)
        //    {
        //        return cookie["Userid"];
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //protected ActionResult ForceLogin()
        //{
        //    return RedirectToAction("Login", "Account");
        //}
    }
}