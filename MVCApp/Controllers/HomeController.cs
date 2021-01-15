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

                int recordsCreated = CreateUpload(
                        model.CreatorName, 
                        model.TaskName, 
                        model.File.FileName, 
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

        [HttpPost]
        public HttpResponseBase Download(string base64Str, string mimeType) {

            byte[] data = Convert.FromBase64String(base64Str);

            HttpResponseBase resp = Response;
            resp.ContentType = mimeType;
            resp.BinaryWrite(data);


            return resp;

        }

    }
}