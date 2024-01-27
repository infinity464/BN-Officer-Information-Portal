using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class PhotoService : IPhotoService
    {
        private readonly IBnoisRepository<Photo> photosRepository;
        private readonly IBnoisRepository<EmployeeGeneral> employeeGeneralRepository;
        
        public PhotoService(IBnoisRepository<EmployeeGeneral> employeeGeneralRepository,IBnoisRepository<Photo> photosRepository)
        {
            this.photosRepository = photosRepository;
            this.employeeGeneralRepository = employeeGeneralRepository;
        }

        public async Task<PhotoModel> GetFile(int employeeId, int type)
        {
            Guid userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToGuid();
            Photo file = await photosRepository.FindOneAsync(x => x.PhotoType==type && x.EmployeeId == employeeId);
            if (file == null)
            {
                return new PhotoModel() { CreatedDate = DateTime.Now };
            }
            PhotoModel model = ObjectConverter<Photo, PhotoModel>.Convert(file);
            return model;
        }

        public async Task<PhotoModel> SavePhoto(int employeeId, PhotoModel model)
        {
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId;
            if (string.IsNullOrEmpty(userId) )
            {
                throw new InfinityArgumentMissingException("Invalid User");
            }

            if (model == null)
            {
                throw new InfinityArgumentMissingException("data missing");
            }
            Photo photo = photo = await photosRepository.FindOneAsync(x => x.EmployeeId == employeeId && x.PhotoType == model.PhotoType)??new Photo();
            if (photo.PhotoId>0)
            {
                photo.ModifiedBy = model.ModifiedBy;
                photo.ModifiedDate = DateTime.Now;
            }
            else
            {
                photo.CreatedBy = userId;
                photo.CreatedDate = DateTime.Now;
            }
            photo.PhotoType = model.PhotoType;
            photo.EmployeeId = employeeId;
            photo.FileName = model.FileName;
            photo.ContentType = model.ContentType;
            photo.FileExtension = model.FileExtension;
            photo.IsActive = true;
            await photosRepository.SaveAsync(photo);
            model.PhotoId = photo.PhotoId;
            return model;
        }

        public Task<string> UploadImageToFolder()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ডাটা মাইগ্রেশনের পর এই কোড রান হবে
        /// </summary>
        /// <returns></returns>
        //public async Task<string> UploadImageToFolder()
        //{
        //        int count = 0;

        //        IQueryable<EmployeeGeneral> employeeGenerals = employeeGeneralRepository.FilterWithInclude(x => x.Employee.RankCategoryId == 3, "Employee.RankCategory");
        //        foreach (var item in employeeGenerals.ToList())
        //        {
        //            Photo photo = new Photo();

        //            photo.PhotoType = 1;
        //            photo.EmployeeId = item.EmployeeId;
        //            photo.FileName = item.Employee.PNo + ".jpg";
        //            photo.ContentType = "image/jpeg";
        //            photo.FileExtension = "jpg";
        //            photo.CreatedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
        //            photo.CreatedDate = DateTime.Now;
        //            photo.ModifiedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
        //            photo.ModifiedDate = DateTime.Now;
        //            photo.IsActive = true;

        //            await photosRepository.SaveAsync(photo);

        //            count = count + 1;
        //        }
        //        return  count.ToString() + " Officers Photo Uploaded Successfully.";
        //    }

    }
}
