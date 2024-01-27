using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ICommissionTypeService
    {
        List<CommissionTypeModel> CommissionTypes(int ps, int pn, string qs, out int total);
        Task<CommissionTypeModel> GetCommissionType(int id);
        Task<CommissionTypeModel> SaveCommissionType(int v, CommissionTypeModel model);
        Task<bool> DeleteCommissionType(int id);
        Task<List<SelectModel>> GetCommissionTypeSelectModels();
    }
}
