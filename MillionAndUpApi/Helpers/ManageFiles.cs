using MillionAndUpApi.Interfaces.Helpers;
using System.IO;
namespace MillionAndUpApi.Helpers
{
    public class ManageFiles : IManageFiles
    {
        public void SaveFile(IFormFile formFile, string path, string nameFolder)
        {
            string folderPath = $"{path}\\{nameFolder}";
            if (!string.IsNullOrEmpty(nameFolder))
            {
                
                if(!Directory.Exists(folderPath)) 
                    Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, formFile.FileName);
            using (var stream = File.Create(filePath))
            {
                formFile.CopyToAsync(stream);
            }
        }
    }
}
