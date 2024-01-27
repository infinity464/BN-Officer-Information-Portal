(function () {
    'use strict';
    angular.module('app').service('medalService', ['dataConstants', 'apiHttpService', medalService]);

    function medalService(dataConstants, apiHttpService) {
        var service = {
            getMedals: getMedals,
            getMedal: getMedal,
            saveMedal: saveMedal,
            updateMedal: updateMedal,
            deleteMedal: deleteMedal
        };

        return service;
        function getMedals(pageSize, pageNumber, searchText) {
            var url = dataConstants.MEDAL_URL + 'get-medals?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getMedal(id) {
            var url = dataConstants.MEDAL_URL + 'get-medal?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveMedal(data) {
            var url = dataConstants.MEDAL_URL + 'save-medal';
            return apiHttpService.POST(url, data);
        }

        function updateMedal(id, data) {
            var url = dataConstants.MEDAL_URL + 'update-medal/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteMedal(id) {
            var url = dataConstants.MEDAL_URL + 'delete-medal/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();