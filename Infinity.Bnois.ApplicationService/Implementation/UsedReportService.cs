using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class UsedReportService : IUsedReportService
    {
        private readonly IBnoisRepository<UsedReport> usedReportRepository;
        public UsedReportService(IBnoisRepository<UsedReport> usedReportRepository)
        {
            this.usedReportRepository = usedReportRepository;
        }

    
        public List<UsedReportModel> GetUsedReports(int ps, int pn, string qs, out int total)
        {
            IQueryable<UsedReport> usedReports = usedReportRepository.FilterWithInclude(x => x.UsedReportId > 0 && (x.ReportName.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = usedReports.Count();
            usedReports = usedReports.OrderByDescending(x => x.UsedReportId).Skip((pn - 1) * ps).Take(ps);
            List<UsedReportModel> models = ObjectConverter<UsedReport, UsedReportModel>.ConvertList(usedReports.ToList()).ToList();
            return models;
        }
        public async Task<UsedReportModel> GetUsedReport(int id)
        {
            if (id <= 0)
            {
                return new UsedReportModel();
            }
            UsedReport usedReport = await usedReportRepository.FindOneAsync(x => x.UsedReportId == id);
            if (usedReport == null)
            {
                throw new InfinityNotFoundException("Report not found");
            }
            UsedReportModel model = ObjectConverter<UsedReport, UsedReportModel>.Convert(usedReport);
            return model;
        }

        public async Task<UsedReportModel> SaveUsedReport(int id, UsedReportModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Report data missing");
            }
            bool isExist = usedReportRepository.Exists(x => x.ReportName== model.ReportName && x.UsedReportId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            UsedReport usedReport = ObjectConverter<UsedReportModel, UsedReport>.Convert(model);
            if (id > 0)
            {
                usedReport = await usedReportRepository.FindOneAsync(x => x.UsedReportId == id);
                if (usedReport == null)
                {
                    throw new InfinityNotFoundException("Report not found !");
                }

               
            }

            usedReport.ReportName = model.ReportName;
            usedReport.CreatedBy = model.CreatedBy;
            usedReport.CreatedDate = model.CreatedDate;
        
            await usedReportRepository.SaveAsync(usedReport);
            model.UsedReportId = usedReport.UsedReportId;
            return model;
        }

        public async Task<bool> DeleteUsedReport(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            UsedReport usedReport = await usedReportRepository.FindOneAsync(x => x.UsedReportId == id);
            if (usedReport == null)
            {
                throw new InfinityNotFoundException("Report not found");
            }
            else
            {
                return await usedReportRepository.DeleteAsync(usedReport);
            }
        }

      
    }
}
