<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Viewer.aspx.cs" Inherits="Infinity.Bnois.Api.Web.pages.Viewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms"
    TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-3.2.1.js"></script>
    <script type="text/javascript">
            function PrintReport() { 
                var reportViewerName = 'reportViewer';
                var src_url = $find(reportViewerName)._getInternalViewer().ExportUrlBase + 'PDF';

                var contentDisposition = 'AlwaysInline';
                var src_new = src_url.replace(/(ContentDisposition=).*?(&)/, '$1' + contentDisposition + '$2');

                var iframe = $('<iframe>', {
                    src: src_new,
                    id: 'iframePDF',
                    frameborder: 0,
                    scrolling: 'no'
                });
                $('#pdfPrint').html('');    //There should be a div named "pdfPrint"

                $('#pdfPrint').html(iframe);    //There should be a div named "pdfPrint"

                if (iframe != undefined && iframe.length > 0) {
                    var frame = iframe[0];

                    if (frame != null || frame != undefined) {
                        var contentView = iframe[0].contentWindow;

                        contentView.focus();
                        contentView.print();
                    }
                }
            }

 


    </script>
</head>
<body>
    <input type="button" onclick="PrintReport()" value="Print" style="float: right; background-color: green; color: white; font-weight: bold;"/>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div style="height: 600px;">
            <rsweb:ReportViewer ID="reportViewer" runat="server" Width="100%" Height="100%" ShowPrintButton="True"></rsweb:ReportViewer>
        </div>
    </form>
<div id="pdfPrint" style="display: none"></div>

</body>
</html>
