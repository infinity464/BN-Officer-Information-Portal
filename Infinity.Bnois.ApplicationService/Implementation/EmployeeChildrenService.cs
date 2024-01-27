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
    public class EmployeeChildrenService : IEmployeeChildrenService
    {
        private readonly IBnoisRepository<EmployeeChildren> employeeChildrenRepository;
        public EmployeeChildrenService(IBnoisRepository<EmployeeChildren> employeeChildrenRepository)
        {
            this.employeeChildrenRepository = employeeChildrenRepository;
        }

        public List<SelectModel> GetChildrenTypeSelectModels()
        {
            List<SelectModel> selectModels =
           Enum.GetValues(typeof(ChildrenType)).Cast<ChildrenType>()
                .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                .ToList();
            return selectModels;
        }




        public async Task<EmployeeChildrenModel> GetEmployeeChildren(int employeeChildrenId)
        {
            if (employeeChildrenId <= 0)
            {
                return new EmployeeChildrenModel();
            }
            EmployeeChildren employeeChildren = await employeeChildrenRepository.FindOneAsync(x => x.EmployeeChildrenId == employeeChildrenId);

            if (employeeChildren == null)
            {
                throw new InfinityNotFoundException("Children not found!");
            }
            EmployeeChildrenModel model = ObjectConverter<EmployeeChildren, EmployeeChildrenModel>.Convert(employeeChildren);
            return model;
        }

        public List<EmployeeChildrenModel> GetEmployeeChildrens(int employeeId)
        {
            List<EmployeeChildren> employeeChildrens = employeeChildrenRepository.FilterWithInclude(x => x.EmployeeId == employeeId, "Employee", "Occupation").ToList();
            List<EmployeeChildrenModel> models = ObjectConverter<EmployeeChildren, EmployeeChildrenModel>.ConvertList(employeeChildrens.ToList()).ToList();
            models = models.Select(x =>
            {
                x.ChildrenTypeName = Enum.GetName(typeof(ChildrenType), x.ChildrenType);
                return x;
            }).ToList();
            return models;
        }

        public async Task<EmployeeChildrenModel> SaveEmployeeChildren(int id, EmployeeChildrenModel model)
        {

            if (model == null)
            {
                throw new InfinityArgumentMissingException("Children data missing");
            }
            EmployeeChildren employeeChildren = ObjectConverter<EmployeeChildrenModel, EmployeeChildren>.Convert(model);

            if (id > 0)
            {
                employeeChildren = await employeeChildrenRepository.FindOneAsync(x => x.EmployeeChildrenId == id);
                if (employeeChildren == null)
                {
                    throw new InfinityNotFoundException("Children not found!");
                }
                employeeChildren.ModifiedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                employeeChildren.ModifiedDate = DateTime.Now;
            }
            else
            {
                employeeChildren.IsActive = true;
                employeeChildren.CreatedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                employeeChildren.CreatedDate = DateTime.Now;
            }
            employeeChildren.EmployeeId = model.EmployeeId;
            employeeChildren.ChildrenType = model.ChildrenType;
            employeeChildren.Child = model.Child;
            employeeChildren.Name = model.Name;
            employeeChildren.NameBan = model.NameBan;
            employeeChildren.DateOfBirth = model.DateOfBirth;
            employeeChildren.LivingWith = model.LivingWith;
            employeeChildren.ContactAddress = model.ContactAddress;
            employeeChildren.OccupationId = model.OccupationId;
            employeeChildren.OfficeAddress = model.OfficeAddress;
            employeeChildren.IsDead = model.IsDead;
            employeeChildren.DeadDate = model.DeadDate;
            employeeChildren.DeadReason = model.DeadReason;
            await employeeChildrenRepository.SaveAsync(employeeChildren);
            model.EmployeeChildrenId = employeeChildren.EmployeeChildrenId;
            return model;
        }



        public async Task<bool> DeleteEmployeeChildren(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EmployeeChildren employeeChildren = await employeeChildrenRepository.FindOneAsync(x => x.EmployeeChildrenId == id);
            if (employeeChildren == null)
            {
                throw new InfinityNotFoundException("Children not found");
            }
            else
            {
                return await employeeChildrenRepository.DeleteAsync(employeeChildren);
            }
        }

        public async Task<EmployeeChildrenModel> UpdateEmployeeChildren(EmployeeChildrenModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Children data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            EmployeeChildren employeeChildren = ObjectConverter<EmployeeChildrenModel, EmployeeChildren>.Convert(model);

            employeeChildren = await employeeChildrenRepository.FindOneAsync(x => x.EmployeeChildrenId == model.EmployeeChildrenId);
            if (employeeChildren == null)
            {
                throw new InfinityNotFoundException("Children not found !");
            }

            if (model.FileName != null)
            {
                employeeChildren.FileName = model.FileName;
            }

            if (model.GenFormName != null)
            {
                employeeChildren.GenFormName = model.GenFormName;
            }

            employeeChildren.ModifiedDate = DateTime.Now;
            employeeChildren.ModifiedBy = userId;
            await employeeChildrenRepository.SaveAsync(employeeChildren);
            model.EmployeeChildrenId = employeeChildren.EmployeeChildrenId;
            return model;

        }
    }
}
