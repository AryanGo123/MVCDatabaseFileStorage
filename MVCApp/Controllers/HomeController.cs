using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MVCApp.Models;
using DataLibrary;
using static DataLibrary.BusinessLogic.UploadProcessor;
using System.Net.Http;
using System.Net;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using Newtonsoft.Json;

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

        [HttpGet]
        public ActionResult DeleteData(int id)
        {
            ViewBag.Message = "Delete File";

            bool deleted = Delete(id);

            if (deleted)
            {
                return RedirectToAction("ReadData");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult ReadData()
        {
            ViewBag.Message = "Download File";

            var data = LoadUpload();
            List<UploadModel> uploadModel = new List<UploadModel>();

            foreach (var row in data) {
                uploadModel.Add(new UploadModel
                {
                    Id = row.Id,
                    CreatorName = row.CreatorName,
                    TaskName = row.TaskName,
                    DateCreated = row.DateCreated,
                    FileName = row.FileName,
                    MimeType = row.MimeType,
                    Base64String = row.Base64String
                });
            }

            return View(uploadModel);
        }

        [HttpPost]
        public ActionResult SubmitForm(UploadModel model)
        {
            if (ModelState.IsValid) {
                MemoryStream target = new MemoryStream();
                model.File.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                string base64String = Convert.ToBase64String(data);

                DateTime date = DateTime.Now;

                var dbData = LoadUpload();

                string trim = Regex.Replace(model.File.FileName, " ", "-");

                foreach (var row in dbData)
                {
                    if (row.FileName == trim) {
                        trim += "-cpy";
                    }
                }

                int recordsCreated = CreateUpload(
                        model.CreatorName, 
                        model.TaskName, 
                        trim, 
                        model.File.ContentType,
                        base64String, 
                        date
                    );

                return RedirectToAction("ReadData");
            }

            //model.File.FileName
            //model.File.ContentType

            return View();
        }

        [HttpGet]
        public HttpResponseMessage Download(int id) {

            var model = LoadUpload(id);

            if (model.Id == -1) {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            byte[] data = Convert.FromBase64String(model.Base64String);

            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.ContentType = model.MimeType;
            System.Web.HttpContext.Current.Response.Charset = "UTF-16";
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Length", data.Length.ToString());
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", model.FileName));
            System.Web.HttpContext.Current.Response.BinaryWrite(data);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();

            return new HttpResponseMessage(HttpStatusCode.OK);

        }

    }
}