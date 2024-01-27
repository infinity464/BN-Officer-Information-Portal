(function () {
    'use strict';
    angular.module('app').service('employeeHajjDetailService', ['dataConstants', 'apiHttpService', employeeHajjDetailService]);

    function employeeHajjDetailService(dataConstants, apiHttpService) {
        var service = {
            getEmployeeHajjDetails: getEmployeeHajjDetails,
            getEmployeeHajjDetail: getEmployeeHajjDetail,
            saveEmployeeHajjDetail: saveEmployeeHajjDetail,
            updateEmployeeHajjDetail: updateEmployeeHajjDetail,
            deleteEmployeeHajjDetail: deleteEmployeeHajjDetail,
            GetEmployeeHajjDetailsByPno: GetEmployeeHajjDetailsByPno
        };

        return service;
        function getEmployeeHajjDetails(pageSize, pageNumber, searchText) {
            var url = dataConstants.EMPLOYEE_HAJJ_DETAILS_URL + 'get-employee-hajj-details?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }
        function GetEmployeeHajjDetailsByPno(PNo) {
            var url = dataConstants.EMPLOYEE_HAJJ_DETAILS_URL + 'get-employee-hajj-details-by-pno?PNo=' + PNo
            return apiHttpService.GET(url);
        }
        function getEmployeeHajjDetail(employeeHajjDetailId) {
            var url = dataConstants.EMPLOYEE_HAJJ_DETAILS_URL + 'get-employeeHajj-detail?id=' + employeeHajjDetailId;
            return apiHttpService.GET(url);
        }

        function saveEmployeeHajjDetail(data) {
            var url = dataConstants.EMPLOYEE_HAJJ_DETAILS_URL + 'save-employee-hajj-detail';
            return apiHttpService.POST(url, data);
        }

        function updateEmployeeHajjDetail(employeeHajjDetailId, data) {
            var url = dataConstants.EMPLOYEE_HAJJ_DETAILS_URL + 'update-employee-hajj-detail/' + employeeHajjDetailId;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployeeHajjDetail(employeeHajjDetailId) {
            var url = dataConstants.EMPLOYEE_HAJJ_DETAILS_URL + 'delete-employee-hajj-detail/' + employeeHajjDetailId;
            return apiHttpService.DELETE(url);
        }


    }
})();