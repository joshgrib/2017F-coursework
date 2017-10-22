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
                    User user = db.Users.SingleOrDefault(u => u.Id.Equals(image.UserID));

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

                            String imgFileName = Server.MapPath("~/Content/Images/" + imageEntity.Id + ".jpg");
                            ImageFile.SaveAs(imgFileName);
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
        public ActionResult Details(String Id)
        {
            CheckAda();
            String fileName = Server.MapPath("~/App_Data/Image_Info/" + Id + ".js");
            if (System.IO.File.Exists(fileName))
            {
                try
                {
                    String jsonData = System.IO.File.ReadAllText(fileName);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    ImageView image = serializer.Deserialize<ImageView>(jsonData);

                    return View("QuerySuccess", image);
                }
                catch (Exception e)
                {
                    return RedirectToAction("Error", "Home", new { msg = e.Message });
                }

            }
            else
            {
                ViewBag.Message = "Image with id '" + Id + "' not found";
                ViewBag.Id = Id;
                return View("Query");
            }
        }
    }
}