(function () {
    'use strict';
    angular.module('app').service('employeeGeneralService', ['dataConstants', 'apiHttpService', employeeGeneralService]);

    function employeeGeneralService(dataConstants, apiHttpService) {
        var service = {
            getEmployeeGenerals: getEmployeeGenerals,
            getEmployeeGeneral: getEmployeeGeneral,
            updateEmployeeGeneral: updateEmployeeGeneral,
            getSubCategoryByCategoryId: getSubCategoryByCategoryId,
            getReligionCastByReligionId: getReligionCastByReligionId
        };

        return service;

        function getEmployeeGenerals(employeeId) {
            var url = dataConstants.EMPLOYEEG_GENERAL_URL + 'get-employee-generals?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }
        function getEmployeeGeneral(employeeId) {
            var url = dataConstants.EMPLOYEEG_GENERAL_URL + 'get-employee-general?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function updateEmployeeGeneral(employeeId, data) {
            var url = dataConstants.EMPLOYEEG_GENERAL_URL + 'update-employee-general/' + employeeId;
            return apiHttpService.PUT(url, data);
        }
        function getSubCategoryByCategoryId(categoryId) {
            var url = dataConstants.EMPLOYEEG_GENERAL_URL + 'get-sub-category?categoryId=' + categoryId;
            return apiHttpService.GET(url);
        }
        function getReligionCastByReligionId(religionId) {
            var url = dataConstants.EMPLOYEEG_GENERAL_URL + 'get-religion-casts?religionId=' + religionId;
            return apiHttpService.GET(url);
        }
        
    }
})();