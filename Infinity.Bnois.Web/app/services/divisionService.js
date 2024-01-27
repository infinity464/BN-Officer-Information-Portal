(function () {
    'use strict';
    angular.module('app').service('divisionService', ['dataConstants', 'apiHttpService', divisionService]);

    function divisionService(dataConstants, apiHttpService) {
        var service = {
            getDivisions: getDivisions,
            getDivision: getDivision,
            saveDivision: saveDivision,
            updateDivision: updateDivision,
            deleteDivision: deleteDivision
        };

        return service;
        function getDivisions(pageSize, pageNumber, searchText) {
            var url = dataConstants.DIVISION_URL + 'get-divisions?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getDivision(id) {
            var url = dataConstants.DIVISION_URL + 'get-division?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveDivision(data) {
            var url = dataConstants.DIVISION_URL + 'save-division';
            return apiHttpService.POST(url, data);
        }

        function updateDivision(id, data) {
            var url = dataConstants.DIVISION_URL + 'update-division/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteDivision(id) {
            var url = dataConstants.DIVISION_URL + 'delete-division/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();