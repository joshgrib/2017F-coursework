using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using System.Web.Script.Serialization;
using System.Data.Entity;

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

                            //return View("Details", image);
                            return RedirectToAction("Details", imageEntity.Id);
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
            if(GetLoggedInUser() == null)
            {
                return ForceLogin();
            }
            else
            {
                Image imageEntity = db.Images.Find(Id);
                if (imageEntity != null)
                {
                    ImageView imageView = new ImageView();
                    imageView.Id = imageEntity.Id;
                    imageView.Caption = imageEntity.Caption;
                    imageView.Description = imageEntity.Description;
                    imageView.DateTaken = imageEntity.DateTaken;
                    imageView.TagName = imageEntity.Tag.Name;
                    imageView.UserId = imageEntity.User.UserId;
                    return View(imageView);
                }
                else
                {
                    return RedirectToAction("Error", "Home", new { errid = "Details" });
                }
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            CheckAda();
            if (GetLoggedInUser() == null)
            {
                return ForceLogin();
            }
            else
            {
                Image imageEntity = db.Images.Find(Id);
                if (imageEntity != null)
                {
                    HttpCookie cookie = Request.Cookies.Get("ImageSharing");
                    if (cookie != null && imageEntity.User.UserId.Equals(cookie["UserId"]))
                    {
                        ViewBag.Message = "";
                        ViewBag.Tags = new SelectList(db.Tags, "Id", "Name", imageEntity.TagId);

                        ImageView imageView = new ImageView();
                        imageView.Id = imageEntity.Id;
                        imageView.TagId = imageEntity.TagId;
                        imageView.Caption = imageEntity.Caption;
                        imageView.Description = imageEntity.Description;
                        imageView.DateTaken = imageEntity.DateTaken;

                        return View("Edit", imageView);
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

        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(ImageView image)
        {
            CheckAda();
            String userid = GetLoggedInUser();
            if (userid == null)
            {
                return ForceLogin();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Image imageEntity = db.Images.Find(image.Id);
                    if (imageEntity != null)
                    {
                        imageEntity.TagId = image.TagId;
                        imageEntity.Caption = image.Caption;
                        imageEntity.Description = image.Description;
                        imageEntity.DateTaken = image.DateTaken;

                        db.Entry(imageEntity).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Details", new { Id = image.Id });
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home", new { errid = "EditNotFound" });
                    }
                }
                else
                {
                    return View("Edit", image);
                }
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeletePost(FormCollection values, int Id)
        {
            /*
             * FormCollection is just here to clear up the overloading
             * of the Delete action for GET and POST
             */
            CheckAda();
            String userid = GetLoggedInUser();
            if (userid != null)
            {
                Image imageEntity = db.Images.Find(Id);
                if (imageEntity != null)
                {
                    if (imageEntity.User.UserId.Equals(userid))
                    {
                        //db.Entry(imageEntity).State = EntityState.Deleted;
                        db.Images.Remove(imageEntity);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home", new { errid = "DeleteNotAuth" });
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Home", new { errid = "DeleteNotFound" });
                }
            }
            else
            {
                return ForceLogin();
            }
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            CheckAda();
            if (GetLoggedInUser() == null)
            {
                return ForceLogin();
            }
            else
            {
                Image imageEntity = db.Images.Find(Id);
                if (imageEntity != null)
                {
                    ImageView imageView = new ImageView();
                    imageView.Id = imageEntity.Id;
                    imageView.Caption = imageEntity.Caption;
                    imageView.Description = imageEntity.Description;
                    imageView.DateTaken = imageEntity.DateTaken;
                    imageView.TagName = imageEntity.Tag.Name;
                    imageView.UserId = imageEntity.User.UserId;
                    return View(imageView);
                }
                else
                {
                    return RedirectToAction("Error", "Home", new { errid = "Delete" });
                }
            }
        }

        [HttpGet]
        public ActionResult ListAll()
        {
            CheckAda();
            IEnumerable<Image> images = db.Images.ToList();
            String userid = GetLoggedInUser();
            if(userid != null)
            {
                ViewBag.UserId = userid;
                return View(images);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public ActionResult ListByUser()
        {
            CheckAda();
            SelectList users = new SelectList(db.Users, "Id", "UserId", 1);
            return View(users);
        }

        [HttpGet]
        public ActionResult DoListByUser(int Id)
        {
            CheckAda();
            String userid = GetLoggedInUser();
            if (userid != null)
            {
                User user = db.Users.Find(Id);
                if (user != null)
                {

                    ViewBag.UserId = userid;
                    return View("ListAll", user.Images);
                }
                else
                {
                    return RedirectToAction("Error", "Home", new { errid = "ListByUser" });
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public ActionResult ListByTag()
        {
            CheckAda();
            SelectList tags = new SelectList(db.Tags, "Id", "Name", 1);
            return View(tags);
        }

        [HttpGet]
        public ActionResult DoListByTag(int Id)
        {
            CheckAda();
            String userid = GetLoggedInUser();
            if (userid != null)
            {
                Tag tag = db.Tags.Find(Id);
                if (tag != null)
                {

                    ViewBag.UserId = userid;
                    return View("ListAll", tag.Images);
                }
                else
                {
                    return RedirectToAction("Error", "Home", new { errid = "ListByUser" });
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}