(function () {
    'use strict';
    angular.module('app').service('retiredAgeService', ['dataConstants', 'apiHttpService', retiredAgeService]);

    function retiredAgeService(dataConstants, apiHttpService) {
        var service = {
            getRetiredAges: getRetiredAges,
            getRetiredAge: getRetiredAge,
            saveRetiredAge: saveRetiredAge,
            updateRetiredAge: updateRetiredAge,
            deleteRetiredAge: deleteRetiredAge
        };

        return service;
        function getRetiredAges(pageSize, pageNumber, searchText) {
            var url = dataConstants.RETIRED_AGE_URL + 'get-retired-ages?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getRetiredAge(id) {
            var url = dataConstants.RETIRED_AGE_URL + 'get-retired-age?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveRetiredAge(data) {
            var url = dataConstants.RETIRED_AGE_URL + 'save-retired-age';
            return apiHttpService.POST(url, data);
        }

        function updateRetiredAge(id, data) {
            var url = dataConstants.RETIRED_AGE_URL + 'update-retired-age/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteRetiredAge(id) {
            var url = dataConstants.RETIRED_AGE_URL_URL + 'delete-retired-age/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();