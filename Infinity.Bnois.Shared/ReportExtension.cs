using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois
{
   public static  class ReportExtension
    {

        public static ByteArrayContent ToByteArray(ReportType type,string reportName, List<ReportParameter> reportParameters,out string extension, out string mimeType)
        {
            Warning[] warnings;
            string[] streamIds;
            string encoding = string.Empty;
            Microsoft.Reporting.WebForms.ReportViewer reportViewer = new ReportViewer();
            string reportServerUrl = ConfigurationManager.AppSettings["ReportServerURL"];
            string domain = ConfigurationManager.AppSettings["rsDomain"];
            string userName = ConfigurationManager.AppSettings["rsUserName"];
            string password = ConfigurationManager.AppSettings["rsPassword"];
            string reportPath = ConfigurationManager.AppSettings["ReportPath"];
            reportViewer.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
            reportViewer.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
            reportViewer.ServerReport.ReportPath = string.Format(reportPath, reportName);
            if (reportParameters.Any())
            {
                reportViewer.ServerReport.SetParameters(reportParameters);
            }
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            byte[] bytes = reportViewer.ServerReport.Render(type.ToString("g"), null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            return new ByteArrayContent(bytes);
        }

        public static byte[] ToByteArray(ReportType type, string reportName, out string extension)
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
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
            byte[] bytes = reportViewer.ServerReport.Render(type.ToString("g"), null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            return bytes;
        }


        public static ByteArrayContent ToByteArrayWithDeviceInfo(ReportType type,double pageHeight, double pageWidth, string reportName, List<ReportParameter> reportParameters, out string extension, out string mimeType)
        {
            string deviceInfo = string.Format("<DeviceInfo><PageHeight>{0}in</PageHeight><PageWidth>{1}in</PageWidth></DeviceInfo>", pageHeight, pageWidth);
            Warning[] warnings;
            string[] streamIds;
            string encoding = string.Empty;
            Microsoft.Reporting.WebForms.ReportViewer reportViewer = new ReportViewer();
            string reportServerUrl = ConfigurationManager.AppSettings["ReportServerURL"];
            string domain = ConfigurationManager.AppSettings["rsDomain"];
            string userName = ConfigurationManager.AppSettings["rsUserName"];
            string password = ConfigurationManager.AppSettings["rsPassword"];
            string reportPath = ConfigurationManager.AppSettings["ReportPath"];
            reportViewer.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
            reportViewer.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
            reportViewer.ServerReport.ReportPath = string.Format(reportPath, reportName);
            if (reportParameters.Any())
            {
                reportViewer.ServerReport.SetParameters(reportParameters);
            }
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            byte[] bytes = reportViewer.ServerReport.Render(type.ToString("g"), deviceInfo, out mimeType, out encoding, out extension, out streamIds, out warnings);
            return new ByteArrayContent(bytes);
        }
    }
}
