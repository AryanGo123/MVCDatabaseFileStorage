﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UploadPage()
        {
            ViewBag.Message = "Upload File";

            return View();
        }

        [HttpPost]
        public ActionResult SubmitForm(UploadModel model)
        {
            MemoryStream target = new MemoryStream();
            model.File.InputStream.CopyTo(target);
            byte[] data = target.ToArray();
            string base64String = Convert.ToBase64String(data);

            //model.File.FileName
            //model.File.ContentType

            return View();
        }

    }
}