(function () {
    'use strict';
    angular.module('app').service('reportService', ['dataConstants', 'apiHttpService', reportService]);

    function reportService(dataConstants, apiHttpService) {
        var service = {
            downlaodBroadSheetForeignCourseVisitMission: downlaodBroadSheetForeignCourseVisitMission,
            downloadPromotionBoardTraceReport: downloadPromotionBoardTraceReport,
            downlaodTransferProposalUrl: downlaodTransferProposalUrl,
            downloadReportUrl: downloadReportUrl,
            downloadPromotionBroadSheetReportUrl: downloadPromotionBroadSheetReportUrl,
            downloadPersonalReport: downloadPersonalReport,
            downloadSASBBoardSheetReportUrl: downloadSASBBoardSheetReportUrl,
            downloadSearchResult: downloadSearchResult,
            downloadGraphicalOprListReport: downloadGraphicalOprListReport,
            downloadGraphicalOprYearlyReport: downloadGraphicalOprYearlyReport,
            downloadGraphicalTraceReport: downloadGraphicalTraceReport,
            downloadGraphicalSeaServiceReport: downloadGraphicalSeaServiceReport,
            downloadGraphicalSeaCommandServiceReport: downloadGraphicalSeaCommandServiceReport
        };
        return service;
        function downlaodBroadSheetForeignCourseVisitMission(nominationId, reportType) {
            return dataConstants.REPORT_URL + 'download-broad-sheet-foreign-course-visit-mission?nominationId=' + nominationId + '&type=' + reportType;
        }
        function downloadReportUrl(promotionBoardId, reportType, type) {
            if (type == 1) {
                return dataConstants.REPORT_URL + 'download-trace-for-promotion?promotionBoardId=' + promotionBoardId + '&type=' + reportType;
            }
            if (type == 2) {
                return dataConstants.REPORT_URL + 'download-sasb-for-promotion?promotionBoardId=' + promotionBoardId + '&type=' + reportType;
            }
        }

        function downloadPromotionBroadSheetReportUrl(promotionBoardId, reportType) {
            return dataConstants.REPORT_URL + 'download-promotion-broadsheet?promotionBoardId=' + promotionBoardId + '&type=' + reportType;
        }

        function downloadPersonalReport(promotionBoardId, reportType) {
            return dataConstants.REPORT_URL + 'download-promotion-personal-information?promotionBoardId=' + promotionBoardId + '&type=' + reportType;
        }

        

        function downloadSASBBoardSheetReportUrl(promotionBoardId, reportType) {
            return dataConstants.REPORT_URL + 'download-sasb-for-promotion?promotionBoardId=' + promotionBoardId + '&type=' + reportType;
        }

        function downloadPromotionBoardTraceReport(promotionBoardId, reportType) {
            return dataConstants.REPORT_URL + 'download-trace-for-promotion?promotionBoardId=' + promotionBoardId + '&type=' + reportType;
        }
        function downlaodTransferProposalUrl(transferProposalId, reportType) {
            return dataConstants.REPORT_URL + 'download-transfer-proposal?transferProposalId=' + transferProposalId + '&type=' + reportType;
        }

        function downloadSearchResult(header, type,orientation) {
            var url = dataConstants.REPORT_URL + 'download-search-result?header=' + header + '&type=' + type + '&orientation=' + orientation;
            return url;
        }


        function downloadGraphicalOprListReport(lastOprNo) {
            var url = dataConstants.REPORT_URL + 'download-graphical-opr-list-report?lastOprNo=' + lastOprNo;
            return url;
        }

        function downloadGraphicalOprYearlyReport(fromYear, toYear) {
            var url = dataConstants.REPORT_URL + 'download-graphical-opr-yearly-report?fromYear=' + fromYear + '&toYear=' + toYear;
            return url;
        }

        function downloadGraphicalTraceReport(fromYear, toYear) {
            var url = dataConstants.REPORT_URL + 'download-graphical-trace-report?fromYear=' + fromYear + '&toYear=' + toYear;
            return url;
        }
      
        function downloadGraphicalSeaServiceReport() {
            var url = dataConstants.REPORT_URL + 'download-graphical-sea-service-report';
            return url;
        }
        function downloadGraphicalSeaCommandServiceReport() {
            var url = dataConstants.REPORT_URL + 'download-graphical-sea-command-service-report';
            return url;
        }
        

    }
})();