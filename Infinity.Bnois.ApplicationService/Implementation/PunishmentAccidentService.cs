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
    public class PunishmentAccidentService : IPunishmentAccidentService
    {
        private readonly IBnoisRepository<PunishmentAccident> punishmentAccidentRepository;
        private readonly IBnoisRepository<PtDeductPunishment> ptDeductPunishmentRepository;
        private readonly IBnoisRepository<PunishmentCategory> punishmentCategoryRepository;
        private readonly IBnoisRepository<EmployeeGeneral> employeeGeneralRepository;
        public PunishmentAccidentService(IBnoisRepository<EmployeeGeneral> employeeGeneralRepository, IBnoisRepository<PunishmentCategory> punishmentCategoryRepository, IBnoisRepository<PunishmentAccident> punishmentAccidentRepository, IBnoisRepository<PtDeductPunishment> ptDeductPunishmentRepository)
        {
            this.punishmentAccidentRepository = punishmentAccidentRepository;
            this.ptDeductPunishmentRepository = ptDeductPunishmentRepository;
            this.punishmentCategoryRepository = punishmentCategoryRepository;
            this.employeeGeneralRepository = employeeGeneralRepository;
        }

        //public List<PunishmentAccidentModel> GetPunishmentAccidents(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<PunishmentAccident> punishmentAccidents = punishmentAccidentRepository.FilterWithInclude(x => x.IsActive
        //        && (x.Employee.PNo == (qs) || x.Employee.FullNameEng.Contains(qs) || String.IsNullOrEmpty(qs)), "Employee", "PunishmentCategory", "PunishmentSubCategory", "PunishmentNature", "Rank");
        //    total = punishmentAccidents.Count();
        //    punishmentAccidents = punishmentAccidents.OrderByDescending(x => x.PunishmentAccidentId).Skip((pn - 1) * ps).Take(ps);
        //    List<PunishmentAccidentModel> models = ObjectConverter<PunishmentAccident, PunishmentAccidentModel>.ConvertList(punishmentAccidents.ToList()).ToList();
        //    models = models.Select(x =>
        //    {
        //        x.TypeName = Enum.GetName(typeof(PunishmentAccidentType), x.Type);
        //        return x;
        //    }).ToList();
        //    return models;
        //}

        public async Task<PunishmentAccidentModel> GetPunishmentAccident(int id)
        {
            if (id <= 0)
            {
                return new PunishmentAccidentModel();
            }
            PunishmentAccident punishmentAccident = await punishmentAccidentRepository.FindOneAsync(x => x.PunishmentAccidentId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch" });
            if (punishmentAccident == null)
            {
                throw new InfinityNotFoundException("Punishment Accident not found");
            }
            PunishmentAccidentModel model = ObjectConverter<PunishmentAccident, PunishmentAccidentModel>.Convert(punishmentAccident);
            return model;
        }


        //public async Task<PunishmentAccidentModel> SavePunishmentAccident(int id, PunishmentAccidentModel model)
        //{
        //    if (model == null)
        //    {
        //        throw new InfinityArgumentMissingException("PunishmentAccident  data missing");
        //    }

        //    string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
        //    PunishmentAccident punishmentAccident = ObjectConverter<PunishmentAccidentModel, PunishmentAccident>.Convert(model);
        //    if (id > 0)
        //    {
        //        punishmentAccident = await punishmentAccidentRepository.FindOneAsync(x => x.PunishmentAccidentId == id);
        //        if (punishmentAccident == null)
        //        {
        //            throw new InfinityNotFoundException("Punishment Accident not found !");
        //        }

        //        punishmentAccident.ModifiedDate = DateTime.Now;
        //        punishmentAccident.ModifiedBy = userId;
        //    }
        //    else
        //    {
        //        punishmentAccident.IsActive = true;
        //        punishmentAccident.CreatedDate = DateTime.Now;
        //        punishmentAccident.CreatedBy = userId;
        //    }

        //    //if (punishmentAccident.Type == 1)
        //    //{
        //    //    PtDeductPunishment ptDeductPunishment = await ptDeductPunishmentRepository.FindOneAsync(x => x.PunishmentSubCategoryId == model.PunishmentSubCategoryId && x.PunishmentNatureId == model.PunishmentNatureId, new List<string> { "PunishmentNature" });
        //    //    if (ptDeductPunishment != null)
        //    //    {
        //    //        if (ptDeductPunishment.PunishmentNature.ShortName == null)
        //    //            ptDeductPunishment.PunishmentNature.ShortName = String.Empty;

        //    //        if (ptDeductPunishment.PunishmentNature.ShortName.Equals("NO"))
        //    //        {
        //    //            punishmentAccident.PunishmentValue = ptDeductPunishment.PunishmentValue;
        //    //            punishmentAccident.SkipYear = ptDeductPunishment.SkipYear;
        //    //            punishmentAccident.DeductPercentage = ptDeductPunishment.DeductPercentage;
        //    //            punishmentAccident.DeductYear = ptDeductPunishment.DeductionYear;
        //    //            punishmentAccident.PtAfterDeduct = ptDeductPunishment.PunishmentValue;
        //    //        }
        //    //    }

        //    //    PunishmentCategory punishmentCategory = await punishmentCategoryRepository.FindOneAsync(x => x.PunishmentCategoryId == model.PunishmentCategoryId);
        //    //    if (punishmentCategory != null)
        //    //    {
        //    //        if(punishmentCategory.ShortName == null)
        //    //            punishmentCategory.ShortName=String.Empty;
                 
        //    //        if (punishmentCategory.ShortName.Equals("FOS"))
        //    //        {
        //    //            EmployeeGeneral employeeGeneral = await employeeGeneralRepository.FindOneAsync(x => x.EmployeeId == model.EmployeeId);
        //    //            if (employeeGeneral != null)
        //    //            {
        //    //                if (employeeGeneral.LieutenantDate != null)
        //    //                {
        //    //                    employeeGeneral.PunishmentLtDate = employeeGeneral.LieutenantDate.Value.AddMonths(model.DurationMonths ?? 0);
        //    //                    employeeGeneral.PunishmentLtDate = employeeGeneral.PunishmentLtDate.Value.AddDays(model.DurationDays ?? 0);
        //    //                    await employeeGeneralRepository.SaveAsync(employeeGeneral);
        //    //                }

        //    //            }
        //    //        }
        //    //    }

        //    //}

        //    punishmentAccident.EmployeeId = model.EmployeeId;
        //    punishmentAccident.IsBackLog = model.IsBackLog;
        //    punishmentAccident.RankId = model.Employee.RankId;
        //    punishmentAccident.TransferId = model.Employee.TransferId;

        //    if (model.IsBackLog)
        //    {

        //        punishmentAccident.RankId = model.RankId;
        //        punishmentAccident.TransferId = model.TransferId;
        //    }


        //    punishmentAccident.PunishmentCategoryId = model.PunishmentCategoryId;
        //    punishmentAccident.PunishmentSubCategoryId = model.PunishmentSubCategoryId;
        //    punishmentAccident.PunishmentNatureId = model.PunishmentNatureId;
        //    punishmentAccident.AccedentType = model.AccedentType;
        //    punishmentAccident.Type = model.Type;
        //    punishmentAccident.DurationMonths = model.DurationMonths;
        //    punishmentAccident.DurationDays = model.DurationDays;
        //    punishmentAccident.Date =model.Date ?? punishmentAccident.Date;
        //    punishmentAccident.PunishmentType = model.PunishmentType;
        //    punishmentAccident.Remarks = model.Remarks;
        //    punishmentAccident.Reason = model.Reason;
        //    punishmentAccident.FileName = model.FileName;
        //    punishmentAccident.Employee = null;
        //    await punishmentAccidentRepository.SaveAsync(punishmentAccident);
        //    model.PunishmentAccidentId = punishmentAccident.PunishmentAccidentId;
        //    return model;
        //}


        public async Task<bool> DeletePunishmentAccident(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            PunishmentAccident punishmentAccident = await punishmentAccidentRepository.FindOneAsync(x => x.PunishmentAccidentId == id);
            if (punishmentAccident == null)
            {
                throw new InfinityNotFoundException("Punishment Accident not found");
            }
            else
            {
                return await punishmentAccidentRepository.DeleteAsync(punishmentAccident);
            }
        }

        public List<SelectModel> GetAccidentTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(AccidentType)).Cast<AccidentType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        public List<SelectModel> GetPunishmentAccidentTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(PunishmentAccidentType)).Cast<PunishmentAccidentType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        public List<PunishmentAccidentModel> GetPunishmentAccidents(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }

        public Task<PunishmentAccidentModel> SavePunishmentAccident(int id, PunishmentAccidentModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExecutePunishmentProcess()
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> ExecutePunishmentProcess()
        //{
        //    int result = 0;
        //    IQueryable<PunishmentAccident> punishments = punishmentAccidentRepository.FilterWithInclude(p => p.Type == 1 && !p.IsProcessed && p.PunishmentNature.ShortName.Equals("NO"), "PunishmentNature");
        //    foreach (var item in punishments.ToList())
        //    {
        //        var totalYears = (DateTime.Today - item.Date).TotalDays / 365.2425;
        //        if (totalYears <= item.SkipYear)
        //        {
        //            item.PtAfterDeduct = item.PunishmentValue;
        //        }
        //        else
        //        {
        //            var years = totalYears - item.SkipYear;

        //            for (int i = 1; i <= item.DeductYear; i++)
        //            {
        //                if (years <= i)
        //                {
        //                    item.YearCount = i;
        //                    break;
        //                }
        //            }

        //            double r = item.DeductPercentage / 100;
        //            double n = Convert.ToDouble(item.YearCount);
        //            double point = item.PunishmentValue * Math.Pow((1 - r), n);
        //            item.PtAfterDeduct = point;

        //        }

        //        result = await punishmentAccidentRepository.SaveAsync(item);
        //    }


        //    return result > 0;
        //}
    }
}
