(function () {
    'use strict';
    angular.module('app').service('sportService', ['dataConstants', 'apiHttpService', sportService]);

    function sportService(dataConstants, apiHttpService) {
        var service = {
            getSports: getSports,
            getSport: getSport,
            saveSport: saveSport,
            updateSport: updateSport,
            deleteSport: deleteSport
        };

        return service;
        function getSports(pageSize, pageNumber, searchText) {
            var url = dataConstants.SPORT_URL + 'get-sports?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getSport(id) {
            var url = dataConstants.SPORT_URL + 'get-sport?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveSport(data) {
            var url = dataConstants.SPORT_URL + 'save-sport';
            return apiHttpService.POST(url, data);
        }

        function updateSport(id, data) {
            var url = dataConstants.SPORT_URL + 'update-sport/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteSport(id) {
            var url = dataConstants.SPORT_URL + 'delete-sport/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();