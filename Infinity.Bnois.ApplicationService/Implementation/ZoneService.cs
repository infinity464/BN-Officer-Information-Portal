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
	public class ZoneService : IZoneService
	{
		private readonly IBnoisRepository<Zone> zoneRepository;
		public ZoneService(IBnoisRepository<Zone> zoneRepository)
		{
			this.zoneRepository = zoneRepository;
		}
        public async Task<ZoneModel> GetZone(int id)
        {
            if (id <= 0)
            {
                return new ZoneModel();
            }
            Zone zone = await zoneRepository.FindOneAsync(x => x.ZoneId == id);
            if (zone == null)
            {
                throw new InfinityNotFoundException("Zone not found");
            }
            ZoneModel model = ObjectConverter<Zone, ZoneModel>.Convert(zone);
            return model;
        }

        public List<ZoneModel> GetZones(int ps, int pn, string qs, out int total)
        {
            IQueryable<Zone> zones = zoneRepository.FilterWithInclude(x => x.IsActive
                  && ((x.Name.Contains(qs) || String.IsNullOrEmpty(qs))));
            total = zones.Count();
	        zones = zones.OrderByDescending(x => x.ZoneId).Skip((pn - 1) * ps).Take(ps);
            List<ZoneModel> models = ObjectConverter<Zone, ZoneModel>.ConvertList(zones.ToList()).ToList();
            return models;
            
        }



        public async Task<ZoneModel> SaveZone(int id, ZoneModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Zone data missing");
            }

            bool isExist = zoneRepository.Exists(x =>  x.Name == model.Name && x.ZoneId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Zone zone = ObjectConverter<ZoneModel, Zone>.Convert(model);
            if (id > 0)
            {
	            zone = await zoneRepository.FindOneAsync(x => x.ZoneId == id);
                if (zone == null)
                {
                    throw new InfinityNotFoundException("Zone not found !");
                }

	            zone.ModifiedDate = DateTime.Now;
	            zone.ModifiedBy = userId;
            }
            else
            {
	            zone.IsActive = true;
	            zone.CreatedDate = DateTime.Now;
	            zone.CreatedBy = userId;
            }
	        zone.Name = model.Name;
	        zone.Remarks = model.Remarks;
            await zoneRepository.SaveAsync(zone);
            model.ZoneId = zone.ZoneId;
            return model;
        }

        public async Task<bool> DeleteZone(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Zone Zone = await zoneRepository.FindOneAsync(x => x.ZoneId == id);
            if (Zone == null)
            {
                throw new InfinityNotFoundException("Zone not found");
            }
            else
            {
                return await zoneRepository.DeleteAsync(Zone);
            }
        }

        public async Task<List<SelectModel>> GetZoneSelectModels()
        {
            ICollection<Zone> Zones = await zoneRepository.FilterAsync(x => x.IsActive);
            List<SelectModel> selectModels = Zones.OrderBy(x=>x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.ZoneId
            }).ToList();
            return selectModels;
        }


    }
}
