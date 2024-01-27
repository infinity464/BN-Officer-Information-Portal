(function () {
    'use strict';
    angular.module('app').service('empRunMissingService', ['dataConstants', 'apiHttpService', empRunMissingService]);

    function empRunMissingService(dataConstants, apiHttpService) {
        var service = {
            getEmpRunMissings: getEmpRunMissings,
            getEmpRunMissing: getEmpRunMissing,
            saveEmpRunMissing: saveEmpRunMissing,
            updateEmpRunMissing: updateEmpRunMissing,
            deleteEmpRunMissing: deleteEmpRunMissing,

            //--Officer Back to Unit---------------------
            getEmpBackToUnits: getEmpBackToUnits,
            getEmpBackToUnit: getEmpBackToUnit,
            saveEmpBackToUnit: saveEmpBackToUnit,
            updateEmpBackToUnit: updateEmpBackToUnit,
        };

        return service;
        function getEmpRunMissings(pageSize, pageNumber, searchText) {
            var url = dataConstants.EMPLOYEE_RUN_MISSING_URL + 'get-employee-run-missings?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getEmpRunMissing(id) {
            var url = dataConstants.EMPLOYEE_RUN_MISSING_URL + 'get-employee-run-missing?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveEmpRunMissing(data) {
            var url = dataConstants.EMPLOYEE_RUN_MISSING_URL + 'save-employee-run-missing';
            return apiHttpService.POST(url, data);
        }

        function updateEmpRunMissing(id, data) {
            var url = dataConstants.EMPLOYEE_RUN_MISSING_URL + 'update-employee-run-missing/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmpRunMissing(id) {
            var url = dataConstants.EMPLOYEE_RUN_MISSING_URL + 'delete-employee-run-missing/' + id;
            return apiHttpService.DELETE(url);
        }
         //--Officer Back to Unit---------------------

        function getEmpBackToUnits(pageSize, pageNumber, searchText) {
            var url = dataConstants.EMPLOYEE_RUN_MISSING_URL + 'get-emp-back-to-units?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getEmpBackToUnit(id) {
            var url = dataConstants.EMPLOYEE_RUN_MISSING_URL + 'get-emp-back-to-unit?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveEmpBackToUnit(data) {
            var url = dataConstants.EMPLOYEE_RUN_MISSING_URL + 'save-emp-back-to-unit';
            return apiHttpService.POST(url, data);
        }

        function updateEmpBackToUnit(id, data) {
            var url = dataConstants.EMPLOYEE_RUN_MISSING_URL + 'update-emp-back-to-unit/' + id;
            return apiHttpService.PUT(url, data);
        }
    }
})();