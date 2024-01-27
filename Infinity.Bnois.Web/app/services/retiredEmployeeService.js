(function () {
    'use strict';
    angular.module('app').service('retiredEmployeeService', ['dataConstants', 'apiHttpService', retiredEmployeeService]);

    function retiredEmployeeService(dataConstants, apiHttpService) {
        var service = {
            getRetiredEmployees: getRetiredEmployees,
            getRetiredEmployee: getRetiredEmployee,
            updateRetiredEmployee: updateRetiredEmployee
            
        };

        return service;


        function getRetiredEmployees(pageSize, pageNumber, searchText) {
            var url = dataConstants.RETIRED_EMPLOYEE_URL + 'get-retired-employees?ps=' + pageSize + '&pn=' + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getRetiredEmployee(employeeId) {
            var url = dataConstants.RETIRED_EMPLOYEE_URL + 'get-retired-employee?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function updateRetiredEmployee(employeeId, data) {
            var url = dataConstants.RETIRED_EMPLOYEE_URL + 'update-retired-employee/' + employeeId;
            return apiHttpService.PUT(url, data);
        }
       

    }
})();