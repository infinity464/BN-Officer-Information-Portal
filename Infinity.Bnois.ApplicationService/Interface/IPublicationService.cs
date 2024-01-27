using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPublicationService
    {
        List<PublicationModel> GetPublications(int ps, int pn, string qs, out int total);
        Task<PublicationModel> GetPublication(int id);
        Task<PublicationModel> SavePublication(int v, PublicationModel model);
        Task<bool> DeletePublication(int id);
        Task<List<SelectModel>> GetPublicationSelectModelsByPublicationCategory(int id);
        Task<List<SelectModel>> GetPublicationSelectModels();
    }
}