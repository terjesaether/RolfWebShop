using System;
using System.IO;
using System.Web;

namespace RolfWebShop.Models
{
    public class FileOperations
    {
        // Returnerer om lagringen gikk ok
        public bool SaveUploadedFile(HttpRequest httpRequest, string storagePath, string mainFileName)
        {
            var fullStoragePath = "~" + storagePath;
            bool isSavedSuccessfully = true;
            //string fName = "";

            var counter = 0;
            try
            {
                foreach (string fileName in httpRequest.Files)
                {
                    var file = httpRequest.Files[fileName];

                    //Save file content goes here
                    //fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        // HttpContext.Current.Server.MapPath genererer absolutt path:

                        var baseDirectory = HttpContext.Current.Server.MapPath(fullStoragePath);
                        //var fileNamePath = Path.GetFileName(mainFileName);

                        //string path = Path.Combine(baseDirectory, title.Replace(" ", "_") + "_" + counter.ToString());
                        string path = Path.Combine(baseDirectory, mainFileName);

                        if (!Directory.Exists(baseDirectory.ToString()))
                            Directory.CreateDirectory(baseDirectory.ToString());

                        file.SaveAs(path);
                    }
                    counter++;
                }
            }
            catch (Exception)
            {
                return true;
            }

            if (isSavedSuccessfully)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}