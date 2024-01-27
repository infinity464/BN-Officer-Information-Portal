(function () {
    'use strict';
    angular.module('app').service('graphicalReportService', ['dataConstants', 'apiHttpService', graphicalReportService]);

    function graphicalReportService(dataConstants, apiHttpService) {
        var service = {
            getGraphicalReports: getGraphicalReports,
            getGraphicalReport: getGraphicalReport,
            getLastOPRChart: getLastOPRChart,
            getOPRYearlyChart: getOPRYearlyChart,
            getSeaServiceChart: getSeaServiceChart,
            getSeaCommandServiceChart: getSeaCommandServiceChart,
            getCourseResultChart: getCourseResultChart,
            getTraceChart: getTraceChart,
            saveGraphicalReport: saveGraphicalReport,
            saveGraphReport: saveGraphReport,
            updateGraphicalReport: updateGraphicalReport,
            deleteGraphicalReport: deleteGraphicalReport,
            deleteAllGraphicalReport: deleteAllGraphicalReport 
          
        };

        return service;
        function getGraphicalReports() {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'get-employee-reports';
            return apiHttpService.GET(url);
        }

        function getGraphicalReport(id) {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'get-employee-report?id=' + id;
            return apiHttpService.GET(url);
        }

        function getLastOPRChart(lastOprNo) {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'get-last-opr-chart?lastOprNo=' + lastOprNo;
            return apiHttpService.GET(url);
        }
        function getCourseResultChart(categoryId,subCategoryId,venue) {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'get-course-result-chart?categoryId=' + categoryId + '&subCategoryId=' + subCategoryId + '&venue=' + venue;
            return apiHttpService.GET(url);
        }
        function getTraceChart() {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'get-trace-chart';
            return apiHttpService.GET(url);
        }
        function getOPRYearlyChart(fromYear, toYear) {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'get-opr-yearly-chart?fromYear=' + fromYear + '&toYear=' + toYear;
            return apiHttpService.GET(url);
        }
        function getSeaServiceChart() {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'get-sea-service-chart';
            return apiHttpService.GET(url);
        }
        function getSeaCommandServiceChart() {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'get-sea-command-service-chart';
            return apiHttpService.GET(url);
        }

        function saveGraphicalReport(data) {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'save-employee-report';
            return apiHttpService.POST(url, data);
        }


        function saveGraphReport(fromYear, toYear) {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'save-graphical-report?fromYear=' + fromYear + '&toYear=' + toYear;
            return apiHttpService.POST(url, {});
        }
        function updateGraphicalReport(id, data) {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'update-employee-report/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteGraphicalReport(id) {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'delete-employee-report/' + id;
            return apiHttpService.DELETE(url);
        }

        function deleteAllGraphicalReport() {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'delete-employee-reports/';
            return apiHttpService.DELETE(url);
        }

       
    }
})();