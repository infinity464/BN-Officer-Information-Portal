using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;


namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ICertificateService
    {
        List<CertificateModel> GetCertificates(int ps, int pn, string qs, out int total);
        Task<CertificateModel> SaveCertificate(int id, CertificateModel model);
        Task<CertificateModel> GetCertificate(int id);
        Task<bool> DeleteCertificate(int id);
        Task <List<SelectModel>> GetCertificateSelectModels();

    }
}
