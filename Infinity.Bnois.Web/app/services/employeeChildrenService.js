(function () {
    'use strict';
    angular.module('app').service('employeeChildrenService', ['dataConstants', 'apiHttpService', employeeChildrenService]);

    function employeeChildrenService(dataConstants, apiHttpService) {
        var service = {
            getEmployeeChildrens: getEmployeeChildrens,
            getEmployeeChildren: getEmployeeChildren,
            saveEmployeeChildren: saveEmployeeChildren,
            updateEmployeeChildren: updateEmployeeChildren,
            deleteEmployeeChildren: deleteEmployeeChildren,
            imageUploadUrl: imageUploadUrl,
            childrenGenFormUploadUrl: childrenGenFormUploadUrl
        };

        return service;

        function imageUploadUrl(employeeId, employeeChildrenId) {
            var url = dataConstants.EMPLOYEE_CHILDREN_URL + 'upload-children-image?employeeId=' + employeeId + '&employeeChildrenId=' + employeeChildrenId;
            return url;
        }
        function childrenGenFormUploadUrl(employeeId, employeeChildrenId) {
            var url = dataConstants.EMPLOYEE_CHILDREN_URL + 'upload-children-gen-form?employeeId=' + employeeId + '&employeeChildrenId=' + employeeChildrenId;
            return url;
        }


        function getEmployeeChildrens(employeeId) {
            var url = dataConstants.EMPLOYEE_CHILDREN_URL + 'get-employee-childrens?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function getEmployeeChildren(employeeId, employeeChildrenId) {
            var url = dataConstants.EMPLOYEE_CHILDREN_URL + 'get-employee-children?employeeId=' + employeeId + '&employeeChildrenId=' + employeeChildrenId;
            return apiHttpService.GET(url);
        }

        function saveEmployeeChildren(employeeId,data) {
            var url = dataConstants.EMPLOYEE_CHILDREN_URL + 'save-employee-children/' + employeeId;
            return apiHttpService.POST(url, data);
        }

        function updateEmployeeChildren(employeeChildrenId, data) {
            var url = dataConstants.EMPLOYEE_CHILDREN_URL + 'update-employee-children/' + employeeChildrenId;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployeeChildren(id) {
            var url = dataConstants.EMPLOYEE_CHILDREN_URL + 'delete-employee-children/' + id;
            return apiHttpService.DELETE(url);
        }

    }
})();