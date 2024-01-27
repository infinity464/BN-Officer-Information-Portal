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
    public class SeaServiceService : ISeaServiceService
    {
        private readonly IBnoisRepository<SeaService> seaServiceRepository;
        public SeaServiceService(IBnoisRepository<SeaService> seaServiceRepository)
        {
            this.seaServiceRepository = seaServiceRepository;
        }

        //public List<SeaServiceModel> GetSeaServices(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<SeaService> seaServices = seaServiceRepository.FilterWithInclude(x => x.IsActive
        //        && (x.Employee.PNo.Contains(qs) || String.IsNullOrEmpty(qs)), "Employee", "Country");
        //    total = seaServices.Count();
	       // seaServices = seaServices.OrderByDescending(x => x.SeaServiceId).Skip((pn - 1) * ps).Take(ps);
        //    List<SeaServiceModel> models = ObjectConverter<SeaService, SeaServiceModel>.ConvertList(seaServices.ToList()).ToList();
        //    models = models.Select(x =>
        //    {
        //        x.ShipTypeName = Enum.GetName(typeof(ShipType), x.ShipType);
        //        return x;
        //    }).ToList();
        //    return models;
        //}

        public async Task<SeaServiceModel> GetSeaService(int id)
        {
            if (id <= 0)
            {
                return new SeaServiceModel();
            }
            SeaService seaService = await seaServiceRepository.FindOneAsync(x => x.SeaServiceId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch" });
            if (seaService == null)
            {
                throw new InfinityNotFoundException("Training Result not found");
            }
            SeaServiceModel model = ObjectConverter<SeaService, SeaServiceModel>.Convert(seaService);
            return model;
        }


        public async Task<SeaServiceModel> SaveSeaService(int id, SeaServiceModel model)
        {
            model.Employee = null;
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Training Result  data missing");
            }



            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            SeaService seaService = ObjectConverter<SeaServiceModel, SeaService>.Convert(model);
            if (id > 0)
            {
	            seaService = await seaServiceRepository.FindOneAsync(x => x.SeaServiceId == id);
                if (seaService == null)
                {
                    throw new InfinityNotFoundException("Training Result not found !");
                }

	            seaService.ModifiedDate = DateTime.Now;
	            seaService.ModifiedBy = userId;
            }
            else
            {
	            seaService.IsActive = true;
	            seaService.CreatedDate = DateTime.Now;
	            seaService.CreatedBy = userId;
            }
	        seaService.EmployeeId = model.EmployeeId;
	        seaService.CountryId = model.CountryId;
	        seaService.ShipName = model.ShipName;
	        seaService.ShipType = model.ShipType;
	        seaService.OrganizationName = model.OrganizationName;
	        seaService.AppointmentName = model.AppointmentName;
	        seaService.FromDate = model.FromDate ?? seaService.FromDate;
	        seaService.ToDate = model.ToDate?? seaService.ToDate;
	        seaService.Purpose = model.Purpose;
	        seaService.Reference = model.Reference;

            await seaServiceRepository.SaveAsync(seaService);
            model.SeaServiceId = seaService.SeaServiceId;
            return model;
        }


        public async Task<bool> DeleteSeaService(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            SeaService seaService = await seaServiceRepository.FindOneAsync(x => x.SeaServiceId == id);
            if (seaService == null)
            {
                throw new InfinityNotFoundException("Training Result not found");
            }
            else
            {
                return await seaServiceRepository.DeleteAsync(seaService);
            }
        }


        public List<SelectModel> GetShipTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(ShipType)).Cast<ShipType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        public List<SeaServiceModel> GetSeaServices(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
