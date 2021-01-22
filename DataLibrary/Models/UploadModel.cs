using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class UploadModel
    {
        public int Id { get; set; }
        public string CreatorName { get; set; }
        public string TaskName { get; set; }
        public DateTime DateCreated { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public string Base64String { get; set; }
    }
}
