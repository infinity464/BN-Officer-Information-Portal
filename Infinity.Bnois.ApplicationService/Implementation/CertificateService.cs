using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
   public class CertificateService : ICertificateService
    {

        private readonly IBnoisRepository<Certificate> certificateRepository;
        public CertificateService(IBnoisRepository<Certificate> certificateRepository)
        {
            this.certificateRepository = certificateRepository;
        }

        public List<CertificateModel> GetCertificates(int ps, int pn, string qs, out int total)
        {
            IQueryable<Certificate> certificates = certificateRepository.FilterWithInclude(x => x.IsActive && (x.FullName.Contains(qs) || String.IsNullOrEmpty(qs) ));
            total = certificates.Count();
            certificates = certificates.OrderByDescending(x => x.CertificateId).Skip((pn - 1) * ps).Take(ps);
            List<CertificateModel> models = ObjectConverter<Certificate, CertificateModel>.ConvertList(certificates.ToList()).ToList();
            return models;
        }

        public async Task<CertificateModel> GetCertificate(int id)
        {
            if (id <= 0)
            {
                return new CertificateModel();
            }
            Certificate certificate = await certificateRepository.FindOneAsync(x => x.CertificateId == id);
            if (certificate == null)
            {
                throw new InfinityNotFoundException("Certificate not found");
            }
            CertificateModel model = ObjectConverter<Certificate, CertificateModel>.Convert(certificate);
            return model;
        }

        public async Task<CertificateModel> SaveCertificate(int id, CertificateModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Certificate data missing");
            }
            bool isExist = certificateRepository.Exists(x => x.FullName == model.FullName  && x.CertificateId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Certificate certificate = ObjectConverter<CertificateModel, Certificate>.Convert(model);
            if (id > 0)
            {
                certificate = await certificateRepository.FindOneAsync(x => x.CertificateId == id);
                if (certificate == null)
                {
                    throw new InfinityNotFoundException("Certificate not found !");
                }

                certificate.ModifiedDate = DateTime.Now;
                certificate.ModifiedBy = userId;
            }
            else
            {
                certificate.IsActive = true;
                certificate.CreatedDate = DateTime.Now;
                certificate.CreatedBy = userId;
            }
            certificate.FullName = model.FullName;
            certificate.ShortName = model.ShortName;

            await certificateRepository.SaveAsync(certificate);
            model.CertificateId = certificate.CertificateId;
            return model;
        }

        public async Task<bool> DeleteCertificate(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Certificate certificate = await certificateRepository.FindOneAsync(x => x.CertificateId == id);
            if (certificate == null)
            {
                throw new InfinityNotFoundException("Certificate not found");
            }
            else
            {
                return await certificateRepository.DeleteAsync(certificate);
            }
        }

        public async Task<List<SelectModel>> GetCertificateSelectModels()
        {
            ICollection<Certificate> models = await certificateRepository.FilterAsync(x => x.IsActive);
            return models.Select(x => new SelectModel()
            {
                Text = x.FullName,
                Value = x.CertificateId
            }).ToList();

        }




    }
}
