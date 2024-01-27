

using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;

namespace Infinity.Bnois.Api.Web.pages
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Viewer'
    public partial class Viewer : System.Web.UI.Page
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Viewer'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Viewer.Page_Load(object, EventArgs)'
        protected void Page_Load(object sender, EventArgs e)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Viewer.Page_Load(object, EventArgs)'
        {
            try
            {
                if (!IsPostBack)
                {
                    string reportId = Request.QueryString["id"].ToString();
                    string reportName = Request.QueryString["reportName"].ToString();

                    string reportServerUrl = ConfigurationManager.AppSettings["ReportServerURL"];
                    string domain = ConfigurationManager.AppSettings["rsDomain"];
                    string userName = ConfigurationManager.AppSettings["rsUserName"];
                    string password = ConfigurationManager.AppSettings["rsPassword"];
                    string reportPath = ConfigurationManager.AppSettings["ReportPath"];
                    reportViewer.ServerReport.DisplayName = reportName;
                    reportViewer.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
                    reportViewer.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
                    reportViewer.ServerReport.ReportPath = string.Format(reportPath, reportId);
                    reportViewer.ProcessingMode = ProcessingMode.Remote;
                    reportViewer.ShowCredentialPrompts = false;
                    reportViewer.ServerReport.Refresh();




                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}