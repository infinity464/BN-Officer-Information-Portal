(function () {
    'use strict';
    angular.module('app').service('employeeCategoryService', ['dataConstants', 'apiHttpService', employeeCategoryService]);

    function employeeCategoryService(dataConstants, apiHttpService) {
        var service = {
            geteEmployeeCategories: geteEmployeeCategories,
            getEmployeeCategory: getEmployeeCategory,
            saveEmployeeCategory: saveEmployeeCategory,
            updateEmployeeCategory: updateEmployeeCategory,
            deleteEmployeeCategory: deleteEmployeeCategory,
            getEmployeeCategorySelectModels: getEmployeeCategorySelectModels
        };

        return service;
        function geteEmployeeCategories(pageSize, pageNumber,searchText) {
            var url = dataConstants.EMPLOYEE_CATEGORY_URL + 'get-employee-categories?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getEmployeeCategory(employeeCategoryId) {
            var url = dataConstants.EMPLOYEE_CATEGORY_URL + 'get-employee-category?employeeCategoryId=' + employeeCategoryId;
            return apiHttpService.GET(url);
        }

        function saveEmployeeCategory(data) {
            var url = dataConstants.EMPLOYEE_CATEGORY_URL + 'save-employee-category';
            return apiHttpService.POST(url, data);
        }

        function updateEmployeeCategory(employeeCategoryId, data) {
            var url = dataConstants.EMPLOYEE_CATEGORY_URL + 'update-employee-category/' + employeeCategoryId;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployeeCategory(employeeCategoryId) {
            var url = dataConstants.EMPLOYEE_CATEGORY_URL + 'delete-employee-category/' + employeeCategoryId;
            return apiHttpService.DELETE(url);
        }
        function getEmployeeCategorySelectModels() {
            var url = dataConstants.EMPLOYEE_CATEGORY_URL + 'get-employee-category-select-models';
            return apiHttpService.GET(url);
        }
        
    }
})();