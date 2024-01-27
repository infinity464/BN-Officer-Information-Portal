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
    public class EmpRunMissingService : IEmpRunMissingService
    {

        private readonly IBnoisRepository<EmpRunMissing> empRunMissingRepository;
        private readonly IBnoisRepository<Employee> employeeRepository;
        private readonly IBnoisRepository<EmployeeStatus> employeeStatusRepository;

        public EmpRunMissingService(IBnoisRepository<EmpRunMissing> empRunMissingRepository,
            IBnoisRepository<Employee> employeeRepository,
            IBnoisRepository<EmployeeStatus> employeeStatusRepository)
        {
            this.empRunMissingRepository = empRunMissingRepository;
            this.employeeRepository = employeeRepository;
            this.employeeStatusRepository = employeeStatusRepository;
        }


        //public List<EmpRunMissingModel> GetEmpRunMissings(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<EmpRunMissing> empRunMissings = empRunMissingRepository.FilterWithInclude(x => x.IsActive && x.Type != (int)OfficerCurrentStatus.Back_TO_Unit && (x.Employee.PNo == (qs) || x.Employee.FullNameEng.Contains(qs) || String.IsNullOrEmpty(qs)), "Employee");

        //    total = empRunMissings.Count();
        //    empRunMissings = empRunMissings.OrderByDescending(x => x.EmpRunMissingId).Skip((pn - 1) * ps).Take(ps);
        //    List<EmpRunMissingModel> models = ObjectConverter<EmpRunMissing, EmpRunMissingModel>.ConvertList(empRunMissings.ToList()).ToList();
        //    models = models.Select(x =>
        //    {
        //        x.TypeName = Enum.GetName(typeof(OfficerCurrentStatus), x.Type);
        //        return x;
        //    }).ToList();

        //    return models;

        //}

        public async Task<EmpRunMissingModel> GetEmpRunMissing(int id)
        {
            if (id <= 0)
            {
                return new EmpRunMissingModel();
            }
            EmpRunMissing empRunMissing = await empRunMissingRepository.FindOneAsync(x => x.EmpRunMissingId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch" });
            if (empRunMissing == null)
            {
                throw new InfinityNotFoundException(" Officer Run Or Missing not found");
            }
            EmpRunMissingModel model = ObjectConverter<EmpRunMissing, EmpRunMissingModel>.Convert(empRunMissing);
            return model;
        }

        public async Task<EmpRunMissingModel> SaveEmpRunMissing(int id, EmpRunMissingModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Officer's Run Or Missing data not found");
            }


            bool isExist = await empRunMissingRepository.ExistsAsync(x => x.EmployeeId == model.EmployeeId && x.Date == model.Date && x.EmpRunMissingId != model.EmpRunMissingId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Same date entry not allowed. !");
            }

            EmployeeStatus employeeStatus = await employeeStatusRepository.FindOneAsync(x => x.SLCode == model.Type);

            if (employeeStatus == null)
            {
                throw new InfinityNotFoundException("Officer's Status Not Found!");
            }

            Employee employee = await employeeRepository.FindOneAsync(x => x.EmployeeId == model.EmployeeId);
            if (employee == null)
            {
                throw new InfinityInvalidDataException("Officer not exist!");
            }



            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            EmpRunMissing empRunMissing = ObjectConverter<EmpRunMissingModel, EmpRunMissing>.Convert(model);
            if (id > 0)
            {
                empRunMissing = await empRunMissingRepository.FindOneAsync(x => x.EmpRunMissingId == id);
                if (empRunMissing == null)
                {
                    throw new InfinityNotFoundException("Officer's Run Or Missing data not found !");
                }

                empRunMissing.ModifiedDate = DateTime.Now;
                empRunMissing.ModifiedBy = userId;
            }
            else
            {
                if (employee.EmployeeStatusId == employeeStatus.GCode && employee.SLCode == employeeStatus.SLCode)
                {
                    throw new InfinityInvalidDataException("Officer's Status Aleady updated !");
                }
                empRunMissing.IsActive = true;
                empRunMissing.CreatedDate = DateTime.Now;
                empRunMissing.CreatedBy = userId;
            }
            empRunMissing.EmployeeId = model.EmployeeId;
            empRunMissing.Type = model.Type;
            empRunMissing.Remarks = model.Remarks;
            empRunMissing.Date = model.Date;
            //empRunMissing.Employee = null;

            await empRunMissingRepository.SaveAsync(empRunMissing);
            model.EmpRunMissingId = empRunMissing.EmpRunMissingId;
            if (model.IsBackLog)
            {
                return model;
            }

            #region -----Employee is updating when Run/Missing execution is successful----
            employee.EmployeeStatusId = employeeStatus.GCode;
            employee.SLCode = employeeStatus.SLCode;
            await employeeRepository.SaveAsync(employee);
            #endregion

            return model;
        }

        public async Task<bool> DeleteEmpRunMissing(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EmpRunMissing empRunMissing = await empRunMissingRepository.FindOneAsync(x => x.EmpRunMissingId == id);
            if (empRunMissing == null)
            {
                throw new InfinityNotFoundException("Officer's Run Or Missing data not found");
            }
            else
            {
                return await empRunMissingRepository.DeleteAsync(empRunMissing);
            }
        }

        public List<SelectModel> GetStatusTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(OfficerCurrentStatus)).Cast<OfficerCurrentStatus>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }
        //----Employee Back to Unit-------
        //public List<EmpRunMissingModel> GetBackToUnits(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<EmpRunMissing> empRunMissings = empRunMissingRepository.FilterWithInclude(x => x.IsActive && x.Type == (int)OfficerCurrentStatus.Back_TO_Unit && (x.Employee.PNo == (qs) || x.Employee.FullNameEng.Contains(qs) || String.IsNullOrEmpty(qs)), "Employee");

        //    total = empRunMissings.Count();
        //    empRunMissings = empRunMissings.OrderByDescending(x => x.EmpRunMissingId).Skip((pn - 1) * ps).Take(ps);
        //    List<EmpRunMissingModel> models = ObjectConverter<EmpRunMissing, EmpRunMissingModel>.ConvertList(empRunMissings.ToList()).ToList();
        //    models = models.Select(x =>
        //    {
        //        x.TypeName = Enum.GetName(typeof(OfficerCurrentStatus), x.Type);
        //        return x;
        //    }).ToList();

        //    return models;
        //}

        public async Task<EmpRunMissingModel> SaveEmpBackToUnit(int id, EmpRunMissingModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Officer Back to Unit data missing !");
            }
            Employee employee = await employeeRepository.FindOneAsync(x => x.EmployeeId == model.Employee.EmployeeId);
            if (employee == null)
            {
                throw new InfinityArgumentMissingException("Officer not found!");
            }

