
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
    public class EmployeeOprService : IEmployeeOprService
    {

        private readonly IBnoisRepository<EmployeeOpr> employeeOprRepository;
        public EmployeeOprService(IBnoisRepository<EmployeeOpr> employeeOprRepository)
        {

            this.employeeOprRepository = employeeOprRepository;
        }


        //public List<EmployeeOprModel> GetEmployeeOprs(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<EmployeeOpr> oprAptSuitabilities = employeeOprRepository.FilterWithInclude(x => x.IsActive && (x.Employee.PNo == (qs) || x.Employee.FullNameEng.Contains(qs) || x.Rank.ShortName==(qs) || String.IsNullOrEmpty(qs)), "Employee", "Rank", "OprOccasion","Office", "Office1", "OfficeAppointment");
        //    total = oprAptSuitabilities.Count();
        //    oprAptSuitabilities = oprAptSuitabilities.OrderByDescending(x => x.Id).Skip((pn - 1) * ps).Take(ps);
        //    List<EmployeeOprModel> models = ObjectConverter<EmployeeOpr, EmployeeOprModel>.ConvertList(oprAptSuitabilities.ToList()).ToList();
        //    return models;
        //}


        public async Task<EmployeeOprModel> GetEmployeeOpr(int id)
        {
            if (id <= 0)
            {
                return new EmployeeOprModel();
            }
            EmployeeOpr employeeOpr = await employeeOprRepository.FindOneAsync(x => x.Id == id, new List<string> { "Employee", "Office", "Office1", "Employee.Rank", "Employee.Batch" });
            if (employeeOpr == null)
            {
                throw new InfinityNotFoundException("OPR Entry not found");
            }
            EmployeeOprModel model = ObjectConverter<EmployeeOpr, EmployeeOprModel>.Convert(employeeOpr);
            return model;
        }

        public async Task<EmployeeOprModel> SaveEmployeeOpr(int id, EmployeeOprModel model)
        {

            //bool isExist = employeeOprRepository.Exists(x =>x.OccasionId == model.OccasionId && x.EmployeeId == x.EmployeeId && x.OprFromDate==model.OprFromDate && x.OprToDate == model.OprToDate && x.Id != model.Id);
            //if (isExist)
            //{
            //    throw new InfinityInvalidDataException("Data already exists !");
            //}

            model.Employee = null;
            if (model == null)
            {
                throw new InfinityArgumentMissingException("OPR Entry data missing");
            }


            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            EmployeeOpr employeeOpr = ObjectConverter<EmployeeOprModel, EmployeeOpr>.Convert(model);

            if (id > 0)
            {
                employeeOpr = await employeeOprRepository.FindOneAsync(x => x.Id == id);

                if (employeeOpr == null)
                {
                    throw new InfinityNotFoundException("OPR Entry not found !");
                }

                employeeOpr.ModifiedDate = DateTime.Now;
                employeeOpr.ModifiedBy = userId;

            }
            else
            {

                employeeOpr.IsActive = true;
                employeeOpr.CreatedDate = DateTime.Now;
                employeeOpr.CreatedBy = userId;
            }
            employeeOpr.EmployeeId = model.EmployeeId;
            employeeOpr.OfficeId = model.OfficeId;
            employeeOpr.OprRankId = model.OprRankId;
            employeeOpr.OccasionId = model.OccasionId;
            employeeOpr.RecomandationTypeId = model.RecomandationTypeId;
            employeeOpr.OprGrade = model.OprGrade;
            employeeOpr.GradingStatus = model.GradingStatus;
            employeeOpr.OprFromDate = model.OprFromDate ?? employeeOpr.OprFromDate;
            employeeOpr.OprToDate = model.OprToDate ?? employeeOpr.OprToDate;
            employeeOpr.FileName = model.FileName;
            employeeOpr.IsAdverseRemark = model.IsAdverseRemark;
            employeeOpr.AdverseRemark = model.AdverseRemark;
            employeeOpr.Section2= model.Section2;
            employeeOpr.Section3 = model.Section3;
            employeeOpr.Section4 = model.Section4;
            employeeOpr.Overweight = model.Overweight;
            employeeOpr.Underweight = model.Underweight;
            employeeOpr.ApptRecom = model.ApptRecom;
            employeeOpr.BOffCId = model.BOffCId;
            employeeOpr.AppoinmentId = model.AppoinmentId;
            employeeOpr.OtherAppointment = model.OtherAppointment;
            await employeeOprRepository.SaveAsync(employeeOpr);
            model.Id = employeeOpr.Id;

            return model;
        }

        public async Task<bool> DeleteEmployeeOpr(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EmployeeOpr employeeOpr = await employeeOprRepository.FindOneAsync(x => x.Id == id);
            if (employeeOpr == null)
            {

                throw new InfinityNotFoundException("OPR Entry not found");
            }
            else
            {



                return await employeeOprRepository.DeleteAsync(employeeOpr); ;
            }
        }

        public  async Task<EmployeeOprModel> UpdateEmployeeOpr(EmployeeOprModel model)
        {
            string userId = Configuration.ConfigurationResolver.Get().LoggedInUser.UserId;
            EmployeeOpr employeeOpr = ObjectConverter<EmployeeOprModel, EmployeeOpr>.Convert(model);

            employeeOpr = await employeeOprRepository.FindOneAsync(x => x.Id == model.Id);

            if (employeeOpr == null)
            {
                throw new InfinityNotFoundException("OPR Entry not found !");
            }
            if (model.FileName!=null)
            {
                employeeOpr.FileName = model.FileName;
            }
            if (model.ImageSec2 != null)
            {
                employeeOpr.ImageSec2 = model.ImageSec2;
            }
            if (model.ImageSec4 != null)
            {
                employeeOpr.ImageSec4 = model.ImageSec4;
            }
            employeeOpr.ModifiedDate = DateTime.Now;
            employeeOpr.ModifiedBy = userId;
            await employeeOprRepository.SaveAsync(employeeOpr);
            model =  ObjectConverter<EmployeeOpr, EmployeeOprModel>.Convert(employeeOpr);
            return model;
        }

        public List<EmployeeOprModel> GetEmployeeOprs(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}

