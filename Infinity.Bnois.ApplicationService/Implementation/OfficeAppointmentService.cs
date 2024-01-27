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
    public class OfficeAppointmentService : IOfficeAppointmentService
    {
        private readonly IBnoisRepository<OfficeAppointment> officeAppointmentRepository;
        private readonly IBnoisRepository<OfficeAppRank> officeAppRankRepository;
        private readonly IBnoisRepository<OfficeAppBranch> officeAppBranchRepository;
        BnoisDbContext db = new BnoisDbContext();
        public OfficeAppointmentService(IBnoisRepository<OfficeAppRank> officeAppRankRepository, IBnoisRepository<OfficeAppBranch> officeAppBranchRepository, IBnoisRepository<OfficeAppointment> officeAppointmentRepository)
        {
            this.officeAppointmentRepository = officeAppointmentRepository;
            this.officeAppRankRepository = officeAppRankRepository;
            this.officeAppBranchRepository = officeAppBranchRepository;
        }

        //public List<OfficeAppointmentModel> GetOfficeAppointments(int ps, int pn, string qs, int officeId,int type, out int total)
        //{
        //    IQueryable<OfficeAppointment> officeAppointments = officeAppointmentRepository.FilterWithInclude(x => x.IsActive && x.OfficeId==officeId && x.AppointmentType==type
        //        && ((x.Office.ShortName.Contains(qs) || String.IsNullOrEmpty(qs)) || (x.AptNat.ANatShnm.Contains(qs) || String.IsNullOrEmpty(qs)) ||
        //        (x.AptCat.ACatShNm.Contains(qs) || String.IsNullOrEmpty(qs))), "Office", "AptNat", "AptCat");
        //    total = officeAppointments.Count();
        //    officeAppointments = officeAppointments.OrderBy(x => x.AptNat.ANatNm).Skip((pn - 1) * ps).Take(ps);
        //    List<OfficeAppointmentModel> models = ObjectConverter<OfficeAppointment, OfficeAppointmentModel>.ConvertList(officeAppointments.ToList()).ToList();
        //    return models;
        //}


        public async Task<List<SelectModel>> GetOfficeAppointmentsByOfficeId(int officeId)
        {
            ICollection<OfficeAppointment> officeAppointments = await officeAppointmentRepository.FilterAsync(x => x.IsActive && x.OfficeId == officeId && x.AppointmentType==1);
            List<SelectModel> selectModels = officeAppointments.OrderBy(x => x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.OffAppId
            }).ToList();
            return selectModels;
        }

        public async Task<List<SelectModel>> GetOfficeAppointmentSelectModels()
        {
            ICollection<OfficeAppointment> officeAppointments = await officeAppointmentRepository.FilterAsync(x => x.IsActive);
            List<SelectModel> selectModels = officeAppointments.OrderBy(x=>x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.OffAppId
            }).ToList();
            return selectModels;
        }


        public async Task<List<SelectModel>> GetOfficeAdditionalAppointmentsByOfficeId(int officeId)
        {
            ICollection<OfficeAppointment> officeAppointments = await officeAppointmentRepository.FilterAsync(x => x.IsActive && x.OfficeId == officeId && x.AppointmentType == 2);
            List<SelectModel> selectModels = officeAppointments.OrderBy(x => x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.OffAppId
            }).ToList();
            return selectModels;
        }
        //public async Task<List<SelectModel>> GetAppointmentByShipType(int shipType)
        //{
        //    ICollection<OfficeAppointment> officeAppointments = await officeAppointmentRepository.FilterAsync(x => x.IsActive && x.Office.ShipType == shipType);
        //    List<SelectModel> selectModels = officeAppointments.OrderBy(x => x.Name).Select(x => new SelectModel
        //    {
        //        Text = x.Name,
        //        Value = x.OffAppId
        //    }).ToList();
        //    return selectModels;
        //}

        //public async Task<List<SelectModel>> GetAppointmentByOrganizationPattern(int officeId)
        //{
        //    ICollection<OfficeAppointment> officeAppointments = await officeAppointmentRepository.FilterAsync(x => x.IsActive && x.Office.ParentId == officeId);
        //    List<SelectModel> selectModels = officeAppointments.OrderBy(x => x.Name).Select(x => new SelectModel
        //    {
        //        Text = x.Name,
        //        Value = x.OffAppId
        //    }).ToList();
        //    return selectModels;
        //}
        public List<SelectModel> GetCommandingOfficerListByOfficeId()
        {
            string UserId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string employeePNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + UserId + "'").SingleOrDefault();
            int employeeId = db.Database.SqlQuery<int>("select EmployeeId from Employee where PNo='" + employeePNo + "'").SingleOrDefault();
            int officeId = db.Database.SqlQuery<int>("select Top(1) ISNULL(AttachOfficeId, CurrentBornOfficeId) from Transfer where EmployeeId= (Select EmployeeId from Employee where PNo='" + employeePNo + "') and TransferMode = 1 and ToDate is null  order by FromDate desc").SingleOrDefault();
            bool IsOfficeHead = db.Database.SqlQuery<bool>("select dbo.fnIsOfficeHead("+ employeeId + ")").SingleOrDefault();
            bool IsOfficeSubHead = db.Database.SqlQuery<bool>("select dbo.fnIsOfficeSubHead(" + employeeId + ")").SingleOrDefault();
            bool IsAreaCommender = db.Database.SqlQuery<bool>("select dbo.fnIsAreaCommander(" + employeeId + ")").SingleOrDefault();
            bool IsNSOfficeHead = db.Database.SqlQuery<bool>("select dbo.fnIsNSOfficeHead(" + employeeId + ")").SingleOrDefault();
            int lastAppoinmentId = db.Database.SqlQuery<int>("select Top(1) AppointmentId from Transfer where EmployeeId=" + employeeId + " and TransferMode = 1 and ToDate is null order by FromDate desc").SingleOrDefault();
            List<OfficeAppointment> commandingOfficers = new List<OfficeAppointment>();
            if (IsOfficeHead && !IsAreaCommender && !IsNSOfficeHead)
            {
                int parentOffice = db.Database.SqlQuery<int>("select ParentId from Office Where OfficeId=" + officeId).SingleOrDefault();
                int OfficeHeadAppoinmentId = db.Database.SqlQuery<int>("select Top(1) OffAppId from OfficeAppointment where OfficeId="+ parentOffice + "and OfficeHead=1").SingleOrDefault();
                bool isValidAppoinment = db.Database.SqlQuery<bool>("select dbo.fnOfficeHeadAppointmentValidityCheck("+ OfficeHeadAppoinmentId+")").SingleOrDefault();

                if (isValidAppoinment)
                {
                    commandingOfficers = officeAppointmentRepository.Where(x => x.OfficeId == parentOffice && (x.OfficeHead == true || x.OrgCd == "1")).OrderBy(x => x.ShortName).ToList();
                }
                else
                {
                    parentOffice = db.Database.SqlQuery<int>("select ParentId from Office Where OfficeId=" + parentOffice).SingleOrDefault();
                    commandingOfficers = officeAppointmentRepository.Where(x => x.OfficeId == parentOffice && (x.OfficeHead == true || x.OrgCd == "1")).OrderBy(x => x.ShortName).ToList();

                }
            }
            else if (IsAreaCommender == true || IsNSOfficeHead==true)
            {
                commandingOfficers = officeAppointmentRepository.Where(x => x.OfficeId == 3 && x.OfficeHead == true ).OrderBy(x => x.ShortName).ToList();
            }
            else 
            {
                commandingOfficers = officeAppointmentRepository.Where(x => x.OfficeId == officeId && (x.OfficeHead == true || x.OrgCd == "1")).OrderBy(x => x.ShortName).ToList();
            }
            List<SelectModel> selectModels= commandingOfficers.Select(x => new SelectModel
            {
                Text = x.ShortName,
                Value = x.OffAppId
            }).ToList();
            return selectModels;
        }

        public async Task<OfficeAppointmentModel> GetOfficeAppointment(int id)
        {
            if (id <= 0)
            {
                return new OfficeAppointmentModel();
            }
            OfficeAppointment officeAppointment = await officeAppointmentRepository.FindOneAsync(x => x.OffAppId == id);

            if (officeAppointment == null)
            {
                throw new InfinityNotFoundException("Office Appointment not found");
            }
            int[] rankIds=  officeAppRankRepository.Where(x => x.OffAppId == id).Select(x => x.RankId).ToArray();
            int[] branchIds = officeAppBranchRepository.Where(x => x.OffAppId == id).Select(x => x.BranchId).ToArray();
            OfficeAppointmentModel model = ObjectConverter<OfficeAppointment, OfficeAppointmentModel>.Convert(officeAppointment);
            model.RankIds = rankIds;
            model.BranchIds = branchIds;
            return model;
        }

        //public async Task<OfficeAppointmentModel> SaveOfficeAppointment(int id, OfficeAppointmentModel model)
        //{
        //    if (model == null)
        //    {
        //        throw new InfinityArgumentMissingException("Office Appointment  data missing");
        //    }


        //    string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
        //    OfficeAppointment officeAppointment = ObjectConverter<OfficeAppointmentModel, OfficeAppointment>.Convert(model);
        //    if (id > 0)
        //    {
        //        officeAppointment = await officeAppointmentRepository.FindOneAsync(x => x.OffAppId == id);
        //        if (officeAppointment == null)
        //        {
        //            throw new InfinityNotFoundException("Office Appointment not found !");
        //        }
        //        ICollection<OfficeAppRank> trainingRanks = await officeAppRankRepository.FilterAsync(x => x.OffAppId == id);
        //        officeAppRankRepository.RemoveRange(trainingRanks);

        //        ICollection<OfficeAppBranch> trainingBranches = await officeAppBranchRepository.FilterAsync(x => x.OffAppId == id);
        //        officeAppBranchRepository.RemoveRange(trainingBranches);

        //        officeAppointment.ModifiedDate = DateTime.Now;
        //        officeAppointment.ModifiedBy = userId;
        //    }
        //    else
        //    {
        //        officeAppointment.IsActive = true;
        //        officeAppointment.CreatedDate = DateTime.Now;
        //        officeAppointment.CreatedBy = userId;

        //    }
        //    if (model.RankIds.Any())
        //    {
        //        officeAppointment.OfficeAppRank = model.RankIds.Select(x => new OfficeAppRank() { RankId = x }).ToList();
        //    }
        //    if (model.BranchIds.Any())
        //    {
        //        officeAppointment.OfficeAppBranch = model.BranchIds.Select(x => new OfficeAppBranch() { BranchId = x }).ToList();
        //    }
        //    officeAppointment.OfficeId = model.OfficeId;
        //    officeAppointment.AptCatId = model.AptCatId;
        //    officeAppointment.AptNatId = model.AptNatId;
        //    officeAppointment.Name = model.Name;
        //    officeAppointment.ShortName = model.ShortName;
        //    officeAppointment.NameBangla = model.NameBangla;
        //    officeAppointment.ShortNameBangla = model.ShortNameBangla;           
        //    officeAppointment.GovtApproved = model.GovtApproved;
        //    officeAppointment.HeadofDpt = model.HeadofDpt;
        //    officeAppointment.OfficeHead = model.OfficeHead;
        //    officeAppointment.IsInstrServiceCount = model.IsInstrServiceCount;
        //    officeAppointment.ParentOffAppId = model.ParentOffAppId;
        //    officeAppointment.AppointmentType = 1;
        //    await officeAppointmentRepository.SaveAsync(officeAppointment);
        //    model.OffAppId = officeAppointment.OffAppId;
        //    return model;
        //}


        public async Task<OfficeAppointmentModel> SaveOfficeAdditionalAppointment(int id, OfficeAppointmentModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Office Additional Appointment  data missing");
            }


            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            OfficeAppointment officeAppointment = ObjectConverter<OfficeAppointmentModel, OfficeAppointment>.Convert(model);
            if (id > 0)
            {
                officeAppointment = await officeAppointmentRepository.FindOneAsync(x => x.OffAppId == id);
                if (officeAppointment == null)
                {
                    throw new InfinityNotFoundException("Office Additional Appointment not found !");
                }

                officeAppointment.ModifiedDate = DateTime.Now;
                officeAppointment.ModifiedBy = userId;
            }
            else
            {
                officeAppointment.IsActive = true;
                officeAppointment.CreatedDate = DateTime.Now;
                officeAppointment.CreatedBy = userId;

            }
           
            officeAppointment.OfficeId = model.OfficeId;
            officeAppointment.AptCatId = model.AptCatId;
            officeAppointment.AptNatId = model.AptNatId;
            officeAppointment.Name = model.Name;
            officeAppointment.ShortName = model.ShortName;
			officeAppointment.IsAdditional= model.IsAdditional;
			officeAppointment.AppointmentType = 2;
            await officeAppointmentRepository.SaveAsync(officeAppointment);
            model.OffAppId = officeAppointment.OffAppId;
            return model;
        }


        public async Task<bool> DeleteOfficeAppointment(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            OfficeAppointment officeAppointment = await officeAppointmentRepository.FindOneAsync(x => x.OffAppId == id);
            if (officeAppointment == null)
            {
                throw new InfinityNotFoundException("Office Appointment not found");
            }
            else
            {
                officeAppRankRepository.Delete(x => x.OffAppId == officeAppointment.OffAppId);
                officeAppBranchRepository.Delete(x => x.OffAppId == officeAppointment.OffAppId);

                return await officeAppointmentRepository.DeleteAsync(officeAppointment);
            } 
        }


        public async Task<List<SelectModel>> GetParentAppointmentSelectModels(int officeId,int OffAppId)
        {
            ICollection<OfficeAppointment> officeAppointments = await officeAppointmentRepository.FilterAsync(x => x.IsActive && x.OfficeId == officeId && x.AppointmentType==1 && x.OffAppId != OffAppId);
            List<SelectModel> selectModels = officeAppointments.OrderBy(x=>x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.OffAppId
            }).ToList();
            return selectModels;
        }

        public List<OfficeAppointmentModel> GetOfficeAppointments(int ps, int pn, string qs, int officeId, int type, out int total)
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectModel>> GetAppointmentByShipType(int shipType)
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectModel>> GetAppointmentByOrganizationPattern(int officeId)
        {
            throw new NotImplementedException();
        }

        public Task<OfficeAppointmentModel> SaveOfficeAppointment(int id, OfficeAppointmentModel model)
        {
            throw new NotImplementedException();
        }
    }
}
