(function () {
    'use strict';
    angular.module('app').service('employeePftService', ['dataConstants', 'apiHttpService', employeePftService]);

    function employeePftService(dataConstants, apiHttpService) {
        var service = {
            getEmployeePfts: getEmployeePfts,
            getEmployeePft: getEmployeePft,
            saveEmployeePft: saveEmployeePft,
            updateEmployeePft: updateEmployeePft,
            deleteEmployeePft: deleteEmployeePft
        };

        return service;
        function getEmployeePfts(pageSize, pageNumber,searchText) {
            var url = dataConstants.EMPLOYEE_PFTS_URL + 'get-employee-pfts?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getEmployeePft(employeePftId) {
            var url = dataConstants.EMPLOYEE_PFTS_URL + 'get-employee-pft?id=' + employeePftId;
            return apiHttpService.GET(url);
        }

        function saveEmployeePft(data) {
            var url = dataConstants.EMPLOYEE_PFTS_URL + 'save-employee-pft';
            return apiHttpService.POST(url, data);
        }

        function updateEmployeePft(employeePftId, data) {
            var url = dataConstants.EMPLOYEE_PFTS_URL + 'update-employee-pft/' + employeePftId;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployeePft(employeePftId) {
            var url = dataConstants.EMPLOYEE_PFTS_URL + 'delete-employee-pft/' + employeePftId;
            return apiHttpService.DELETE(url);
        }
        
    }
})();