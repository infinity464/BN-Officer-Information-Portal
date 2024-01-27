using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IOPRAppointmentService
    {
        List<OprAptSuitabilityModel> GetOPRAppointments(int id); 
      
        Task<OprAptSuitabilityModel> SaveOPRAppointment(int id, OprAptSuitabilityModel model);    
        Task<bool> DeleteOPRAppointment(int id);
    
    }
}
