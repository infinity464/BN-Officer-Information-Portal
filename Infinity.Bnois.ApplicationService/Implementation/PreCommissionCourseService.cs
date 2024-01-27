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
   public class PreCommissionCourseService: IPreCommissionCourseService
    {
        private readonly IBnoisRepository<PreCommissionCourse> preCommissionCourseRepository;
        public PreCommissionCourseService(IBnoisRepository<PreCommissionCourse> preCommissionCourseRepository)
        {
            this.preCommissionCourseRepository = preCommissionCourseRepository;
        }
        
        public async Task<PreCommissionCourseModel> GetPreCommissionCourse(int preCommissionCourseId)
        {
            if (preCommissionCourseId <= 0)
            {
                return new PreCommissionCourseModel();
            }
            PreCommissionCourse preCommissionCourse = await preCommissionCourseRepository.FindOneAsync(x => x.PreCommissionCourseId == preCommissionCourseId);

            if (preCommissionCourse == null)
            {
                throw new InfinityNotFoundException("Pre Commission Course not found!");
            }
            PreCommissionCourseModel model = ObjectConverter<PreCommissionCourse, PreCommissionCourseModel>.Convert(preCommissionCourse);
            return model;
        }

        public List<PreCommissionCourseModel> GetPreCommissionCourses(int employeeId)
        {
            List<PreCommissionCourse> preCommissionCourses = preCommissionCourseRepository.FilterWithInclude(x => x.EmployeeId == employeeId,"Employee","Country","Medal").ToList();
            List<PreCommissionCourseModel> models = ObjectConverter<PreCommissionCourse, PreCommissionCourseModel>.ConvertList(preCommissionCourses.ToList()).ToList();
            return models;
        }

        public async Task<PreCommissionCourseModel> SavePreCommissionCourse(int preCommissionCourseId, PreCommissionCourseModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Pre Commission Course data missing");
            }
            

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            PreCommissionCourse preCommissionCourse = ObjectConverter<PreCommissionCourseModel, PreCommissionCourse>.Convert(model);
            if (preCommissionCourseId > 0)
            {
                preCommissionCourse = await preCommissionCourseRepository.FindOneAsync(x => x.PreCommissionCourseId == preCommissionCourseId);
                if (preCommissionCourse == null)
                {
                    throw new InfinityNotFoundException("Pre Commission Course not found !");
                }
                preCommissionCourse.Modified = DateTime.Now;
                preCommissionCourse.ModifiedBy = userId;

            }
            else
            {
                preCommissionCourse.IsActive = true;
                preCommissionCourse.CreatedDate = DateTime.Now;
                preCommissionCourse.CreatedBy = userId;
            }
            preCommissionCourse.EmployeeId = model.EmployeeId;
            preCommissionCourse.BnaNo = model.BnaNo;
            preCommissionCourse.IsAbroad = model.IsAbroad;
            preCommissionCourse.CountryId = model.CountryId;
            preCommissionCourse.MedalId = model.MedalId;
            preCommissionCourse.AppointmentHeld = model.AppointmentHeld;
            preCommissionCourse.ModuleD = model.ModuleD;
            preCommissionCourse.Total = model.Total;
            preCommissionCourse.FinalPosition = model.FinalPosition;
            preCommissionCourse.Remarks = model.Remarks;
            
            await preCommissionCourseRepository.SaveAsync(preCommissionCourse);
            return model;
        }

        public async Task<bool> DeletePreCommissionCourse(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }

            PreCommissionCourse preCommissionCourse =
                await preCommissionCourseRepository.FindOneAsync(x => x.PreCommissionCourseId == id);
            if (preCommissionCourse == null)
            {
                throw new InfinityNotFoundException("Pre Commission Course not found");
            }
            else
            {
                return await preCommissionCourseRepository.DeleteAsync(preCommissionCourse);
            }
        }
    }
}
