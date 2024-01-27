using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.Data;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.ExceptionHelper;
using Infinity.Bnois.Configuration;

namespace Infinity.Bnois.ApplicationService.Implementation
{
   public class ExtracurricularService: IExtracurricularService
    {
        private readonly IBnoisRepository<Extracurricular> extracurricularRepository;
        public ExtracurricularService(IBnoisRepository<Extracurricular> extracurricularRepository)
        {
            this.extracurricularRepository = extracurricularRepository;
        }

        public async Task<ExtracurricularModel> GetExtracurricular(int extracurricularId)
        {
            if (extracurricularId <= 0)
            {
                return new ExtracurricularModel();
            }
            Extracurricular extracurricular = await extracurricularRepository.FindOneAsync(x => x.ExtracurricularId == extracurricularId);

            if (extracurricular == null)
            {
                throw new InfinityNotFoundException("Extracurricular not found!");
            }
            ExtracurricularModel model = ObjectConverter<Extracurricular, ExtracurricularModel>.Convert(extracurricular);
            return model;
        }

        public List<ExtracurricularModel> GetExtracurriculars(int employeeId)
        {
            List<Extracurricular> extracurriculars = extracurricularRepository.FilterWithInclude(x => x.EmployeeId == employeeId, "ExtracurricularType").ToList();
            List<ExtracurricularModel> models = ObjectConverter<Extracurricular, ExtracurricularModel>.ConvertList(extracurriculars.ToList()).ToList();
            return models;
        }

        public async Task<ExtracurricularModel> SaveExtracurricular(int extracurricularId, ExtracurricularModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Extracurricular data missing");
            }
            bool isExist = await extracurricularRepository.ExistsAsync(x => x.ExtracurricularTypeId == model.ExtracurricularTypeId && x.EmployeeId == model.EmployeeId && x.ExtracurricularId != model.ExtracurricularId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Extracurricular data already exist !");
            }
            Extracurricular extracurricular = ObjectConverter<ExtracurricularModel, Extracurricular>.Convert(model);
            string userId= ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            if (extracurricularId > 0)
            {
                extracurricular = await extracurricularRepository.FindOneAsync(x => x.ExtracurricularId == extracurricularId);
                if (extracurricular == null)
                {
                    throw new InfinityNotFoundException("Extracurricular not found!");
                }
                extracurricular.ModifiedBy = userId;
                extracurricular.ModifiedDate = DateTime.Now;
            }
            else
            {
                extracurricular.CreatedDate = DateTime.Now;
                extracurricular.CreatedBy = userId;
            }
            extracurricular.EmployeeId = model.EmployeeId;
            extracurricular.ExtracurricularTypeId = model.ExtracurricularTypeId;
            extracurricular.HoldAnyPost = model.HoldAnyPost;
            extracurricular.Remarks = model.Remarks;
            await extracurricularRepository.SaveAsync(extracurricular);
            model.ExtracurricularId = extracurricular.ExtracurricularId;
            return model;
        }


        public async Task<bool> DeleteExtraCurricular(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Extracurricular extracurricular = await extracurricularRepository.FindOneAsync(x => x.ExtracurricularId == id);
            if (extracurricular == null)
            {
                throw new InfinityNotFoundException("Extracurricular not found");
            }
            else
            {
                return await extracurricularRepository.DeleteAsync(extracurricular);
            }
        }
    }
}
