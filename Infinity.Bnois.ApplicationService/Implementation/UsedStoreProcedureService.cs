using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
	public class UsedStoreProcedureService : IUsedStoreProcedureService
	{
		private readonly IBnoisRepository<UsedStoreProcedure> usedStoreProcedureRepository;
		public UsedStoreProcedureService(IBnoisRepository<UsedStoreProcedure> usedStoreProcedureRepository)
		{
			this.usedStoreProcedureRepository = usedStoreProcedureRepository;
		}

        public List<UsedStoreProcedureModel> GetUsedStoreProcedures(int id)
        {
            List<UsedStoreProcedure> usedStoreProcedures = usedStoreProcedureRepository.FilterWithInclude(x => x.UsedReportId == id, "UsedReport").ToList();
            List<UsedStoreProcedureModel> models = ObjectConverter<UsedStoreProcedure, UsedStoreProcedureModel>.ConvertList(usedStoreProcedures.ToList()).ToList();
            return models;
        }


        public async Task<UsedStoreProcedureModel> SaveUsedStoreProcedure(int id, UsedStoreProcedureModel model)
        {          
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Store Procedure data missing");
            }
            bool isExist = usedStoreProcedureRepository.Exists(x => x.StroreProcedureName == model.StroreProcedureName && x.UsedReportId==model.UsedReportId && x.UsedStoreProcedureId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }


            UsedStoreProcedure usedStoreProcedure = ObjectConverter<UsedStoreProcedureModel, UsedStoreProcedure>.Convert(model);
            if (id > 0)
            {
                usedStoreProcedure = await usedStoreProcedureRepository.FindOneAsync(x => x.UsedStoreProcedureId == id);
                if (usedStoreProcedure == null)
                {
                    throw new InfinityNotFoundException("Store Procedure not found !");
                }

            }

            usedStoreProcedure.UsedReportId = model.UsedReportId;
            usedStoreProcedure.StroreProcedureName = model.StroreProcedureName;
            usedStoreProcedure.CreatedBy = model.CreatedBy;
            usedStoreProcedure.CreatedDate = model.CreatedDate;

            await usedStoreProcedureRepository.SaveAsync(usedStoreProcedure);
            model.UsedStoreProcedureId = usedStoreProcedure.UsedStoreProcedureId;
            return model;
        }


        public async Task<bool> DeleteUsedStoreProcedure(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            UsedStoreProcedure usedStoreProcedure = await usedStoreProcedureRepository.FindOneAsync(x => x.UsedStoreProcedureId == id);
            if (usedStoreProcedure == null)
            {
                throw new InfinityNotFoundException("Store Procedure not found");
            }
            else
            {
                return await usedStoreProcedureRepository.DeleteAsync(usedStoreProcedure);
            }
        }

    }
}
