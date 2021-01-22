using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.BusinessLogic
{
    public static class UploadProcessor
    {
        public static int CreateUpload(string creatorName, string taskName, string fileName, string mimeType,
            string base64String, DateTime dateCreated)
        {
            UploadModel data = new UploadModel
            {
                CreatorName = creatorName,
                TaskName = taskName,
                FileName = fileName,
                MimeType = mimeType,
                Base64String = base64String,
                DateCreated = dateCreated
            };

            string sql = @"insert into dbo.FileTable(CreatorName, TaskName, FileName, MimeType, Base64String, DateCreated)
                           values (@CreatorName, @TaskName, @FileName, @MimeType, @Base64String, @DateCreated);";

            return SqlDataAccess.SaveData(sql, data);

        }

        public static List<UploadModel> LoadUpload() {
            string sql = @"select Id, CreatorName, TaskName, FileName, MimeType, Base64String, DateCreated
                           from dbo.FileTable;";

            return SqlDataAccess.LoadData<UploadModel>(sql);

        }

        public static UploadModel LoadUpload(int id)
        {
            string sql = @"select Id, CreatorName, TaskName, FileName, MimeType, Base64String, DateCreated
                           from dbo.FileTable
                           where Id=" + id.ToString() + ";";

            return SqlDataAccess.LoadSingleData(sql);

        }

        public static bool Delete(int id)
        {
            string sql = @"delete from dbo.FileTable
                           where Id=" + id.ToString() + ";";

            return SqlDataAccess.DeleteData(sql);

        }

    }
}
