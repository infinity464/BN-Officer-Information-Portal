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
    public class EmployeeHajjDetaitService : IEmployeeHajjDetaitService
    {
        private readonly IBnoisRepository<EmployeeHajjDetail> _employeeHajjDetailRepository;
        private readonly IBnoisRepository<Employee> _employeeRepository;

        public EmployeeHajjDetaitService(IBnoisRepository<EmployeeHajjDetail> employeeHajjDetailRepository, IBnoisRepository<Employee> employeeRepository)
        {
            _employeeHajjDetailRepository = employeeHajjDetailRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> DeleteEmployeeHajjDetail(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EmployeeHajjDetail employeeHajjDetail = await _employeeHajjDetailRepository.FindOneAsync(x => x.EmployeeHajjDetailId == id);
            if (employeeHajjDetail == null)
            {
                throw new InfinityNotFoundException("Employee Hajj Detail not found");
            }
            else
            {
                return await _employeeHajjDetailRepository.DeleteAsync(employeeHajjDetail);
            }
        }

        public async Task<EmployeeHajjDetailModel> getEmployeeHajjDetail(int id)
        {
            if (id <= 0)
            {
                return new EmployeeHajjDetailModel();
            }
            EmployeeHajjDetail employeeHajjDetail = await _employeeHajjDetailRepository.FindOneAsync(x => x.EmployeeHajjDetailId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch" });
            if (employeeHajjDetail == null)
            {
                throw new InfinityNotFoundException("Employee Hajj Detail not found");
            }
            EmployeeHajjDetailModel model = ObjectConverter<EmployeeHajjDetail, EmployeeHajjDetailModel>.Convert(employeeHajjDetail);
            return model;
        }

        public List<EmployeeHajjDetailModel> GetEmployeeHajjDetails(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }

        //public List<EmployeeHajjDetailModel> GetEmployeeHajjDetails(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<EmployeeHajjDetail> employeeHajjDetails = _employeeHajjDetailRepository.FilterWithInclude(x => x.Active && (x.Employee.PNo.Contains(qs) || string.IsNullOrEmpty(qs)), "Employee");
        //    total = _employeeHajjDetailRepository.Count();
        //    employeeHajjDetails = employeeHajjDetails.OrderByDescending(x => x.EmployeeHajjDetailId).Skip((pn - 1) * ps).Take(ps);
        //    List<EmployeeHajjDetailModel> models = ObjectConverter<EmployeeHajjDetail, EmployeeHajjDetailModel>.ConvertList(employeeHajjDetails.ToList()).ToList();
        //    return models;
        //}

        public List<EmployeeHajjDetailModel> GetEmployeeHajjDetailsByPno(string PNo)
        {
            int employeeId = _employeeRepository.Where(x => x.PNo == PNo).Select(x => x.EmployeeId).SingleOrDefault();
            List<EmployeeHajjDetail> employeeHajjDetails = _employeeHajjDetailRepository.Where(x => x.EmployeeId == employeeId).ToList();
            List<EmployeeHajjDetailModel> models = ObjectConverter<EmployeeHajjDetail, EmployeeHajjDetailModel>.ConvertList(employeeHajjDetails.ToList()).ToList();
            return models;
        }

        public async Task<EmployeeHajjDetailModel> SaveEmployeeHajjDetail(int id, EmployeeHajjDetailModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Employee Hajj Detail  data missing");
            }
            bool isExistData = _employeeHajjDetailRepository.Exists(x => x.EmployeeHajjDetailId == model.EmployeeId  && x.EmployeeHajjDetailId != id);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            EmployeeHajjDetail employeeHajjDetail = ObjectConverter<EmployeeHajjDetailModel, EmployeeHajjDetail>.Convert(model);
            if (id > 0)
            {
                employeeHajjDetail = await _employeeHajjDetailRepository.FindOneAsync(x => x.EmployeeHajjDetailId == id);
                if (employeeHajjDetail == null)
                {
                    throw new InfinityNotFoundException("Employee Hajj  Detail not found !");
                }

                employeeHajjDetail.Modified = DateTime.Now;
                employeeHajjDetail.ModifiedBy = userId;
            }
            else
            {
                employeeHajjDetail.Active = true;
                employeeHajjDetail.Created = DateTime.Now;
                employeeHajjDetail.CreatedBy = userId;
            }
            employeeHajjDetail.EmployeeId = model.EmployeeId;
            employeeHajjDetail.BalotyNonBaloty = model.BalotyNonBaloty;
            employeeHajjDetail.RoyelGuest = model.RoyelGuest;
            employeeHajjDetail.HajjOrOmra = model.HajjOrOmra;
            employeeHajjDetail.ArrangedBy = model.ArrangedBy;
            employeeHajjDetail.ACompanyBy = model.ACompanyBy;
            employeeHajjDetail.FromDate = model.FromDate;
            employeeHajjDetail.ToDate = model.ToDate;
            await _employeeHajjDetailRepository.SaveAsync(employeeHajjDetail);
            return model;
        }
    }
}
