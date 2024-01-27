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
	public class OfficeShipmentService : IOfficeShipmentService
	{
		private readonly IBnoisRepository<OfficeShipment> OfficeShipmentRepository;
		public OfficeShipmentService(IBnoisRepository<OfficeShipment> OfficeShipmentRepository)
		{
			this.OfficeShipmentRepository = OfficeShipmentRepository;
		}
      

        public async Task<OfficeShipmentModel> SaveOfficeShipment(OfficeShipmentModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("OfficeShipment data missing");
            }

           
            OfficeShipment OfficeShipment = ObjectConverter<OfficeShipmentModel, OfficeShipment>.Convert(model);
            
	      
	        OfficeShipment.OfficeId = model.OfficeId;
	        OfficeShipment.PreviousParentId = model.PreviousParentId;
	        OfficeShipment.ShipDate = model.ShipDate;
            OfficeShipment.PreZoneId = model.PreZoneId;
            OfficeShipment.PreAdminAuthorityId = model.PreAdminAuthorityId;
            OfficeShipment.PreAddress = model.PreAddress;
            await OfficeShipmentRepository.SaveAsync(OfficeShipment);
            model.OfficeMoveId = OfficeShipment.OfficeMoveId;
            return model;
        }

        public async Task<bool> DeleteOfficeShipment(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            OfficeShipment OfficeShipment = await OfficeShipmentRepository.FindOneAsync(x => x.OfficeMoveId == id);
            if (OfficeShipment == null)
            {
                throw new InfinityNotFoundException("OfficeShipment not found");
            }
            else
            {
                return await OfficeShipmentRepository.DeleteAsync(OfficeShipment);
            }
        }

      


    }
}
