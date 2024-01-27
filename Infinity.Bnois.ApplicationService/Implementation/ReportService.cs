using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class ReportService : IReportService
    {
        private readonly IBnoisRepository<NominationDetail> nominationDetailRepository;
        private readonly IBnoisRepository<PromotionNomination> promotionNominationRepository;
        private readonly IBnoisRepository<EmployeeReport> employeeReportRepository;
        public ReportService(IBnoisRepository<NominationDetail> nominationDetailRepository,
            IBnoisRepository<EmployeeReport> employeeReportRepository,
            IBnoisRepository<PromotionNomination> promotionNominationRepository)
        {
            this.nominationDetailRepository = nominationDetailRepository;
            this.employeeReportRepository = employeeReportRepository;
            this.promotionNominationRepository = promotionNominationRepository;
        }

        public async Task<bool> ExecutePlanForBroadSheetForeignCourseMissionVisit(int nominationId)
        {
            string userId = Configuration.ConfigurationResolver.Get().LoggedInUser.UserId;
            ICollection<NominationDetail> nominationDetails = await nominationDetailRepository.FilterAsync(x => x.NominationId == nominationId);
            if (!nominationDetails.Any())
            {
                throw new InfinityInvalidDataException("Officer not added to Nomination!");
            }
            List<EmployeeReport> employeeReports =  employeeReportRepository.Where(x => x.UserId == userId).ToList();
            if (employeeReports.Any())
            {
                employeeReportRepository.RemoveRange(employeeReports);
                employeeReports =new List<EmployeeReport>();
            }

            foreach (var detail in nominationDetails.ToList())
            {
                EmployeeReport employeeReport = new EmployeeReport()
                {
                    EmployeeId = detail.EmployeeId,
                    UserId = userId
                };

                employeeReports.Add(employeeReport);
            }
            await employeeReportRepository.SaveAllAsync(employeeReports);
            return employeeReports.Count() > 1;
        }

        public async Task<bool> ExecutePlanPromotionBoard(int promotionBoardId)
        {
            string userId = Configuration.ConfigurationResolver.Get().LoggedInUser.UserId;
            ICollection<PromotionNomination> promotionNominations = await promotionNominationRepository.FilterAsync(x => x.PromotionBoardId == promotionBoardId);
            if (!promotionNominations.Any())
            {
                throw new InfinityInvalidDataException("Officer not added to Nomination!");
            }
            List<EmployeeReport> employeeReports = employeeReportRepository.Where(x => x.UserId == userId).ToList();
            if (employeeReports.Any())
            {
                employeeReportRepository.RemoveRange(employeeReports);
                employeeReports = null;
            }

            foreach (var detail in promotionNominations.ToList())
            {
                EmployeeReport employeeReport = new EmployeeReport()
                {
                    EmployeeId = detail.EmployeeId,
                    UserId = userId
                };

                employeeReports.Add(employeeReport);
            }
            await employeeReportRepository.SaveAllAsync(employeeReports);
            return employeeReports.Count() > 1;
        }
    }
}
