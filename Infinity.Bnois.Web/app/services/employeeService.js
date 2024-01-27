(function () {
    'use strict';
    angular.module('app').service('employeeService', ['dataConstants', 'apiHttpService', employeeService]);

    function employeeService(dataConstants, apiHttpService) {
        var service = {
            getEmployees: getEmployees,
            getEmployee: getEmployee,
            saveEmployee: saveEmployee,
            updateEmployee: updateEmployee,
            deleteEmployee: deleteEmployee,
            getEmployeeByPno: getEmployeeByPno,
            GetLoginEmployeePno: GetLoginEmployeePno,
            getEmployeesByDollarSign: getEmployeesByDollarSign,
            getEmployeeByDollarSign: getEmployeeByDollarSign,
            updateEmployeeDollarSign: updateEmployeeDollarSign,
            deleteEmployeeDollarSign: deleteEmployeeDollarSign,
            getEmployeebyName: getEmployeebyName,
            getEmployeeByPno1: getEmployeeByPno1
        };

        return service;
        function getEmployees(pageSize, pageNumber, searchText) {
            var url = dataConstants.EMPLOYEE_URL + 'get-employees?ps=' + pageSize + '&pn=' + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }
           function getEmployeebyName(name) {
               var url = dataConstants.EMPLOYEE_URL + 'get-employeesByName?qs=' + name;
            return apiHttpService.GET(url);
        }
        function getEmployee(employeeId) {
            var url = dataConstants.EMPLOYEE_URL + 'get-employee?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }


        function saveEmployee(data) {
            var url = dataConstants.EMPLOYEE_URL + 'save-employee';
            return apiHttpService.POST(url, data);
        }

        function updateEmployee(id, data) {
            var url = dataConstants.EMPLOYEE_URL + 'update-employee/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployee(id) {
            var url = dataConstants.EMPLOYEE_URL + 'delete-employee/' + id;
            return apiHttpService.DELETE(url);
        }
        function getEmployeeByPno() {
            var url = dataConstants.EMPLOYEE_URL + 'get-employee-by-pno';
            return apiHttpService.GET(url);
        }

        function getEmployeeByPno1(pno) {
            var url = dataConstants.EMPLOYEE_URL + 'get-employee-by-pno1?pno=' + pno;
            return apiHttpService.GET(url);
        }


        function GetLoginEmployeePno() {
            var url = dataConstants.EMPLOYEE_URL + 'get-employeePno';
            return apiHttpService.GET(url);
        }
        //----------------------------------------------------------------------------------------------------------

        function getEmployeesByDollarSign(pageSize, pageNumber, searchText) {
            var url = dataConstants.EMPLOYEE_URL + 'get-employees-by-dollar-sign?ps=' + pageSize + '&pn=' + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }
        function getEmployeeByDollarSign(employeeId) {
            var url = dataConstants.EMPLOYEE_URL + 'get-employee-by-dollar-sign?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function updateEmployeeDollarSign(data) {
            var url = dataConstants.EMPLOYEE_URL + 'update-employee-dollar-sign' ;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployeeDollarSign(employeeId) {
            var url = dataConstants.EMPLOYEE_URL + 'delete-employee-dollar-sign?employeeId=' + employeeId;
            return apiHttpService.DELETE(url);
        }
        
    }
})();