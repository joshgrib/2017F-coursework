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
    public class ImagesController : Controller
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

        // GET: Images
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Upload()
        {
            CheckAda();

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

        [HttpPost, ActionName("Upload")]
        public ActionResult UploadPost(Image image,
                                       HttpPostedFileBase ImageFile)
        {
            CheckAda();

            if (ModelState.IsValid)
            {
                HttpCookie cookie = Request.Cookies.Get("ImageSharing");
                if (cookie != null)
                {
                    image.UserID = cookie["UserID"];

                    /*
                     * Save image information on the server file system
                     */
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    String jsonData = serializer.Serialize(image);

                    String fileName = Server.MapPath("~/App_Data/Image_Info/" + image.ID + ".js");

                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        try
                        {
                            String imgFileName = Server.MapPath("~/Content/Images/" + image.ID + ".jpg");
                            ImageFile.SaveAs(imgFileName);
                            System.IO.File.WriteAllText(fileName, jsonData);
                        }
                        catch (Exception e)
                        {
                            return RedirectToAction("Error", "Home", new { msg = e.Message });
                        }

                    }

                    ViewBag.Message = "";
                    return View("QuerySuccess", image);
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
            String fileName = Server.MapPath("~/App_Data/Image_Info/" + Id + ".js");
            if (System.IO.File.Exists(fileName))
            {
                try
                {
                    String jsonData = System.IO.File.ReadAllText(fileName);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Image image = serializer.Deserialize<Image>(jsonData);

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