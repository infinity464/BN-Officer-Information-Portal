using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Reports)]
    [EnableCors("*", "*", "*")]
    public class ReportsController : BaseController
    {
        private readonly IAdvanceSearchService advanceSearchService;
        private readonly IReportService reportService;
        private readonly INominationDetailService nominationDetailService;
        private readonly ITransferProposalService transferProposalService;
        private readonly IPromotionBoardService promotionBoardService;
        public ReportsController(IPromotionBoardService promotionBoardService, ITransferProposalService transferProposalService, IAdvanceSearchService advanceSearchService, IReportService reportService, INominationDetailService nominationDetailService)
        {
            this.advanceSearchService = advanceSearchService;
            this.reportService = reportService;
            this.nominationDetailService = nominationDetailService;
            this.transferProposalService = transferProposalService;
            this.promotionBoardService = promotionBoardService;
        }

        [HttpGet]
        [Route("download-broad-sheet-foreign-course-visit-mission")]
        public async Task<HttpResponseMessage> DownlaodBroadSheetForeignCourseVisitMission(int nominationId, ReportType type)
        {
            string extension = string.Empty;
            string mimeType = string.Empty;
            string reportName = "BroadSheetForNominationCourseVisitMission";
            var parms = new List<ReportParameter> { new ReportParameter("NominationId", nominationId.ToString()), new ReportParameter("TableId", "1") };
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                Content = ReportExtension.ToByteArray(type, reportName, parms, out extension, out mimeType)
            };
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = reportName + "." + extension
            };
            httpResponseMessage.Content.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            return httpResponseMessage;
        }

        //[HttpGet]
        //[Route("download-broad-sheet-foreign-course-visit-mission/{nominationId}")]
        //public async Task<HttpResponseMessage> DownlaodBroadSheetForeignCourseVisitMission(int nominationId)
        //{
        //    bool resultExecutePlan = await reportService.ExecutePlanForBroadSheetForeignCourseMissionVisit(nominationId);
        //    var reportData = GetBroadSheetForeignCourseVisitMission(nominationId);
        //    HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK);
        //    result.Content = new ByteArrayContent(reportData);
        //    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
        //    result.Content.Headers.ContentDisposition.FileName = "BroadSheetForNominationCourseVisitMission.pdf";
        //    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
        //    return result;
        //}

        //private byte[] GetBroadSheetForeignCourseVisitMission(int id)
        //{
        //    Warning[] warnings;
        //    string[] streamIds;
        //    string mimeType = string.Empty;
        //    string encoding = string.Empty;
        //    string extension = string.Empty;
        //    string reportName = "BroadSheetForNominationCourseVisitMission";
        //    Microsoft.Reporting.WebForms.ReportViewer reportViewer = new ReportViewer();
        //    string reportServerUrl = ConfigurationManager.AppSettings["ReportServerURL"];
        //    string domain = ConfigurationManager.AppSettings["rsDomain"];
        //    string userName = ConfigurationManager.AppSettings["rsUserName"];
        //    string password = ConfigurationManager.AppSettings["rsPassword"];
        //    string reportPath = ConfigurationManager.AppSettings["ReportPath"];
        //    reportViewer.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
        //    reportViewer.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
        //    reportViewer.ServerReport.ReportPath = string.Format(reportPath, reportName);
        //   reportViewer.ServerReport.SetParameters(new List<ReportParameter> {
        //      new ReportParameter ("Id" , id.ToString()), new ReportParameter("TableId","1") });

        //    reportViewer.ProcessingMode = ProcessingMode.Remote;
        //    byte[] bytes = reportViewer.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        //    return bytes;
        //}




        [HttpGet]
        [Route("download-sasb-for-promotion")]
        public async Task<HttpResponseMessage> DownloadSasbReport(int promotionBoardId, ReportType type)
        {
            string extension = string.Empty;
            string mimeType = string.Empty;
            string reportName = "BroadSheetSASB";
            var parms = new List<ReportParameter> { new ReportParameter("PromotionBoardId", promotionBoardId.ToString()) };
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                Content = ReportExtension.ToByteArray(type, reportName, parms, out extension, out mimeType)
            };
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = reportName + "." + extension
            };
            httpResponseMessage.Content.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            return httpResponseMessage;
        }

        [HttpGet]
        [Route("download-trace-for-promotion")]
        public async Task<HttpResponseMessage> DownloadPromotionBoardTraceReport(int promotionBoardId, ReportType type)
        {
            string extension = string.Empty;
            string mimeType = string.Empty;
            string reportName = "TraceCalulation";
            var parms = new List<ReportParameter> { new ReportParameter("PromotionBoardId", promotionBoardId.ToString()) };
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                Content = ReportExtension.ToByteArray(type, reportName, parms, out extension, out mimeType)
            };
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = reportName + "." + extension
            };
            httpResponseMessage.Content.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            return httpResponseMessage;
        }


        [HttpGet]
        [Route("download-promotion-personal-information")]
        public async Task<HttpResponseMessage> DownloadPromotionPersoanlReport(int promotionBoardId, ReportType type)
        {
            string extension = string.Empty;
            string mimeType = string.Empty;
            string reportName = "TracePersoanlInfo";
            var parms = new List<ReportParameter> { new ReportParameter("PromotionBoardId", promotionBoardId.ToString()) };
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                Content = ReportExtension.ToByteArray(type, reportName, parms, out extension, out mimeType)
            };
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = reportName + "." + extension
            };
            httpResponseMessage.Content.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            return httpResponseMessage;
        }




        [HttpGet]
        [Route("download-promotion-broadsheet")]
        public async Task<HttpResponseMessage> DownloadPromotionBoardSheetReport(int promotionBoardId, ReportType type)
        {
            string extension = string.Empty;
            string mimeType = string.Empty;
            string reportName = string.Empty;
            PromotionBoardModel promotionBoard = await promotionBoardService.GetPromotionBoard(promotionBoardId);

            if (promotionBoard.LtCdrLevel==1)
            {
                reportName = "BroadSheetPromotion";
            }
            else
            {
                reportName = "BroadSheetPromotionLtAndCdreBelow";
            }
         
            var parms = new List<ReportParameter> { new ReportParameter("PromotionBoardId", promotionBoardId.ToString()) };
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                Content = ReportExtension.ToByteArray(type, reportName, parms, out extension, out mimeType)
            };
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = reportName + "." + extension
            };
            httpResponseMessage.Content.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            return httpResponseMessage;
        }


        [HttpGet]
        [Route("download-transfer-proposal")]
        public async Task<HttpResponseMessage> DownloadTransferProposal(int transferProposalId, ReportType type)
        {
            TransferProposalModel transferProposal = await transferProposalService.GetTransferProposal(transferProposalId);
            string extension = string.Empty;
            string mimeType = string.Empty;
            string reportName = string.Empty;
            if(transferProposal.LtCdrLevel == 1 && transferProposal.WithPicture)
                reportName = "TransferProposalLtCdrAboveSpecial";
            else if(transferProposal.LtCdrLevel == 1)
                reportName = "TransferProposalLtCdrAbove";
            else if (transferProposal.LtCdrLevel == 2 && transferProposal.WithPicture)
                reportName = "TransferProposalSpecial";
            else if(transferProposal.LtCdrLevel == 2)
                reportName = "TransferProposal";
            //if (transferProposal.WithPicture)
            //{
            //    reportName = "TransferProposalLtCdrAboveSpecial";
            //}
            //else
            //{
            //    if (transferProposal.LtCdrLevel == 1)
            //    {
            //        reportName = "TransferProposal";
            //    }
            //    else
            //    {
            //        reportName = "TransferProposalLtCdrAbove";
            //    }
            //}
            var parms = new List<ReportParameter> { new ReportParameter("TransferProposalId", transferProposalId.ToString()) };
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                Content = ReportExtension.ToByteArray(type, reportName, parms, out extension, out mimeType)
            };
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = reportName + "." + extension
            };
            httpResponseMessage.Content.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            return httpResponseMessage;

        }

        [HttpGet]
        [Route("download-search-result")]
        public async Task<HttpResponseMessage> DownloadSearchResult(string header, ReportType type, PageOrientation orientation)
        {

            string extension = string.Empty;
            string mimeType = string.Empty;
            string reportName = "AdvanceSearchResult";

            var parms = new List<ReportParameter> { new ReportParameter("HeaderTitle", header ?? "Advance Search Result"), new ReportParameter("UserId", base.UserId) };
            List<string> checkedColumns = advanceSearchService.SelectCheckedColumn(base.UserId);
            foreach (var column in checkedColumns)
            {
                parms.Add(new ReportParameter(column, "True"));
            }


            double height = 11.69;
            double width = 8.7;
            if (orientation == PageOrientation.Landscape)
            {
                height = 8.9;
                width = 12;
            }
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                Content = ReportExtension.ToByteArrayWithDeviceInfo(type, height, width, reportName, parms, out extension, out mimeType)
            };
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            var fileName = header + "." + extension;
            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };
            httpResponseMessage.Content.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            return httpResponseMessage;
        }



        [HttpGet]
        [Route("download-graphical-opr-list-report")]
        public async Task<HttpResponseMessage> DownloadGraphicalOprListReport(string lastOprNo)
        {

            string extension = string.Empty;
            string mimeType = string.Empty;
            string reportName = "OprGraphicalReportList";

            var parms = new List<ReportParameter> { new ReportParameter("LastOprNo", lastOprNo), new ReportParameter("UserId", base.UserId) };

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                Content = ReportExtension.ToByteArray(ReportType.Pdf,reportName, parms, out extension, out mimeType)
            };
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = reportName + "." + extension
            };
            httpResponseMessage.Content.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            return httpResponseMessage;
        }

        [HttpGet]
        [Route("download-graphical-opr-yearly-report")]
        public async Task<HttpResponseMessage> DownloadGraphicalOprYearlyReport(string fromYear, string toYear)
        {

            string extension = string.Empty;
            string mimeType = string.Empty;
            string reportName = "OprGraphicalReportYearly";

            var parms = new List<ReportParameter> { new ReportParameter("FromYear", fromYear), new ReportParameter("ToYear", toYear), new ReportParameter("UserId", base.UserId) };

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                Content = ReportExtension.ToByteArray(ReportType.Pdf,reportName, parms, out extension, out mimeType)
            };
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = reportName + "." + extension
            };
            httpResponseMessage.Content.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            return httpResponseMessage;
        }




        [HttpGet]
        [Route("download-graphical-trace-report")]
        public async Task<HttpResponseMessage> DownloadGraphicalTraceReport(string fromYear, string toYear)
        {

            string extension = string.Empty;
            string mimeType = string.Empty;
            string reportName = "TraceGraphicalReport";

            var parms = new List<ReportParameter> { new ReportParameter("FromYear", fromYear), new ReportParameter("ToYear", toYear), new ReportParameter("UserId", base.UserId) };

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                Content = ReportExtension.ToByteArray(ReportType.Pdf, reportName, parms, out extension, out mimeType)
            };
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = reportName + "." + extension
            };
            httpResponseMessage.Content.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            return httpResponseMessage;
        }



        [HttpGet]
        [Route("download-graphical-sea-service-report")]
        public async Task<HttpResponseMessage> DownloadGraphicalSeaServiceReport()
        {

            string extension = string.Empty;
            string mimeType = string.Empty;
            string reportName = "SeaServiceGraphicalReport";

            var parms = new List<ReportParameter> {  new ReportParameter("UserId", base.UserId) };

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                Content = ReportExtension.ToByteArray(ReportType.Pdf, reportName, parms, out extension, out mimeType)
            };
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = reportName + "." + extension
            };
            httpResponseMessage.Content.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            return httpResponseMessage;
        }



        [HttpGet]
        [Route("download-graphical-sea-command-service-report")]
        public async Task<HttpResponseMessage> DownloadGraphicalSeaCommandServiceReport()
        {

            string extension = string.Empty;
            string mimeType = string.Empty;
            string reportName = "SeaCommandServiceGraphicalReport";

            var parms = new List<ReportParameter> {  new ReportParameter("UserId", base.UserId) };

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                Content = ReportExtension.ToByteArray(ReportType.Pdf, reportName, parms, out extension, out mimeType)
            };
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = reportName + "." + extension
            };
            httpResponseMessage.Content.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            return httpResponseMessage;
        }

    }
}
