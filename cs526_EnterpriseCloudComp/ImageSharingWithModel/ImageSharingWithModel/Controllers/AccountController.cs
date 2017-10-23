﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;

using ImageSharingWithModel.DAL;
using ImageSharingWithModel.Models;

namespace ImageSharingWithModel.Controllers
{
    public class AccountController : BaseController
    {
        private ImageSharingDB db = new ImageSharingDB();

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
        public ActionResult RegisterPost(UserView info)
        {
            CheckAda();

            if (ModelState.IsValid)
            {
                User User = db.Users.SingleOrDefault(u => u.UserId.Equals(info.UserID));
                if (User == null)
                {
                    //save use to database
                    User = new User(info.UserID, info.ADA);
                    db.Users.Add(User);
                }
                else
                {
                    User.ADA = info.ADA;
                    db.Entry(User).State = EntityState.Modified;
                }
                db.SaveChanges();

                SaveCookie(info.UserID, info.ADA);
            }

            ViewBag.UserID = info.UserID;
            ViewBag.ADA = info.ADA;
            ViewBag.isADA = info.ADA;
            return View("RegisterPost");
        }

        [HttpGet]
        public ActionResult Login()
        {
            CheckAda();
            ViewBag.Message = "";
            return View();
        }

        [HttpGet]
        public ActionResult DoLogin(string UserID)
        {
            CheckAda();
            User User = db.Users.SingleOrDefault(u => u.UserId.Equals(UserID));
            if (User != null)
            {
                SaveCookie(UserID, User.ADA);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "No such user registered!";
                return View("Login");
            }
        }
        protected void SaveCookie(string UserID, bool ADA)
        {
            //save user to cookie
            HttpCookie cookie = new HttpCookie("ImageSharing");
            cookie.Expires = DateTime.Now.AddMonths(2);
            cookie["UserID"] = UserID;
            cookie["ADA"] = ADA ? "true" : "false";
            Response.Cookies.Add(cookie);
        }
    }
}