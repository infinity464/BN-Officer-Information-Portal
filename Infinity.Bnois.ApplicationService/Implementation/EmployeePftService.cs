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
    public class EmployeePftService : IEmployeePftService
    {
        private readonly IBnoisRepository<EmployeePft> employeePftRepository;
        public EmployeePftService(IBnoisRepository<EmployeePft> employeePftRepository)
        {
            this.employeePftRepository = employeePftRepository;
        }

     //   public List<EmployeePftModel> GetEmployeePfts(int ps, int pn, string qs, out int total)
     //   {
     //       IQueryable<EmployeePft> employeePfts = employeePftRepository.FilterWithInclude(x => x.IsActive
				 //&& (x.Employee.PNo == (qs) || x.Employee.FullNameEng.Contains(qs) || String.IsNullOrEmpty(qs)), "Employee", "PftType","PftResult");
     //       total = employeePfts.Count();
     //       employeePfts = employeePfts.OrderByDescending(x => x.EmployeePftId).Skip((pn - 1) * ps).Take(ps);
     //       List<EmployeePftModel> models = ObjectConverter<EmployeePft, EmployeePftModel>.ConvertList(employeePfts.ToList()).ToList();
     //       return models;
     //   }

        public async Task<EmployeePftModel> GetEmployeePft(int id)
        {
            if (id <= 0)
            {
                return new EmployeePftModel();
            }
            EmployeePft employeePft = await employeePftRepository.FindOneAsync(x => x.EmployeePftId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch" });
            if (employeePft == null)
            {
                throw new InfinityNotFoundException("Employee PFT not found");
            }
            EmployeePftModel model = ObjectConverter<EmployeePft, EmployeePftModel>.Convert(employeePft);
            return model;
        }



        public async Task<EmployeePftModel> SaveEmployeePft(int id, EmployeePftModel model)
        {
            model.Employee = null;
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Employee PFT  data missing");
            }
            bool isExistData = employeePftRepository.Exists(x =>x.PftDate==model.PftDate && x.PftTypeId == model.PftTypeId && x.EmployeeId ==model.EmployeeId && x.EmployeePftId != id);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            EmployeePft employeePft = ObjectConverter<EmployeePftModel, EmployeePft>.Convert(model);
            if (id > 0)
            {
                employeePft = await employeePftRepository.FindOneAsync(x => x.EmployeePftId == id);
                if (employeePft == null)
                {
                    throw new InfinityNotFoundException("Employee PFT not found !");
                }

                employeePft.ModifiedDate = DateTime.Now;
                employeePft.ModifiedBy = userId;
            }
            else
            {
                employeePft.IsActive = true;
                employeePft.CreatedDate = DateTime.Now;
                employeePft.CreatedBy = userId;
            }
            employeePft.EmployeeId = model.EmployeeId;
            employeePft.PftTypeId = model.PftTypeId;
            employeePft.PftResultId = model.PftResultId;
            employeePft.PftDate = (DateTime) model.PftDate;
            
           

            await employeePftRepository.SaveAsync(employeePft);
            model.EmployeePftId = employeePft.EmployeePftId;
            return model;
        }


        public async Task<bool> DeleteEmployeePft(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EmployeePft employeePft = await employeePftRepository.FindOneAsync(x => x.EmployeePftId == id);
            if (employeePft == null)
            {
                throw new InfinityNotFoundException("Employee PFT not found");
            }
            else
            {
                return await employeePftRepository.DeleteAsync(employeePft);
            }
        }

        public List<EmployeePftModel> GetEmployeePfts(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
