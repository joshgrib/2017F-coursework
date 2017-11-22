using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageSharingWithAuth.Models;
using System.Web.Script.Serialization;
using ImageSharingWithAuth.DAL;
using System.Data.Entity;

namespace ImageSharingWithAuth.Controllers
{
    public class ImageController : BaseController
    {
        
        [HttpGet]
        public ActionResult Upload()
        {
            CheckAda();
            ViewBag.Message = "";

            ViewBag.Tags = new SelectList(ApplicationDbContext.Tags, "Id", "Name", 1);

            //string userid = GetLoggedInUser();
            //if (userid == null)
            //{
            //    return ForceLogin();
            //}

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(ImageView image, HttpPostedFileBase ImageFile)
        {
            CheckAda();
            ViewBag.Tags = new SelectList(ApplicationDbContext.Tags, "Id", "Name", 1);
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = GetLoggedInUser();
                    //string userid = GetLoggedInUser();
                    //if (userid == null)
                    //{
                    //    return ForceLogin();
                    //}

                    if (user != null)
                    {

                        Image imageEntity = new Image();
                        imageEntity.Caption = image.Caption;
                        imageEntity.Description = image.Description;
                        imageEntity.Date = image.DateTaken;
                        imageEntity.User = user;

                        imageEntity.TagID = image.TagId;

                        imageEntity.Approved = false;


                        if (ImageFile != null && ImageFile.ContentLength > 0)
                        {
                            ApplicationDbContext.Images.Add(imageEntity);
                            if (ImageFile.ContentType != "image/jpeg")
                            {
                                ViewBag.Message = "Image File must be a jpeg!";
                                return View();
                            }

                            if (ImageFile.ContentLength > 200000)
                            {
                                ViewBag.Message = "Image File must be less than 200 KB!";
                                return View();
                            }
                            ApplicationDbContext.SaveChanges();
                            string imgFileName = Server.MapPath("~/Content/Images/img-" + imageEntity.Id + ".jpg");
                            ImageFile.SaveAs(imgFileName);


                            return RedirectToAction("Details", new { Id = imageEntity.Id });
                        }
                        else
                        {
                            ViewBag.Message = "No image file specified.";
                            return View();
                        }

                    }
                    else
                    {
                        ViewBag.Message = "No such user registered.";
                        return View();
                    }

                }
            
                else
                {
                    ViewBag.Message = "Please correct the errors in the form!";
                    return View();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction("Error", "Home", new { message = "Error while uploading image." });
            }
        }

        [HttpGet]
        public ActionResult Query()
        {
            CheckAda();
            ViewBag.Message = "";
            

            return View();
        }

