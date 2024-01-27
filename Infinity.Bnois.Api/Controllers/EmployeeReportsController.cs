using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration.Models;
using Infinity.Bnois.ExceptionHelper;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.EmployeeReports)]
    [EnableCors("*", "*", "*")]
    public class EmployeeReportsController : BaseController
    {
        private readonly IEmployeeReportService employeeReportService;
        private readonly IEducationService educationService;
        private readonly ICourseCategoryService courseCategoryService;
       
        public EmployeeReportsController(IEmployeeReportService employeeReportService, IEducationService educationService, ICourseCategoryService courseCategoryService)
        {
            this.employeeReportService = employeeReportService;
            this.educationService = educationService;
            this.courseCategoryService = courseCategoryService;
            
        }
        [HttpGet]
        [Route("get-employee-reports")]
        public  async Task<IHttpActionResult> GetEmployeeReports()
        {
            EmployeeReportViewModel vm = new EmployeeReportViewModel();
            vm.EmployeeReports = employeeReportService.GetEmployeeReports();
            vm.PassingYears = educationService.GetYearSelectModel();
            vm.CourseCategories = await courseCategoryService.GetCourseCategorySelectModels();
            return Ok(new ResponseMessage<EmployeeReportViewModel>()
            {
                Result = vm
            });
        }

        [HttpGet]
        [Route("get-employee-report")]
        public async Task<IHttpActionResult> GetEmployeeReport(int id)
        {
            EmployeeReportModel model = await employeeReportService.GetEmployeeReport(id);
            return Ok(new ResponseMessage<EmployeeReportModel>
            {
                Result = model
            });
        }



        [HttpGet]
        [Route("get-last-opr-chart")]
        public IHttpActionResult GetLastOPRChart(int lastOprNo)
        {
            return Ok(new ResponseMessage<Chart>
            {
                Result = employeeReportService.GetLastOPRChart(lastOprNo,base.UserId)
         });
        }

        [HttpGet]
        [Route("get-opr-yearly-chart")]
        public IHttpActionResult GetOprYearlyChart(int fromYear,int toYear)
        {
            return Ok(new ResponseMessage<Chart>
            {
                Result = employeeReportService.GetOprYearlyChart(fromYear,toYear,base.UserId)
         });
        }

        [HttpGet]
        [Route("get-sea-service-chart")]
        public IHttpActionResult GetSeaServiceChart()
        {
            return Ok(new ResponseMessage<Chart>
            {
                Result = employeeReportService.GetSeaServiceChart( base.UserId)
            });
        }


        [HttpGet]
        [Route("get-sea-command-service-chart")]
        public IHttpActionResult GetSeaCommandServiceChart()
        {
            return Ok(new ResponseMessage<Chart>
            {
                Result = employeeReportService.GetSeaCommandServiceChart(base.UserId)
            });
        }

        [HttpGet]
        [Route("get-trace-chart")]
        public IHttpActionResult GetTraceChart()
        {
            return Ok(new ResponseMessage<Chart>
            {
                Result = employeeReportService.GetTraceChart(base.UserId)
            });
        }
        [HttpGet]
        [Route("get-course-result-chart")]
        public IHttpActionResult GetCourseResultChart(int categoryId, int? subCategoryId,int venue)
        {
            return Ok(new ResponseMessage<Chart>
            {
                Result = employeeReportService.GetCourseResultChart(categoryId, subCategoryId, venue, base.UserId)
            });
        }



        [HttpPost]
        [ModelValidation]
        [Route("save-employee-report")]
        public async Task<IHttpActionResult> SaveEmployeeReport([FromBody] EmployeeReportModel model)
        {
            return Ok(new ResponseMessage<EmployeeReportModel>
            {
                Result = await employeeReportService.SaveEmployeeReport(0, model)
            });
        }

        [HttpPut]
        [Route("update-employee-report/{id}")]
        public async Task<IHttpActionResult> UpdateEmployeeReport(int id, [FromBody] EmployeeReportModel model)
        {
            return Ok(new ResponseMessage<EmployeeReportModel>
            {
                Result = await employeeReportService.SaveEmployeeReport(id, model)
            });
        }



        [HttpDelete]
        [Route("delete-employee-report/{id}")]
        public async Task<IHttpActionResult> DeleteEmployeeReport(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await employeeReportService.DeleteEmployeeReport(id)
            });
        }

        [HttpDelete]
        [Route("delete-employee-reports")]
        public async Task<IHttpActionResult> DeleteEmployeeReports()
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = employeeReportService.DeleteEmployeeReports()
            });
        }



        [HttpPost]
        [Route("save-graphical-report")]
        public IHttpActionResult SaveGraphicalReport(int fromYear,int toYear)
        {
            
           int result=  employeeReportService.SaveGraphicalReport(fromYear,toYear, base.UserId);

            return Ok(new ResponseMessage<int>()
            {
                Result = result

            });
        }





        [HttpGet]
        [Route("download-trace")]
        public HttpResponseMessage DownlaodTraceReport()
        {

            var reportData = GetTraceReport();
            HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(reportData);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
            result.Content.Headers.ContentDisposition.FileName = "TrachReport.pdf";
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return result;
        }

       
        private byte[] GetTraceReport()
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string reportName = "TraceCalulation";
            Microsoft.Reporting.WebForms.ReportViewer reportViewer = new ReportViewer();
            string reportServerUrl = ConfigurationManager.AppSettings["ReportServerURL"];
            string domain = ConfigurationManager.AppSettings["rsDomain"];
            string userName = ConfigurationManager.AppSettings["rsUserName"];
            string password = ConfigurationManager.AppSettings["rsPassword"];
            string reportPath = ConfigurationManager.AppSettings["ReportPath"];
            reportViewer.ServerReport.ReportServerUrl = new Uri(reportServerUrl);

            reportViewer.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
            reportViewer.ServerReport.ReportPath = string.Format(reportPath, reportName);
 
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            byte[] bytes = reportViewer.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            return bytes;
        }
    }

}
