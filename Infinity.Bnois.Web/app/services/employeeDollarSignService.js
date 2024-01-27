(function () {
    'use strict';
    angular.module('app').service('employeeDollarSignService', ['dataConstants', 'apiHttpService', employeeDollarSignService]);

    function employeeDollarSignService(dataConstants, apiHttpService) {
        var service = {
            getEmployeeDollarSigns: getEmployeeDollarSigns,
            getEmployeeDollarSign: getEmployeeDollarSign,
            saveEmployeeDollarSign: saveEmployeeDollarSign,
            updateEmployeeDollarSign: updateEmployeeDollarSign,
            deleteEmployeeDollarSign: deleteEmployeeDollarSign
        };

        return service;
        function getEmployeeDollarSigns(pageSize, pageNumber, searchText) {
            var url = dataConstants.EMPLOYEE_DOLLAR_SIGN_URL + 'get-employee-dollar-signs?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getEmployeeDollarSign(employeeDollarSignId) {
            var url = dataConstants.EMPLOYEE_DOLLAR_SIGN_URL + 'get-employee-dollar-sign?employeeDollarSignId=' + employeeDollarSignId;
            return apiHttpService.GET(url);
        }

        function saveEmployeeDollarSign(data) {
            var url = dataConstants.EMPLOYEE_DOLLAR_SIGN_URL + 'save-employee-dollar-sign';
            return apiHttpService.POST(url, data);
        }

        function updateEmployeeDollarSign(employeeDollarSignId, data) {
            var url = dataConstants.EMPLOYEE_DOLLAR_SIGN_URL + 'update-employee-dollar-sign/' + employeeDollarSignId;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployeeDollarSign(employeeDollarSignId) {
            var url = dataConstants.EMPLOYEE_DOLLAR_SIGN_URL + 'delete-employee-dollar-sign/' + employeeDollarSignId;
            return apiHttpService.DELETE(url);
        }
    }
})();