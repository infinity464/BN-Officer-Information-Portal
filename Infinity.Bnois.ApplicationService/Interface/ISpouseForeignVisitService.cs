using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ISpouseForeignVisitService
    {
        List<SpouseForeignVisitModel> GetSpouseForeignVisits(int spouseId);
        Task<SpouseForeignVisitModel> GetSpouseForeignVisit(int spouseForeignVisitId);
        Task<SpouseForeignVisitModel> SaveSpouseForeignVisit(int v, SpouseForeignVisitModel model);
    }
}
