using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IObservationIntelligentService
    {
        List<ObservationIntelligentModel> GetObservationIntelligents(int ps, int pn, string qs, out int total);
        Task<ObservationIntelligentModel> GetObservationIntelligent(int id);
        Task<ObservationIntelligentModel> SaveObservationIntelligent(int id, ObservationIntelligentModel model);
        Task<bool> DeleteObservationIntelligent(int id);
        List<SelectModel> GetObservationIntelligentTypeSelectModels();
        


    }
}
