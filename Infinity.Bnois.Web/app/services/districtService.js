(function () {
    'use strict';
    angular.module('app').service('districtService', ['dataConstants', 'apiHttpService', districtService]);

    function districtService(dataConstants, apiHttpService) {
        var service = {
            getDistricts: getDistricts,
            getDistrict: getDistrict,
            saveDistrict: saveDistrict,
            updateDistrict: updateDistrict,
            deleteDistrict: deleteDistrict
        };

        return service;
        function getDistricts(pageSize, pageNumber, searchText) {
            var url = dataConstants.DISTRICT_URL + 'get-districts?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getDistrict(id) {
            var url = dataConstants.DISTRICT_URL + 'get-district?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveDistrict(data) {
            var url = dataConstants.DISTRICT_URL + 'save-district';
            return apiHttpService.POST(url, data);
        }

        function updateDistrict(id, data) {
            var url = dataConstants.DISTRICT_URL + 'update-district/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteDistrict(id) {
            var url = dataConstants.DISTRICT_URL + 'delete-district/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();