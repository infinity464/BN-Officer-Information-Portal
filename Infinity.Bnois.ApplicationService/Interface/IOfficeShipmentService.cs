using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IOfficeShipmentService
    {
      
        Task<OfficeShipmentModel> SaveOfficeShipment(OfficeShipmentModel model);
        Task<bool> DeleteOfficeShipment(int id);
        
    }
}
