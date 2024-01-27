(function () {
    'use strict';
    angular.module('app').service('upazilaService', ['dataConstants', 'apiHttpService', upazilaService]);

    function upazilaService(dataConstants, apiHttpService) {
        var service = {
            getUpazilas: getUpazilas,
            getUpazila: getUpazila,
            getDistrictByDivision: getDistrictByDivision,
            saveUpazila: saveUpazila,
            updateUpazila: updateUpazila,
            deleteUpazila: deleteUpazila
        };

        return service;
        function getUpazilas(pageSize, pageNumber, searchText) {
            var url = dataConstants.UPAZILA_URL + 'get-upazilas?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getUpazila(id) {
            var url = dataConstants.UPAZILA_URL + 'get-upazila?id=' + id;
            return apiHttpService.GET(url);
        }
        function getDistrictByDivision(id) {
            var url = dataConstants.UPAZILA_URL + 'get-district-by-division?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveUpazila(data) {
            var url = dataConstants.UPAZILA_URL + 'save-upazila';
            return apiHttpService.POST(url, data);
        }

        function updateUpazila(id, data) {
            var url = dataConstants.UPAZILA_URL + 'update-upazila/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteUpazila(id) {
            var url = dataConstants.UPAZILA_URL + 'delete-upazila/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();