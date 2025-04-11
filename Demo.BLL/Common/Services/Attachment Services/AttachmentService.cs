using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Common.Services.Attachment_Services
{
    public class AttachmentService : IAttachmentService
    {
        //Allowed extensions [.png , .jpg , .jpeg]
        public readonly List<string> _allowedExtensions = new List<string>() { ".png", ".jpg", ".jpeg" };
        //Max size (2MB) -> (2097152 BYTE)
        public const int _maxAllowedSize = 2_097_152;
        public async Task<string?> UploadAsync(IFormFile file, string folderName)
        {
            //1] Validate for extensions [.png , .jpg , .jpeg]
            var extension = Path.GetExtension(file.FileName);//Get extension from file name --> like (Eslam.png) --> extension is .png
            if(!_allowedExtensions.Contains(extension))
                return null;
            //2] Validate for max size (2MB) -> (2097152 BYTE)
            if(file.Length > _maxAllowedSize) //Size image is grater than 2mb
                return null;
            //3] Get located folder path
            //var folderPath = "D:\\Courses\\Route C43\\Eng Mariam Shindy\\07 ASP .NET Core MVC\\MVC_Project\\Demo.PL\\wwwroot\\files\\images\\";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);
            //4] Set unique file name
            //746767324675.png
            var fileName = $"{Guid.NewGuid()}{extension}";
            //5] Get file path [FolderPath + FileName]
            var filePath = Path.Combine(folderPath, fileName);
            //6] Save file as stream[Data per time]
            using var fileStream = new FileStream(filePath, FileMode.Create);
            //7] Copy file to the stream
            await file.CopyToAsync(fileStream);
            //8] Return file name
            return fileName;
        }
        public bool Delete(string filePath)
        {
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
