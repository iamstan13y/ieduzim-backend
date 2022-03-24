namespace IEduZimAPI.CoreClasses
{
    public class FileWriter
    {
        //    public static string uploadFolder
        //    {
        //        get
        //        {
        //            string path = $"{Directory.GetCurrentDirectory()}/ClientApp/";
        //            var folder = new DirectoryInfo(path);
        //            if (!folder.Exists) folder.Create();
        //            return $"{Directory.GetCurrentDirectory()}/ClientApp/";
        //        }
        //    }

        //    public static string Write(IFormFile file, string username)
        //    {
        //        var originalName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //        var ext = Path.GetExtension(originalName).ToLower();
        //        var uploadedFile = new UploadedFile()
        //        {
        //            Url = $"{DateTime.Now.Ticks}_{username}{ext}",
        //        };
        //        using (var stream = new FileStream($"{getUploadPath()}\\{uploadedFile.Url}", FileMode.Create))
        //            file.CopyTo(stream);
        //        return uploadedFile.Url;
        //    }

        //    private static string getUploadPath()
        //    {
        //        var folder = new DirectoryInfo($"{uploadFolder}src/assets/prescriptions");
        //        if (!folder.Exists) folder.Create();
        //        return folder.FullName;
        //    }

        //    public static void RemoveFile(string uploadedFile)=>
        //            File.Delete(uploadedFile);
    }
}