        [HttpGet]
        public ActionResult Details(int Id)
        {
            CheckAda();
            try
            {
                Image imageEntity = ApplicationDbContext.Images.Find(Id);
                if (imageEntity != null)
                {
                    ImageView image = new ImageView();
                    image.Id = imageEntity.Id;
                    image.Caption = imageEntity.Caption;
                    image.DateTaken = imageEntity.Date;
                    image.Description = imageEntity.Description;
                    image.TagName = imageEntity.Tag.Name;
                    image.Userid = imageEntity.User.UserName;
                    return View("QuerySuccess", image);
                }
                else
                {
                    return RedirectToAction("Error", "Home", new { message = "Image to view not found." });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction("Error", "Home", new { message = "Error while querying image." });
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            CheckAda();
            try
            {
                Image imageEntity = ApplicationDbContext.Images.Find(Id);

                if (imageEntity != null)
                {

                    //string userid = GetLoggedInUser();
                    //if (userid == null)
                    //{
                    //    return ForceLogin();
                    //}
                    ApplicationUser user = GetLoggedInUser();
                    if (imageEntity.User.UserName.Equals(user.Email))
                    {

                        ViewBag.Message = "";
                        ViewBag.Tags = new SelectList(ApplicationDbContext.Tags, "Id", "Name", imageEntity.TagID);
                        ImageView image = new ImageView();

                        image.Caption = imageEntity.Caption;
                        image.DateTaken = imageEntity.Date;
                        image.Description = imageEntity.Description;
                        image.TagId = imageEntity.TagID;
                        image.Id = imageEntity.Id;

                        return View("Edit", image);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home", new { message = "Error: User Id does not have permission to edit this image." });
                    }
                }
                else
                {
                    ViewBag.Message = "Image with identifier " + Id + " not found.";
                    ViewBag.Id = Id;
                    return View("Query");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction("Error", "Home", new { message = "Error while editing image." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ImageView image)
        {
            CheckAda();
            if(ModelState.IsValid)
            {
                Image imageEntity = ApplicationDbContext.Images.Find(image.Id);
                if (imageEntity != null)
                {

                    ApplicationUser user = GetLoggedInUser();
                    if (imageEntity.User.UserName.Equals(user.Email))
                    {
                        imageEntity.TagID = image.TagId;
                        imageEntity.Caption = image.Caption;
                        imageEntity.Description = image.Description;
                        imageEntity.Date = image.DateTaken;
                        ApplicationDbContext.Entry(imageEntity).State = EntityState.Modified;
                        ApplicationDbContext.SaveChanges();
                        return RedirectToAction("Details", new { id = image.Id });
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home", new { message = "User not authorized to do that action." });
                    }

                }
                else
                {
                    return RedirectToAction("Error", "Home", new { message = "Image to edit not found." });
                }
            }
            else
            {
                return View("Edit", image);
            }
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            CheckAda();
            try
            {

                //string userid = GetLoggedInUser();
                //if (userid == null)
                //{
                //    return ForceLogin();
                //}

                Image imageEntity = ApplicationDbContext.Images.Find(Id);
                if (imageEntity != null)
                {
                    ImageView image = new ImageView();
                    image.Id = imageEntity.Id;
                    image.Caption = imageEntity.Caption;
                    image.DateTaken = imageEntity.Date;
                    image.Description = imageEntity.Description;
                    image.TagName = imageEntity.Tag.Name;
                    image.Userid = imageEntity.User.UserName;
                    return View("Delete", image);
                }
                else
                {
                    return RedirectToAction("Error", "Home", new { message = "Image to Delete not found." });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction("Error", "Home", new { message = "Error while deleting image." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FormCollection values, int id)
        {
            CheckAda();
            Image imageEntity = ApplicationDbContext.Images.Find(id);

            if (imageEntity != null)
            {

                ApplicationUser user = GetLoggedInUser();
                if (imageEntity.User.UserName.Equals(user.Email))
                {
                    ApplicationDbContext.Images.Remove(imageEntity);
                    ApplicationDbContext.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Error", "Home", new { message = "User not authorized to delect this image." });
                }
            }
            else
            {
                return RedirectToAction("Error", "Home", new { message = "Image to delete not found." });
            }
        }

        public ActionResult ListAll()
        {
            CheckAda();
            IEnumerable<Image> images = ApprovedImages().ToList();

            //string userid = GetLoggedInUser();
            //if (userid == null)
            //{
            //    return ForceLogin();
            //}
            ApplicationUser user = GetLoggedInUser();
            ViewBag.Userid = user.UserName;
            return View(images);


        }

        public ActionResult ListByUser()
        {
            CheckAda();

            //string userid = GetLoggedInUser();
            //if (userid == null)
            //{
            //    return ForceLogin();
            //}
            string userId = GetLoggedInUser().Id;

                SelectList users = new SelectList(ActiveUsers(), "Id", "UserName", userId);
            return View(users);
        }

        public ActionResult DoListByUser(int Id)
        {
            CheckAda();
            ApplicationUser user = GetLoggedInUser();
            if (user != null)
            {
                ViewBag.Userid = user.UserName;
                return View("ListAll", ApprovedImages(user.Images).ToList());
            }
            else
            {
                return RedirectToAction("Error", "Home", new { message = "Error: Cannot find user with id " + Id });
            }
        }

        public ActionResult ListByTag()
        {
            CheckAda();


            SelectList tags = new SelectList(ApplicationDbContext.Tags, "Id", "Name", 1);
            return View(tags);
        }

        public ActionResult DoListByTag(int Id)
        {
            CheckAda();
            string userid = GetLoggedInUser().UserName;
            Tag tag = ApplicationDbContext.Tags.Find(Id);
            if (tag != null)
            {
                ViewBag.Userid = userid;
                return View("ListAll", ApprovedImages(tag.Images).ToList());
            }
            else
            {
                return RedirectToAction("Error", "Home", new { message = "Error: Cannot find tag with id " + Id });
            }
        }


        [HttpGet]
        [Authorize(Roles="Approver")]
        public ActionResult Approve()
        {
            CheckAda();
            ViewBag.Message = "";
            List<SelectItemView> model = new List<SelectItemView>();
            foreach (var image in ApplicationDbContext.Images)
            {
                if (!image.Approved)
                {
                    model.Add(new SelectItemView(image.Id.ToString(), image.Caption, false));

                }            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Approver")]
        public ActionResult Approve(List<SelectItemView> model)
        {
            CheckAda();
            var disapprovedImages = new List<SelectItemView>();
            foreach (var imod in model)
            {
                Image img = ApplicationDbContext.Images.Find(imod.Id);
                if (imod.Checked)
                {
                    img.Approved = false;
                    
                }
                else
                {
                    imod.Name = img.Caption;
                    disapprovedImages.Add(imod);
                }
            }
            ApplicationDbContext.SaveChanges();

            ViewBag.Message = "Image Approved";

            return View(disapprovedImages);
        }
    }
}