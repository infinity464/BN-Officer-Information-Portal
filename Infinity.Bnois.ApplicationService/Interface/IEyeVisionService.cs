using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEyeVisionService
    {
        List<EyeVisionModel> GetEyeVisions(int pageSize, int pageNumber, string serchText, out int total);
        Task<EyeVisionModel> GetEyeVision(int eyeVisionId);
        Task<EyeVisionModel> SaveEyeVision(int eyeVisionId, EyeVisionModel model);
        Task<bool> DeleteEyeVision(int eyeVisionId);
        Task<List<SelectModel>> GetEyeVisionSelectModels();
    }
}