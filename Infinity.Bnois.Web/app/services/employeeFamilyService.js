(function () {
    'use strict';
    angular.module('app').service('employeeFamilyService', ['dataConstants', 'apiHttpService', employeeFamilyService]);

    function employeeFamilyService(dataConstants, apiHttpService) {
        var service = {
            getEmployeeFamilies: getEmployeeFamilies,
            getEmployeeFamily: getEmployeeFamily,
            saveEmployeeFamily: saveEmployeeFamily,
            updateEmployeeFamily: updateEmployeeFamily,
            deleteEmployeeFamily: deleteEmployeeFamily
        };

        return service;
        function getEmployeeFamilies(employeeId, pageSize, pageNumber, searchText) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'get-employee-families?employeeId=' + employeeId + '&ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getEmployeeFamily(employeeFamilyId, employeeId) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'get-employee-family?employeeFamilyId=' + employeeFamilyId + "&employeeId=" + employeeId;
            return apiHttpService.GET(url);
        }

        function saveEmployeeFamily(data) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'save-employee-family';
            return apiHttpService.POST(url, data);
        }

        function updateEmployeeFamily(employeeFamily, data) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'update-employee-family/' + employeeFamily;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployeeFamily(employeeFamily) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'delete-employee-family/' + employeeFamily;
            return apiHttpService.DELETE(url);
        }

    }
})(); (function () {
    'use strict';
    angular.module('app').service('employeeFamilyService', ['dataConstants', 'apiHttpService', employeeFamilyService]);

    function employeeFamilyService(dataConstants, apiHttpService) {
        var service = {
            getEmployeeFamilies: getEmployeeFamilies,
            getEmployeeFamily: getEmployeeFamily,
            saveEmployeeFamily: saveEmployeeFamily,
            updateEmployeeFamily: updateEmployeeFamily,
            deleteEmployeeFamily: deleteEmployeeFamily
        };

        return service;
        function getEmployeeFamilies(employeeId, pageSize, pageNumber, searchText) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'get-employee-families?employeeId=' + employeeId + '&ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getEmployeeFamily(employeeFamilyId, employeeId) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'get-employee-family?employeeFamilyId=' + employeeFamilyId + "&employeeId=" + employeeId;
            return apiHttpService.GET(url);
        }

        function saveEmployeeFamily(data) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'save-employee-family';
            return apiHttpService.POST(url, data);
        }

        function updateEmployeeFamily(employeeFamily, data) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'update-employee-family/' + employeeFamily;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployeeFamily(employeeFamily) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'delete-employee-family/' + employeeFamily;
            return apiHttpService.DELETE(url);
        }

    }
})(); (function () {
    'use strict';
    angular.module('app').service('employeeFamilyService', ['dataConstants', 'apiHttpService', employeeFamilyService]);

    function employeeFamilyService(dataConstants, apiHttpService) {
        var service = {
            getEmployeeFamilies: getEmployeeFamilies,
            getEmployeeFamily: getEmployeeFamily,
            saveEmployeeFamily: saveEmployeeFamily,
            updateEmployeeFamily: updateEmployeeFamily,
            deleteEmployeeFamily: deleteEmployeeFamily
        };

        return service;
        function getEmployeeFamilies(employeeId, pageSize, pageNumber, searchText) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'get-employee-families?employeeId=' + employeeId + '&ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getEmployeeFamily(employeeFamilyId, employeeId) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'get-employee-family?employeeFamilyId=' + employeeFamilyId + "&employeeId=" + employeeId;
            return apiHttpService.GET(url);
        }

        function saveEmployeeFamily(data) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'save-employee-family';
            return apiHttpService.POST(url, data);
        }

        function updateEmployeeFamily(employeeFamily, data) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'update-employee-family/' + employeeFamily;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployeeFamily(employeeFamily) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'delete-employee-family/' + employeeFamily;
            return apiHttpService.DELETE(url);
        }

    }
})(); (function () {
    'use strict';
    angular.module('app').service('employeeFamilyService', ['dataConstants', 'apiHttpService', employeeFamilyService]);

    function employeeFamilyService(dataConstants, apiHttpService) {
        var service = {
            getEmployeeFamilies: getEmployeeFamilies,
            getEmployeeFamily: getEmployeeFamily,
            saveEmployeeFamily: saveEmployeeFamily,
            updateEmployeeFamily: updateEmployeeFamily,
            deleteEmployeeFamily: deleteEmployeeFamily
        };

        return service;
        function getEmployeeFamilies(employeeId, pageSize, pageNumber, searchText) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'get-employee-families?employeeId=' + employeeId + '&ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getEmployeeFamily(employeeFamilyId, employeeId) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'get-employee-family?employeeFamilyId=' + employeeFamilyId + "&employeeId=" + employeeId;
            return apiHttpService.GET(url);
        }

        function saveEmployeeFamily(data) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'save-employee-family';
            return apiHttpService.POST(url, data);
        }

        function updateEmployeeFamily(employeeFamily, data) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'update-employee-family/' + employeeFamily;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployeeFamily(employeeFamily) {
            var url = dataConstants.EMPLOYEE_FAMILY_URL + 'delete-employee-family/' + employeeFamily;
            return apiHttpService.DELETE(url);
        }

    }
})();