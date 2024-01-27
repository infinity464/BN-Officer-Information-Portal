using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IHeirNextOfKinInfoService
    {
        List<HeirNextOfKinInfoModel> GetHeirNextOfKinInfoList(int employeeId);
        Task<HeirNextOfKinInfoModel> GetHeirNextOfKinInfo(int heirNextOfKinInfoId);
        Task<HeirNextOfKinInfoModel> SaveHeirNextOfKinInfo(int heirNextOfKinInfoId, HeirNextOfKinInfoModel model);
        List<SelectModel> GetHeirKinTypeSelectModels();

        Task<bool> DeleteHeirNextOfKinInfo(int id);
        Task<HeirNextOfKinInfoModel> UpdateHeirNextOfKinInfo(HeirNextOfKinInfoModel heirNextOfKinInfo);
    }
}