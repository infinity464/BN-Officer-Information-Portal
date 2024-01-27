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
    public class EmployeeDollarSignService : IEmployeeDollarSignService
    {
        private readonly IBnoisRepository<EmployeeDollarSign> employeeDollarSignRepository;
        public EmployeeDollarSignService(IBnoisRepository<EmployeeDollarSign> employeeDollarSignRepository)
        {
            this.employeeDollarSignRepository = employeeDollarSignRepository;
        }
        
        public async Task<EmployeeDollarSignModel> GetEmployeeDollarSign(int employeeDollarSignId)
        {
            if (employeeDollarSignId <= 0)
            {
                return new EmployeeDollarSignModel();
            }
            EmployeeDollarSign employeeDollarSign = await employeeDollarSignRepository.FindOneAsync(x => x.EmployeeDollarSignId == employeeDollarSignId, new List<string> { "Employee","Employee.Rank", "Employee.Batch" });
            if (employeeDollarSign == null)
            {
                throw new InfinityNotFoundException("Officer Dollar Sign not found");
            }
            EmployeeDollarSignModel model = ObjectConverter<EmployeeDollarSign, EmployeeDollarSignModel>.Convert(employeeDollarSign);
            return model;
        }

        //public List<EmployeeDollarSignModel> GetEmployeeDollarSigns(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<EmployeeDollarSign> employeeDollarSigns = employeeDollarSignRepository.FilterWithInclude(x => x.IsActive && (x.Employee.PNo == (qs) || x.Employee.FullNameEng.Contains(qs) || String.IsNullOrEmpty(qs)), "Employee");
        //    total = employeeDollarSigns.Count();
        //    employeeDollarSigns = employeeDollarSigns.OrderByDescending(x => x.EmployeeDollarSignId).Skip((pn - 1) * ps).Take(ps);
        //    List<EmployeeDollarSignModel> models = ObjectConverter<EmployeeDollarSign, EmployeeDollarSignModel>.ConvertList(employeeDollarSigns.ToList()).ToList();
        //    return models;
        //}

        public async Task<EmployeeDollarSignModel> SaveEmployeeDollarSign(int employeeDollarSignId, EmployeeDollarSignModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Officer Dollar Sign data missing");
            }
            bool isExist = employeeDollarSignRepository.Exists(x => x.EmployeeId == model.EmployeeId && x.EmployeeDollarSignId != employeeDollarSignId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Officer already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            EmployeeDollarSign employeeDollarSign = ObjectConverter<EmployeeDollarSignModel, EmployeeDollarSign>.Convert(model);
            if (employeeDollarSignId > 0)
            {
                employeeDollarSign = await employeeDollarSignRepository.FindOneAsync(x => x.EmployeeDollarSignId == employeeDollarSignId);
                if (employeeDollarSign == null)
                {
                    throw new InfinityNotFoundException("Officer Dollar Sign not found !");
                }

                employeeDollarSign.ModifiedDate = DateTime.Now;
                employeeDollarSign.ModifiedBy = userId;
            }
            else
            {
                employeeDollarSign.IsActive = true;
                employeeDollarSign.CreatedDate = DateTime.Now;
                employeeDollarSign.CreatedBy = userId;
            }
            employeeDollarSign.EmployeeId = model.EmployeeId;
            employeeDollarSign.HasDollarSign = true;
            employeeDollarSign.Reason = model.Reason;
            employeeDollarSign.DateOfDollarSign = model.DateOfDollarSign;

            await employeeDollarSignRepository.SaveAsync(employeeDollarSign);
            model.EmployeeDollarSignId = employeeDollarSign.EmployeeDollarSignId;
            return model;
        }

        public async Task<bool> DeleteEmployeeDollarSign(int employeeDollarSignId)
        {
            if (employeeDollarSignId < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EmployeeDollarSign employeeDollarSign = await employeeDollarSignRepository.FindOneAsync(x => x.EmployeeDollarSignId == employeeDollarSignId);
            if (employeeDollarSign == null)
            {
                throw new InfinityNotFoundException("Officer Dollar Sign not found");
            }
            else
            {
                return await employeeDollarSignRepository.DeleteAsync(employeeDollarSign);
            }
        }

        public List<EmployeeDollarSignModel> GetEmployeeDollarSigns(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
