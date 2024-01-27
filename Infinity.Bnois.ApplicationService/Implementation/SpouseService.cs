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
    public class SpouseService : ISpouseService
    {

        private readonly IBnoisRepository<Spouse> spouseRepository;
        private readonly IBnoisRepository<EmployeeGeneral> employeeGeneralRepository;
        public SpouseService(IBnoisRepository<Spouse> spouseRepository, IBnoisRepository<EmployeeGeneral> employeeGeneralRepository)
        {
            this.spouseRepository = spouseRepository;
            this.employeeGeneralRepository = employeeGeneralRepository;
        }


        public List<SpouseModel> GetSpouses(int employeeId)
        {
            List<Spouse> educations = spouseRepository.FilterWithInclude(x => x.EmployeeId == employeeId, "Occupation", "Occupation1", "Occupation2","RankCategory").ToList();
            List<SpouseModel> models = ObjectConverter<Spouse, SpouseModel>.ConvertList(educations.ToList()).ToList();
            models = models.Select(x =>
            {
                x.RelationTypeName = Enum.GetName(typeof(RelationType), x.RelationType);
                x.CurrentStatusName = Enum.GetName(typeof(CurrentStatus), x.CurrentStatus);
                return x;
            }).ToList();
            return models;
        }

        public async Task<SpouseModel> GetSpouse(int spouseId)
        {
            if (spouseId <= 0)
            {
                return new SpouseModel();
            }
            Spouse spouse = await spouseRepository.FindOneAsync(x => x.SpouseId == spouseId, new List<string> { "Employee" });
            if (spouse == null)
            {
                throw new InfinityNotFoundException(" Spouse not found");
            }
            SpouseModel model = ObjectConverter<Spouse, SpouseModel>.Convert(spouse);
            return model;
        }

    
        public async Task<SpouseModel> SaveSpouse(int spouseId, SpouseModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Spouse data missing");
            }

            bool isExistData = spouseRepository.Exists(x => x.OccupationId == model.OccupationId && x.BNameEng == model.BNameEng && x.ANameEng==model.ANameEng  && x.SpouseId != spouseId);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Spouse spouse = ObjectConverter<SpouseModel, Spouse>.Convert(model);
            if (spouseId > 0)
            {
                spouse = await spouseRepository.FindOneAsync(x => x.SpouseId == spouseId);
                if (spouse == null)
                {
                    throw new InfinityNotFoundException("Spouse data not found !");
                }

                spouse.ModifiedDate = DateTime.Now;
                spouse.ModifiedBy = userId;
            }
            else
            {
                spouse.IsActive = true;
                spouse.CreatedDate = DateTime.Now;
                spouse.CreatedBy = userId;
            }

            spouse.EmployeeId = model.EmployeeId;
            spouse.OccupationId = model.OccupationId;
            spouse.BNameEng = model.BNameEng;
            spouse.BNameBan = model.BNameBan;
            spouse.ANameEng = model.ANameEng;
            spouse.ANameBan = model.ANameBan;
            spouse.NickName = model.NickName;
            spouse.NID = model.NID;
            spouse.MarriageDate = model.MarriageDate;
            spouse.BirthPlace = model.BirthPlace;
            spouse.RelationType = model.RelationType;
            spouse.CurrentStatus = model.CurrentStatus;
            spouse.DeadDate = model.DeadDate;
            spouse.DeadReason = model.DeadReason;
            spouse.IdMark = model.IdMark;
            spouse.EduQualification = model.EduQualification;
            spouse.ServiceAddress = model.ServiceAddress;
            spouse.SocialActivity = model.SocialActivity;
            spouse.Degination = model.Degination;
            spouse.DateofBirth=model.DateofBirth;
            //spouse.Age=DateTime.Today.Year-model.DateofBirth.Value.Year;

            spouse.IsFatherDead = model.IsFatherDead;
            spouse.FatherName = model.FatherName;
            spouse.FatherNameBan = model.FatherNameBan;
            spouse.FatherPreAddress = model.FatherPreAddress;
            spouse.FatherPerAddress = model.FatherPerAddress;
            spouse.FatherOccupationId = model.FatherOccupationId;
            spouse.FatherOtherInfo = model.FatherOtherInfo;

            spouse.IsMotherDead = model.IsMotherDead;
            spouse.MotherName = model.MotherName;
            spouse.MotherNameBan = model.MotherNameBan;
            spouse.MotherPreAddress = model.MotherPreAddress;
            spouse.MotherPerAddress = model.MotherPerAddress;
            spouse.MotherOccupationId = model.MotherOccupationId;
            spouse.MotherOtherInfo = model.MotherOtherInfo;

            spouse.IsArmedForceExp = model.IsArmedForceExp;
            spouse.RankCategoryId = model.RankCategoryId;
            spouse.PNo = model.PNo;
            await spouseRepository.SaveAsync(spouse);
            if(spouse.RelationType == 1){
                EmployeeGeneral employeeGeneral = await employeeGeneralRepository.FindOneAsync(x => x.EmployeeId == spouse.EmployeeId);
                employeeGeneral.MarriageDate = spouse.MarriageDate;
                await employeeGeneralRepository.SaveAsync(employeeGeneral);
            }



            return model;
        }


        public async Task<bool> DeleteSpouse(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Spouse spouse = await spouseRepository.FindOneAsync(x => x.SpouseId == id);
            if (spouse == null)
            {
                throw new InfinityNotFoundException("Spouse not found");
            }
            else
            {
                return await spouseRepository.DeleteAsync(spouse);
            }
        }


        public List<SelectModel> GetCurrentStatusSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(CurrentStatus)).Cast<CurrentStatus>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        public List<SelectModel> GetRelationTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(RelationType)).Cast<RelationType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

       
        public async Task<List<SelectModel>> GetSpouseSelectModels(int employeeId)
        {
            ICollection<Spouse> shipCategories = await spouseRepository.FilterAsync(x => x.IsActive && x.EmployeeId==employeeId);
            return shipCategories.Select(x => new SelectModel()
            {
                Text = String.Format("{0}'s Child",x.BNameEng),
                Value = x.SpouseId
            }).ToList();
        }

        public async Task<SpouseModel> UpdateSpouse(SpouseModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Spouse data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Spouse spouse = ObjectConverter<SpouseModel, Spouse>.Convert(model);

            spouse = await spouseRepository.FindOneAsync(x => x.SpouseId == model.SpouseId);
            if (spouse == null)
            {
                throw new InfinityNotFoundException("Spouse not found !");
            }

            if (model.FileName != null)
            {
                spouse.FileName = model.FileName;
            }

            if (model.GenFormFileName != null)
            {
                spouse.GenFormFileName = model.GenFormFileName;
            }
            spouse.ModifiedDate = DateTime.Now;
            spouse.ModifiedBy = userId;
            await spouseRepository.SaveAsync(spouse);
            model.SpouseId = spouse.SpouseId;
            return model;
        }
    }
}