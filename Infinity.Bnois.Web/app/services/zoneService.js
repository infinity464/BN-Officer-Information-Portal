(function () {
    'use strict';
    angular.module('app').service('zoneService', ['dataConstants', 'apiHttpService', zoneService]);

    function zoneService(dataConstants, apiHttpService) {
        var service = {
            getZones: getZones,
            getZone: getZone,
            saveZone: saveZone,
            updateZone: updateZone,
            deleteZone: deleteZone
        };

        return service;
        function getZones(pageSize, pageNumber, searchText) {
            var url = dataConstants.ZONE_URL + 'get-zones?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getZone(zoneId) {
            var url = dataConstants.ZONE_URL + 'get-zone?id=' + zoneId;
            return apiHttpService.GET(url);
        }

        function saveZone(data) {
            var url = dataConstants.ZONE_URL + 'save-zone';
            return apiHttpService.POST(url, data);
        }

        function updateZone(zoneId, data) {
            var url = dataConstants.ZONE_URL + 'update-zone/' + zoneId;
            return apiHttpService.PUT(url, data);
        }

        function deleteZone(zoneId) {
            var url = dataConstants.ZONE_URL + 'delete-zone/' + zoneId;
            return apiHttpService.DELETE(url);
        }


    }
})();