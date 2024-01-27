(function () {
    'use strict';
    angular.module('app').service('employeeRelationService', ['dataConstants', 'apiHttpService', employeeRelationService]);

    function employeeRelationService(dataConstants, apiHttpService) {
        var service = {
            getEmployeeRelations: getEmployeeRelations,
            getEmployeeRelation: getEmployeeRelation,
            saveEmployeeRelation: saveEmployeeRelation,
            updateEmployeeRelation: updateEmployeeRelation,
            deleteEmployeeRelation: deleteEmployeeRelation
        };

        return service;
        function getEmployeeRelations(pageSize, pageNumber, searchText) {
            var url = dataConstants.EMPLOYEE_RELATION_URL + 'get-employee-relations?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getEmployeeRelation(relationId) {
            var url = dataConstants.EMPLOYEE_RELATION_URL + 'get-employee-relation?relationId=' + relationId;
            return apiHttpService.GET(url);
        }

        function saveEmployeeRelation(data) {
            var url = dataConstants.EMPLOYEE_RELATION_URL + 'save-employee-relation';
            return apiHttpService.POST(url, data);
        }

        function updateEmployeeRelation(relationId, data) {
            var url = dataConstants.EMPLOYEE_RELATION_URL + 'update-employee-relation/' + relationId;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmployeeRelation(relationId) {
            var url = dataConstants.EMPLOYEE_RELATION_URL + 'delete-employee-relation/' + relationId;
            return apiHttpService.DELETE(url);
        }

    }
})();