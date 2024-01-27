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
    public class EmployeeSportService : IEmployeeSportService
    {
        private readonly IBnoisRepository<EmployeeSport> employeeSportRepository;
        public EmployeeSportService(IBnoisRepository<EmployeeSport> employeeSportRepository)
        {
            this.employeeSportRepository = employeeSportRepository;
        }

        public async Task<EmployeeSportModel> GetEmployeeSport(int employeeSportId)
        {
            if (employeeSportId <= 0)
            {
                return new EmployeeSportModel();
            }
            EmployeeSport employeeSport = await employeeSportRepository.FindOneAsync(x => x.EmployeeSportId == employeeSportId);
            if (employeeSport == null)
            {
                return new EmployeeSportModel();
            }

            EmployeeSportModel model = ObjectConverter<EmployeeSport, EmployeeSportModel>.Convert(employeeSport);
            return model;
        }

        public List<EmployeeSportModel> GetEmployeeSports(int employeeId)
        {
            List<EmployeeSport> employeeSports = employeeSportRepository.FilterWithInclude(x => x.EmployeeId == employeeId, "Sport").ToList();
            List<EmployeeSportModel> models = ObjectConverter<EmployeeSport, EmployeeSportModel>.ConvertList(employeeSports.ToList()).ToList();
            return models;
        }

        public async Task<EmployeeSportModel> SaveEmployeeSport(int employeeSportId, EmployeeSportModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Officer Sports data missing!");
            }

            EmployeeSport employeeSport = ObjectConverter<EmployeeSportModel, EmployeeSport>.Convert(model);
            string userId= ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            if (employeeSportId > 0)
            {
                employeeSport = await employeeSportRepository.FindOneAsync(x => x.EmployeeSportId == employeeSportId);
                if (employeeSport == null)
                {
                    throw new InfinityNotFoundException("Officer Sports not found !");
                }

                employeeSport.ModifiedDate = DateTime.Now;
                employeeSport.ModifiedBy = userId;
            }
            else
            {
                employeeSport.EmployeeId = model.EmployeeId;
                employeeSport.CreatedBy = userId;
                employeeSport.CreatedDate = DateTime.Now;
                employeeSport.IsActive = true;
            }

            employeeSport.SportId = model.SportId;
            employeeSport.TeamName = model.TeamName;
            employeeSport.DateOfParticipation = model.DateOfParticipation;
            employeeSport.Award = model.Award;
            employeeSport.Hobby = "";
            await employeeSportRepository.SaveAsync(employeeSport);
            model.EmployeeSportId = employeeSport.EmployeeSportId;
            return model;
        }


        public async Task<bool> DeleteEmployeeSport(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EmployeeSport employeeSport = await employeeSportRepository.FindOneAsync(x => x.EmployeeSportId == id);
            if (employeeSport == null)
            {
                throw new InfinityNotFoundException("Sport not found");
            }
            else
            {
                return await employeeSportRepository.DeleteAsync(employeeSport);
            }
        }
    }
}
