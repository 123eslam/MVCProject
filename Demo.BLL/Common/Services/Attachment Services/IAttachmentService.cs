using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Common.Services.Attachment_Services
{
    public interface IAttachmentService
    {
        //(Upload - Delete) Image
        public Task<string?> UploadAsync(IFormFile file, string folderName);
        public bool Delete(string filePath);
    }
}
