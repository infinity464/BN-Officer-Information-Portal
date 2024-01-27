using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IOccupationService
    {
        List<OccupationModel> GetOccupations(int ps, int pn, string qs, out int total);
        Task<OccupationModel> GetOccupation(int id);
        Task<OccupationModel> SaveOccupation(int v, OccupationModel model);
        Task<bool> DeleteOccupation(int id);
        Task<List<SelectModel>> GetOccupationSelectModels();
       
    }
}
