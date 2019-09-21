using System.Web;

namespace $safeprojectname$.Extensions.Storage
{
    public static class FileHelper
    {
        public static string StoreFileTemporarily(HttpPostedFile uploadedFile, string path = "~/temp/")
        {
            bool isPathExist = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(path));
            if (!isPathExist)
                System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));

            var imageFilePath = HttpContext.Current.Server.MapPath($"{path}{uploadedFile.FileName}");
            uploadedFile.SaveAs(imageFilePath);

            return imageFilePath;
        }

        public static void RemoveTemporarilyStoredFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}