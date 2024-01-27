using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPhotoService
    {
        Task<PhotoModel> SavePhoto(int v, PhotoModel model);
        Task<PhotoModel> GetFile(int employeeId, int type);
        Task<string> UploadImageToFolder();
    }
}
