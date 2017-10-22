using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using System.Web.Script.Serialization;

using ImageSharingWithModel.Models;
using ImageSharingWithModel.DAL;

namespace ImageSharingWithModel.Controllers
{
    public class ImagesController : BaseController
    {
        private ImageSharingDB db = new ImageSharingDB();

        [HttpGet]
        public ActionResult Upload()
        {
            CheckAda();
            ViewBag.Message = "";
            ViewBag.tags = new SelectList(db.Tags, "Id", "Name", 1);
            return View();
        }

        [HttpPost, ActionName("Upload")]
        public ActionResult UploadPost(ImageView image,
                                       HttpPostedFileBase ImageFile)
        {
            CheckAda();

            if (ModelState.IsValid)
            {
                HttpCookie cookie = Request.Cookies.Get("ImageSharing");
                if (cookie != null)
                {
                    image.UserId = cookie["UserID"];
                    User user = db.Users.SingleOrDefault(u => u.Id.Equals(image.UserId));

                    if (user != null)
                    {
                        /*
                         * Save image information in the database
                         */
                        Image imageEntity = new Image();
                        imageEntity.Caption = image.Caption;
                        imageEntity.Description = image.Description;
                        imageEntity.DateTaken = image.DateTaken;

                        imageEntity.User = user;

                        imageEntity.TagId = image.TagId;

                        if (ImageFile != null && ImageFile.ContentLength > 0)
                        {
                            db.Images.Add(imageEntity);
                            db.SaveChanges();

                            String imgFileName = Server.MapPath("~/Content/Images/img-" + imageEntity.Id + ".jpg");
                            ImageFile.SaveAs(imgFileName);

                            return View("Details", image);
                        }
                        else
                        {
                            ViewBag.Message = "No image file specified!";
                            return View();
                        }
                    }else
                    {
                        ViewBag.Message = "No such userid registered!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Plase register before uploading.";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Please correct errors in the form.";
                return View();
            }

        }

        [HttpGet]
        public ActionResult Query()
        {
            CheckAda();
            ViewBag.Message = "";

            HttpCookie cookie = Request.Cookies.Get("ImageSharing");
            if (cookie == null)
            {
                return RedirectToAction("Register", "Account");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Details(int Id)
        {
            CheckAda();
            Image image = db.Images.Find(Id);
            if (image != null)
            { 
                return View("Details", image);
            }
            else
            {
                return RedirectToAction("Error", "Home", new {errid="Details"});
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            CheckAda();
            Image imageEntity = db.Images.Find(Id);
            if (imageEntity != null)
            {
                HttpCookie cookie = Request.Cookies.Get("ImageSharing");
                if (cookie != null && imageEntity.User.userid.Equals(cookie["UserId"]))
                {
                    ViewBag.Message = "";
                    ViewBag.Tags = new SelectList(db.Tags, "Id", "Name", imageEntity.TagId);

                    ImageView image = new ImageView();
                    image.Id = imageEntity.Id;
                    image.TagId = imageEntity.TagId;
                    image.Caption = imageEntity.Caption;
                    image.Description = imageEntity.Description;
                    image.DateTaken = imageEntity.DateTaken;

                    return View("Edit", image);
                }
                else
                {
                    return RedirectToAction("Error", "Home", new { errid = "EditNotAuth" });
                }
            }
            else
            {
                return RedirectToAction("Error", "Home", new { errid = "EditNotFound" });
            }
        }
    }
}