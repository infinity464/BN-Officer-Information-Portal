(function () {
    'use strict';
    angular.module('app').service('employeeSportService', ['dataConstants', 'apiHttpService', employeeSportService]);

    function employeeSportService(dataConstants, apiHttpService) {
        var service = {
            getEmployeeSports: getEmployeeSports,
            getEmployeeSport: getEmployeeSport,
            saveEmployeeSport: saveEmployeeSport,
            updateEmployeeSport: updateEmployeeSport,
            deleteEmployeeSport: deleteEmployeeSport
        };

        return service;


        function getEmployeeSports(employeeId) {
            var url = dataConstants.EMPLOYEE_SPORT_URL + 'get-employee-sports?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function getEmployeeSport(employeeId, employeeSportId) {
            var url = dataConstants.EMPLOYEE_SPORT_URL + 'get-employee-sport?employeeId=' + employeeId + '&employeeSportId=' + employeeSportId;
            return apiHttpService.GET(url);
        }

        function saveEmployeeSport(employeeId, data) {
            var url = dataConstants.EMPLOYEE_SPORT_URL + 'save-employee-sport/' + employeeId;
            return apiHttpService.POST(url, data);
        }
        function updateEmployeeSport(employeeSportId, data) {
            var url = dataConstants.EMPLOYEE_SPORT_URL + 'update-employee-sport/' + employeeSportId;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployeeSport(id) {
            var url = dataConstants.EMPLOYEE_SPORT_URL + 'delete-employee-sport/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();