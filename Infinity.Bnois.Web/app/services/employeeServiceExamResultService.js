(function () {
    'use strict';
    angular.module('app').service('employeeServiceExamResultService', ['dataConstants', 'apiHttpService', employeeServiceExamResultService]);

    function employeeServiceExamResultService(dataConstants, apiHttpService) {
        var service = {
            getEmployeeServiceExamResults: getEmployeeServiceExamResults,
            getEmployeeServiceExamResult: getEmployeeServiceExamResult,
            saveEmployeeServiceExamResult: saveEmployeeServiceExamResult,
            updateEmployeeServiceExamResult: updateEmployeeServiceExamResult,
            deleteEmployeeServiceExamResult: deleteEmployeeServiceExamResult
        };

        return service;
        function getEmployeeServiceExamResults(pageSize, pageNumber,searchText) {
            var url = dataConstants.EMPLOYEE_SERVICE_EXAM_RESULTS_URL + 'get-employee-service-exam-results?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getEmployeeServiceExamResult(employeeServiceExamResultId) {
            var url = dataConstants.EMPLOYEE_SERVICE_EXAM_RESULTS_URL + 'get-employee-service-exam-result?id=' + employeeServiceExamResultId;
            return apiHttpService.GET(url);
        }

        function saveEmployeeServiceExamResult(data) {
            var url = dataConstants.EMPLOYEE_SERVICE_EXAM_RESULTS_URL + 'save-employee-service-exam-result';
            return apiHttpService.POST(url, data);
        }

        function updateEmployeeServiceExamResult(employeeServiceExamResultId, data) {
            var url = dataConstants.EMPLOYEE_SERVICE_EXAM_RESULTS_URL + 'update-employee-service-exam-result/' + employeeServiceExamResultId;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployeeServiceExamResult(employeeServiceExamResultId) {
            var url = dataConstants.EMPLOYEE_SERVICE_EXAM_RESULTS_URL + 'delete-employee-service-exam-result/' + employeeServiceExamResultId;
            return apiHttpService.DELETE(url);
        }
        
    }
})();