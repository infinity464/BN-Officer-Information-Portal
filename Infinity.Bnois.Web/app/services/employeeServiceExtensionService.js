(function () {
    'use strict';
    angular.module('app').service('employeeServiceExtensionService', ['dataConstants', 'apiHttpService', employeeServiceExtensionService]);

    function employeeServiceExtensionService(dataConstants, apiHttpService) {
        var service = {
            getEmployeeServiceExtensions: getEmployeeServiceExtensions,
            getEmployeeServiceExtension: getEmployeeServiceExtension,
            saveEmployeeServiceExtension: saveEmployeeServiceExtension,
            updateEmployeeServiceExtension: updateEmployeeServiceExtension,
            deleteEmployeeServiceExtension: deleteEmployeeServiceExtension
        };

        return service;
        function getEmployeeServiceExtensions(pageSize, pageNumber, searchText) {
            var url = dataConstants.EMPLOYEE_SERVICE_EXTENSION_URL + 'get-employee-service-extensions?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getEmployeeServiceExtension(id) {
            var url = dataConstants.EMPLOYEE_SERVICE_EXTENSION_URL + 'get-employee-service-extension?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveEmployeeServiceExtension(data) {
            var url = dataConstants.EMPLOYEE_SERVICE_EXTENSION_URL + 'save-employee-service-extension';
            return apiHttpService.POST(url, data);
        }

        function updateEmployeeServiceExtension(id, data) {
            var url = dataConstants.EMPLOYEE_SERVICE_EXTENSION_URL + 'update-employee-service-extension/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployeeServiceExtension(id) {
            var url = dataConstants.EMPLOYEE_SERVICE_EXTENSION_URL + 'delete-employee-service-extension/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();