using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmpRejoinService
    {

        List<EmpRejoinModel> GetEmpRejoins(int ps, int pn, string qs, out int total);
        Task<EmpRejoinModel> GetEmpRejoin(int id);
        Task<EmpRejoinModel> SaveEmpRejoin(int v, EmpRejoinModel model);
        Task<bool> DeleteEmpRejoin(int id);


    }
}