(function () {
    'use strict';
    angular.module('app').service('employeeReportService', ['dataConstants', 'apiHttpService', employeeReportService]);

    function employeeReportService(dataConstants, apiHttpService) {
        var service = {
            getEmployeeReports: getEmployeeReports,
            getEmployeeReport: getEmployeeReport,
            saveEmployeeReport: saveEmployeeReport,
            updateEmployeeReport: updateEmployeeReport,
            deleteEmployeeReport: deleteEmployeeReport,
            deleteAllEmployeeReport: deleteAllEmployeeReport, 
            getDownlaodTrace: getDownlaodTrace
        };

        return service;
        function getEmployeeReports() {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'get-employee-reports?';
            return apiHttpService.GET(url);
        }

        function getEmployeeReport(id) {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'get-employee-report?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveEmployeeReport(data) {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'save-employee-report';
            return apiHttpService.POST(url, data);
        }

        
        function updateEmployeeReport(id, data) {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'update-employee-report/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployeeReport(id) {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'delete-employee-report/' + id;
            return apiHttpService.DELETE(url);
        }

        function deleteAllEmployeeReport() {
            var url = dataConstants.EMPLOYEE_RREPORT_URL + 'delete-employee-reports/';
            return apiHttpService.DELETE(url);
        }

        function getDownlaodTrace() {
            return dataConstants.EMPLOYEE_RREPORT_URL + 'download-trace/';
        }
    }
})();