//            EmployeeStatus employeeStatus = await employeeStatusRepository.FindOneAsync(x => x.EmployeeStatusId == employee.SLCode);
//            if (employeeStatus == null)
//            {
//                throw new InfinityNotFoundException("Officer Back to Unit data missing !");
//            }
            bool isExist = empRunMissingRepository.Exists(x => x.EmployeeId == model.Employee.EmployeeId && x.EmpRunMissingId != model.EmpRunMissingId);

            if (isExist)
            {
                throw new InfinityInvalidDataException("Officer already Back to Unit !");
            }
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            EmpRunMissing empRunMissing = ObjectConverter<EmpRunMissingModel, EmpRunMissing>.Convert(model);
            if (id > 0)
            {
                empRunMissing = await empRunMissingRepository.FindOneAsync(x => x.EmpRunMissingId == id);
                if (empRunMissing == null)
                {
                    throw new InfinityNotFoundException("Officer's Back to Unit data not found !");
                }

                empRunMissing.ModifiedDate = DateTime.Now;
                empRunMissing.ModifiedBy = userId;
            }
            else
            {
                empRunMissing.IsActive = true;
                empRunMissing.CreatedDate = DateTime.Now;
                empRunMissing.CreatedBy = userId;
            }
            empRunMissing.EmployeeId = model.Employee.EmployeeId;
            empRunMissing.Type = (int)OfficerCurrentStatus.Back_TO_Unit;
            empRunMissing.Remarks = model.Remarks;
            empRunMissing.Date = model.Date;
            //empRunMissing.Employee = null;

            await empRunMissingRepository.SaveAsync(empRunMissing);
            model.EmpRunMissingId = empRunMissing.EmpRunMissingId;

            #region -----Employee is updating when Run/Missing execution is successful----
            employee.EmployeeStatusId = (int)OfficerCurrentStatus.Back_TO_Unit;
            employee.SLCode = (int)OfficerCurrentStatus.Back_TO_Unit;
            await employeeRepository.SaveAsync(employee);
            #endregion

            return model;
        }

        public List<EmpRunMissingModel> GetEmpRunMissings(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }

        public List<EmpRunMissingModel> GetBackToUnits(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}