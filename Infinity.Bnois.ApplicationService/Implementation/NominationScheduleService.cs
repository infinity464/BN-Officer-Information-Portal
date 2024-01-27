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
    public class NominationScheduleService : INominationScheduleService
    {
        private readonly IBnoisRepository<NominationSchedule> nominationScheduleRepository;

        public NominationScheduleService(IBnoisRepository<NominationSchedule> nominationScheduleRepository)
        {
            this.nominationScheduleRepository = nominationScheduleRepository;

        }

        //public List<NominationScheduleModel> GetNominationSchedules(int ps, int pn,string qs, int type, out int total)
        //{
        //    IQueryable<NominationSchedule> nominationSchedules = nominationScheduleRepository.FilterWithInclude(x => x.IsActive && x.NominationScheduleType==type
        //        && (x.TitleName.Contains(qs) || x.Purpose.Contains(qs) || x.VisitCategory.Name.Contains(qs) || x.VisitSubCategory.Name.Contains(qs) ||
        //        (x.Country.FullName.Contains(qs) || String.IsNullOrEmpty(qs)) ), "Country","VisitCategory","VisitSubCategory");
        //    total = nominationSchedules.Count();
	       // nominationSchedules = nominationSchedules.OrderByDescending(x => x.NominationScheduleId).Skip((pn - 1) * ps).Take(ps);
        //    List<NominationScheduleModel> models = ObjectConverter<NominationSchedule, NominationScheduleModel>.ConvertList(nominationSchedules.ToList()).ToList();
        //    return models;
        //}

        public async Task<NominationScheduleModel> GetNominationSchedule(int id)
        {
            if (id <= 0)
            {
                return new NominationScheduleModel();
            }
            NominationSchedule nominationSchedule = await nominationScheduleRepository.FindOneAsync(x => x.NominationScheduleId == id);
            if (nominationSchedule == null)
            {
                throw new InfinityNotFoundException("Nomination Schedule not found");
            }
            NominationScheduleModel model = ObjectConverter<NominationSchedule, NominationScheduleModel>.Convert(nominationSchedule);
            return model;
        }

        public async Task<NominationScheduleModel> SaveNominationSchedule(int id, NominationScheduleModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Nomination Schedule  data missing");
            }
            bool isExistData;


            switch (model.NominationScheduleType)
            {
                //case 1:
                //    isExistData = nominationScheduleRepository.Exists(x => x.CountryId == model.CountryId && x.TitleName == model.TitleName && x.FromDate == model.FromDate && x.ToDate == model.ToDate && x.NominationScheduleId != id);
                //    if (isExistData)
                //    {
                //        throw new InfinityInvalidDataException("Data already exists !");
                //    }
                //    break;
                case 2:
                    isExistData = nominationScheduleRepository.Exists(x => x.CountryId == model.CountryId  && x.FromDate == model.FromDate && x.ToDate == model.ToDate && x.VisitSubCategoryId == model.VisitSubCategoryId && x.NominationScheduleId != id);
                    if (isExistData)
                    {
                        throw new InfinityInvalidDataException("Data already exists !");
                    }
                    break;
                case 3:
                    isExistData = nominationScheduleRepository.Exists(x => x.FromDate == model.FromDate && x.ToDate == model.ToDate && x.Purpose.Trim() == model.Purpose.Trim() && x.NominationScheduleId != id);
                    if (isExistData)
                    {
                        throw new InfinityInvalidDataException("Data already exists !");
                    }
                    break;
            }
            //if (model.NominationScheduleType != 3)
            //{
               
            //    bool isExistData = nominationScheduleRepository.Exists(x =>x.CountryId == model.CountryId  && x.TitleName == model.TitleName && x.FromDate == model.FromDate  && x.NominationScheduleId != id);
            //    if (isExistData)
            //    {
            //        throw new InfinityInvalidDataException("Data already exists !");
            //    }
            //}
           

            if (model.FromDate>model.ToDate)
            {
                throw new InfinityInvalidDataException("From Date is greater than To Date.!");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            NominationSchedule NominationSchedule = ObjectConverter<NominationScheduleModel, NominationSchedule>.Convert(model);
            if (id > 0)
            {
                NominationSchedule = await nominationScheduleRepository.FindOneAsync(x => x.NominationScheduleId == id);
                if (NominationSchedule == null)
                {
                    throw new InfinityNotFoundException("Nomination Schedule not found !");
                }
  

                NominationSchedule.ModifiedDate = DateTime.Now;
                NominationSchedule.ModifiedBy = userId;
            }
            else
            {
                NominationSchedule.IsActive = true;
                NominationSchedule.CreatedDate = DateTime.Now;
                NominationSchedule.CreatedBy = userId;
            
            }

            NominationSchedule.CountryId = model.CountryId;
            NominationSchedule.NominationScheduleType = model.NominationScheduleType;
            NominationSchedule.TitleName = model.TitleName;
            NominationSchedule.FromDate = model.FromDate;
            NominationSchedule.ToDate = model.ToDate;
            NominationSchedule.Purpose = model.Purpose;
            NominationSchedule.Location = model.Location;
            NominationSchedule.Remarks = model.Remarks;
            NominationSchedule.NumberOfPost = model.NumberOfPost;
            NominationSchedule.AssignPost = model.AssignPost;
            await nominationScheduleRepository.SaveAsync(NominationSchedule);
            model.NominationScheduleId = NominationSchedule.NominationScheduleId;
            return model;
        }


        public async Task<bool> DeleteNominationSchedule(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            NominationSchedule nominationSchedule = await nominationScheduleRepository.FindOneAsync(x => x.NominationScheduleId == id);
            if (nominationSchedule == null)
            {
                throw new InfinityNotFoundException("Nomination Schedule not found");
            }
            else
            {
                return await nominationScheduleRepository.DeleteAsync(nominationSchedule);
            }
        }

        public List<SelectModel> GetNominationScheduleTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(NominationScheduleType)).Cast<NominationScheduleType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        //public async Task<List<SelectModel>> GetMissionNominationScheduleSelectModels()
        //{
        //    ICollection<NominationSchedule> models = await nominationScheduleRepository.FilterWithInclude(x => x.IsActive && x.NominationScheduleType == 1 , "Country").ToListAsync();
        //    return models.OrderByDescending(x => x.FromDate).Select(x => new SelectModel() 
        //    {
        //        Text = String.Format("{0},{1},[{2} To{3}]", x.TitleName, x.Country.FullName, String.Format("{0:dd-MM-yyyy}", x.FromDate), String.Format("{0:dd-MM-yyyy}", x.ToDate)),
        //        Value = x.NominationScheduleId
        //    }).ToList();
        //}


        //public async Task<List<SelectModel>> GetForeignVisitNominationScheduleSelectModels()
        //{
        //    ICollection<NominationSchedule> models = await nominationScheduleRepository.FilterWithInclude(x => x.IsActive && x.NominationScheduleType==2 , "Country","VisitCategory","VisitSubCategory").ToListAsync();
        //    return models.OrderByDescending(x => x.FromDate).Select(x => new SelectModel()
        //    {
        //        Text = String.Format("{0}-{1}, {2} [{3} To {4}]", x.VisitCategory.Name,x.VisitSubCategory.Name, x.Country.FullName, String.Format("{0:dd-MM-yyyy}", x.FromDate), String.Format("{0:dd-MM-yyyy}", x.ToDate)),
        //        Value = x.NominationScheduleId
        //    }).ToList();
        //}

        public async Task<List<SelectModel>> GetOtherNominationScheduleSelectModels()
        {
            ICollection<NominationSchedule> models = await nominationScheduleRepository.FilterWithInclude(x => x.IsActive && x.NominationScheduleType == 3).ToListAsync();
            return models.OrderByDescending(x => x.FromDate).Select(x => new SelectModel()
            {
                Text = String.Format("{0} [{1} To {2}] ",  x.Purpose, String.Format("{0:dd-MM-yyyy}", x.FromDate), String.Format("{0:dd-MM-yyyy}", x.ToDate)),
                Value = x.NominationScheduleId
            }).ToList();
        }

        public List<NominationScheduleModel> GetNominationSchedules(int ps, int pn, string qs, int type, out int total)
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectModel>> GetMissionNominationScheduleSelectModels()
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectModel>> GetForeignVisitNominationScheduleSelectModels()
        {
            throw new NotImplementedException();
        }
    }
}
