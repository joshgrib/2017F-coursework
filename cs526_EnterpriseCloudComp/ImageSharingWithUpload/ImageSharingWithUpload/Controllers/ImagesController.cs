﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageSharingWithUpload.Controllers
{
    public class ImagesController : Controller
    {
        // GET: Images
        public ActionResult Index()
        {
            return View();
        }
    }
}