using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace MVCApp.Models
{
    public class UploadModel
    {
        public string CreatorName { get; set; }
        public string TaskName { get; set; }
        public DateTime DateCreated { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public string Base64String { get; set; }
    }
}