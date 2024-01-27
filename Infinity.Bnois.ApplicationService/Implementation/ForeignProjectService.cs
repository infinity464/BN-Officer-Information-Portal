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
    public class ForeignProjectService : IForeignProjectService
    {
        private readonly IBnoisRepository<ForeignProject> foreignProjectRepository;
        public ForeignProjectService(IBnoisRepository<ForeignProject> foreignProjectRepository)
        {
            this.foreignProjectRepository = foreignProjectRepository;
        }

        //public List<ForeignProjectModel> GetForeignProjects(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<ForeignProject> foreignProjects = foreignProjectRepository.FilterWithInclude(x => x.IsActive
        //        && (x.Employee.PNo.Contains(qs) || String.IsNullOrEmpty(qs)), "Employee", "Country");
        //    total = foreignProjects.Count();
        //    foreignProjects = foreignProjects.OrderByDescending(x => x.ForeignProjectId).Skip((pn - 1) * ps).Take(ps);
        //    List<ForeignProjectModel> models = ObjectConverter<ForeignProject, ForeignProjectModel>.ConvertList(foreignProjects.ToList()).ToList();
        //    return models;
        //}

        public async Task<ForeignProjectModel> GetForeignProject(int id)
        {
            if (id <= 0)
            {
                return new ForeignProjectModel();
            }
            ForeignProject foreignProject = await foreignProjectRepository.FindOneAsync(x => x.ForeignProjectId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch" });
            if (foreignProject == null)
            {
                throw new InfinityNotFoundException("Foreign Project not found");
            }
            ForeignProjectModel model = ObjectConverter<ForeignProject, ForeignProjectModel>.Convert(foreignProject);
            return model;
        }


        public async Task<ForeignProjectModel> SaveForeignProject(int id, ForeignProjectModel model)
        {
            model.Employee = null;
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Foreign Project  data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            ForeignProject foreignProject = ObjectConverter<ForeignProjectModel, ForeignProject>.Convert(model);
            if (id > 0)
            {
                foreignProject = await foreignProjectRepository.FindOneAsync(x => x.ForeignProjectId == id);
                if (foreignProject == null)
                {
                    throw new InfinityNotFoundException("Foreign Project not found !");
                }

                foreignProject.ModifiedDate = DateTime.Now;
                foreignProject.ModifiedBy = userId;
            }
            else
            {
                foreignProject.IsActive = true;
                foreignProject.CreatedDate = DateTime.Now;
                foreignProject.CreatedBy = userId;
            }
            foreignProject.EmployeeId = model.EmployeeId;
	        foreignProject.CountryId = model.CountryId;
	        foreignProject.ProjectName = model.ProjectName;
	        foreignProject.OrganizationName = model.OrganizationName;
	        foreignProject.AppointmentName = model.AppointmentName;
	        foreignProject.FromDate = model.FromDate;
	        foreignProject.ToDate = model.ToDate;
	        foreignProject.Purpose = model.Purpose;
	        foreignProject.Reference = model.Reference;
	        foreignProject.Remarks = model.Remarks;

            await foreignProjectRepository.SaveAsync(foreignProject);
            model.ForeignProjectId = foreignProject.ForeignProjectId;
            return model;
        }


        public async Task<bool> DeleteForeignProject(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            ForeignProject foreignProject = await foreignProjectRepository.FindOneAsync(x => x.ForeignProjectId == id);
            if (foreignProject == null)
            {
                throw new InfinityNotFoundException("Foreign Project not found");
            }
            else
            {
                return await foreignProjectRepository.DeleteAsync(foreignProject);
            }
        }

        public List<ForeignProjectModel> GetForeignProjects(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
