(function () {
    'use strict';
    angular.module('app').service('departmentService', ['dataConstants', 'apiHttpService', departmentService]);

    function departmentService(dataConstants, apiHttpService) {
        var service = {
            getDepartments: getDepartments,
            getDepartment: getDepartment,
            saveDepartment: saveDepartment,
            updateDepartment: updateDepartment,
            deleteDepartment: deleteDepartment
        };

        return service;
		function getDepartments(pageSize, pageNumber, searchText) {
            var url = dataConstants.DEPARTMENT_URL + 'get-departments?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getDepartment(departmentId) {
            var url = dataConstants.DEPARTMENT_URL + 'get-department?departmentId=' + departmentId;
     
            return apiHttpService.GET(url);
        }

        function saveDepartment(data) {
            var url = dataConstants.DEPARTMENT_URL + 'save-department';
            return apiHttpService.POST(url, data);
        }

        function updateDepartment(departmentId, data) {
            var url = dataConstants.DEPARTMENT_URL + 'update-department/' + departmentId;
            return apiHttpService.PUT(url, data);
        }

        function deleteDepartment(departmentId) {
            var url = dataConstants.DEPARTMENT_URL + 'delete-department/' + departmentId;
            return apiHttpService.DELETE(url);
        }


    }
})();