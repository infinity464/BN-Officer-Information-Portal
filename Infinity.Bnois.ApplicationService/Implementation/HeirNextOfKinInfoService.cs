using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class HeirNextOfKinInfoService : IHeirNextOfKinInfoService
    {

        private readonly IBnoisRepository<HeirNextOfKinInfo> heirNextOfKinInfoRepository;
        public HeirNextOfKinInfoService(IBnoisRepository<HeirNextOfKinInfo> heirNextOfKinInfoRepository)
        {
            this.heirNextOfKinInfoRepository = heirNextOfKinInfoRepository;
        }


        public List<HeirNextOfKinInfoModel> GetHeirNextOfKinInfoList(int employeeId)
        {
            List<HeirNextOfKinInfo> educations = heirNextOfKinInfoRepository.FilterWithInclude(x => x.EmployeeId == employeeId, "Occupation","Gender","Relation","HeirType").ToList();
            List<HeirNextOfKinInfoModel> models = ObjectConverter<HeirNextOfKinInfo, HeirNextOfKinInfoModel>.ConvertList(educations.ToList()).ToList();
            models = models.Select(x =>
            {
                x.HeirKinTypeName = Enum.GetName(typeof(HeirKinType), x.HeirKinType);
                return x;
            }).ToList();
            return models;
        }

        public async Task<HeirNextOfKinInfoModel> GetHeirNextOfKinInfo(int heirNextOfKinInfoId)
        {
            if (heirNextOfKinInfoId <= 0)
            {
                return new HeirNextOfKinInfoModel();
            }
            HeirNextOfKinInfo heirNextOfKinInfo = await heirNextOfKinInfoRepository.FindOneAsync(x => x.HeirNextOfKinInfoId == heirNextOfKinInfoId, new List<string> { "Employee" });
            if (heirNextOfKinInfo == null)
            {
                throw new InfinityNotFoundException(" Heir/Next Of Kin Info not found");
            }
            HeirNextOfKinInfoModel model = ObjectConverter<HeirNextOfKinInfo, HeirNextOfKinInfoModel>.Convert(heirNextOfKinInfo);
            return model;
        }

        public async Task<HeirNextOfKinInfoModel> SaveHeirNextOfKinInfo(int heirNextOfKinInfoId, HeirNextOfKinInfoModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Heir/Next Of Kin Info data missing");
            }

            bool isExistData = heirNextOfKinInfoRepository.Exists(x => x.OccupationId == model.OccupationId && x.GenderId==model.GenderId && x.RelationId==model.RelationId && x.NameEng == model.NameEng && x.HeirTypeId==model.HeirTypeId  && x.HeirNextOfKinInfoId != model.HeirNextOfKinInfoId);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            if (model.HeirKinType == 1)
            {
                bool isExistNextOfKin = heirNextOfKinInfoRepository.Exists(x => x.HeirKinType == model.HeirKinType && x.EmployeeId==model.EmployeeId && x.HeirNextOfKinInfoId !=model.HeirNextOfKinInfoId);

                if (isExistNextOfKin)
                {
                    throw new InfinityInvalidDataException("Only 1 Next of kin can be added. !");
                }
            }
           

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            HeirNextOfKinInfo heirNextOfKinInfo = ObjectConverter<HeirNextOfKinInfoModel, HeirNextOfKinInfo>.Convert(model);
            if (heirNextOfKinInfoId > 0)
            {
                heirNextOfKinInfo = await heirNextOfKinInfoRepository.FindOneAsync(x => x.HeirNextOfKinInfoId == heirNextOfKinInfoId);
                if (heirNextOfKinInfo == null)
                {
                    throw new InfinityNotFoundException("Heir/Next Of Kin Info data not found !");
                }

                heirNextOfKinInfo.ModifiedDate = DateTime.Now;
                heirNextOfKinInfo.ModifiedBy = userId;
            }
            else
            {
                heirNextOfKinInfo.IsActive = true;
                heirNextOfKinInfo.CreatedDate = DateTime.Now;
                heirNextOfKinInfo.CreatedBy = userId;
            }

            heirNextOfKinInfo.NameEng = model.NameEng;
            heirNextOfKinInfo.NameBan = model.NameBan;
            heirNextOfKinInfo.Email = model.Email;
            heirNextOfKinInfo.ContactNumber = model.ContactNumber;
            heirNextOfKinInfo.PresentAddress = model.PresentAddress;
            heirNextOfKinInfo.PermanentAddress = model.PermanentAddress;
            heirNextOfKinInfo.PassportNumber = model.PassportNumber;
            heirNextOfKinInfo.HeirKinType = model.HeirKinType;
            heirNextOfKinInfo.Pradhikar = model.Pradhikar;
            heirNextOfKinInfo.HeirTypeId = model.HeirTypeId;
            heirNextOfKinInfo.RelationId = model.RelationId;
            heirNextOfKinInfo.GenderId = model.GenderId;
            heirNextOfKinInfo.EmployeeId = model.EmployeeId;
            heirNextOfKinInfo.OccupationId = model.OccupationId;


            await heirNextOfKinInfoRepository.SaveAsync(heirNextOfKinInfo);
            model.HeirNextOfKinInfoId = heirNextOfKinInfo.HeirNextOfKinInfoId;

            return model;
        }



        public async Task<bool> DeleteHeirNextOfKinInfo(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            HeirNextOfKinInfo nextOfKinInfo = await heirNextOfKinInfoRepository.FindOneAsync(x => x.HeirNextOfKinInfoId == id);
            if (nextOfKinInfo == null)
            {
                throw new InfinityNotFoundException("Heir Or Next Of Kin not found");
            }
            else
            {
                return await heirNextOfKinInfoRepository.DeleteAsync(nextOfKinInfo);
            }
        }


        public List<SelectModel> GetHeirKinTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(HeirKinType)).Cast<HeirKinType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        public async Task<HeirNextOfKinInfoModel> UpdateHeirNextOfKinInfo(HeirNextOfKinInfoModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("HeirNextOfKin data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            HeirNextOfKinInfo heirNextOfKinInfo = ObjectConverter<HeirNextOfKinInfoModel, HeirNextOfKinInfo>.Convert(model);

            heirNextOfKinInfo = await heirNextOfKinInfoRepository.FindOneAsync(x => x.HeirNextOfKinInfoId == model.HeirNextOfKinInfoId);
            if (heirNextOfKinInfo == null)
            {
                throw new InfinityNotFoundException("HeirNextOfKin not found !");
            }

            if (model.FileName != null)
            {
                heirNextOfKinInfo.FileName = model.FileName;
            }

            heirNextOfKinInfo.ModifiedDate = DateTime.Now;
            heirNextOfKinInfo.ModifiedBy = userId;
            await heirNextOfKinInfoRepository.SaveAsync(heirNextOfKinInfo);
            model.HeirNextOfKinInfoId = heirNextOfKinInfo.HeirNextOfKinInfoId;
            return model;
        }
    }
}