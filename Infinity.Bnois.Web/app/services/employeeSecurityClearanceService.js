(function () {
    'use strict';
    angular.module('app').service('employeeSecurityClearanceService', ['dataConstants', 'apiHttpService', employeeSecurityClearanceService]);

    function employeeSecurityClearanceService(dataConstants, apiHttpService) {
        var service = {
            getEmployeeSecurityClearances: getEmployeeSecurityClearances,
            getEmployeeSecurityClearance: getEmployeeSecurityClearance,
            saveEmployeeSecurityClearance: saveEmployeeSecurityClearance,
            updateEmployeeSecurityClearance: updateEmployeeSecurityClearance,
            deleteEmployeeSecurityClearance: deleteEmployeeSecurityClearance
        };

        return service;
        function getEmployeeSecurityClearances(pageSize, pageNumber, searchText) {
            var url = dataConstants.EMPLOYEE_SECURITY_CLEARANCE_URL + 'get-employee-security-clearances?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getEmployeeSecurityClearance(id) {
            var url = dataConstants.EMPLOYEE_SECURITY_CLEARANCE_URL + 'get-employee-security-clearance?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveEmployeeSecurityClearance(data) {
            var url = dataConstants.EMPLOYEE_SECURITY_CLEARANCE_URL + 'save-employee-security-clearance';
            return apiHttpService.POST(url, data);
        }

        function updateEmployeeSecurityClearance(id, data) {
            var url = dataConstants.EMPLOYEE_SECURITY_CLEARANCE_URL + 'update-employee-security-clearance/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployeeSecurityClearance(id) {
            var url = dataConstants.EMPLOYEE_SECURITY_CLEARANCE_URL + 'delete-employee-security-clearance/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();