(function () {
    'use strict';
    angular.module('app').service('seaServiceService', ['dataConstants', 'apiHttpService', seaServiceService]);

    function seaServiceService(dataConstants, apiHttpService) {
        var service = {
            getSeaServices: getSeaServices,
            getSeaService: getSeaService,  
            saveSeaService: saveSeaService,
            updateSeaService: updateSeaService,
            deleteSeaService: deleteSeaService
        };

        return service;
        function getSeaServices(pageSize, pageNumber,searchText) {
            var url = dataConstants.SEA_SERVICE_URL + 'get-sea-services?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getSeaService(seaServiceId) {
            var url = dataConstants.SEA_SERVICE_URL + 'get-sea-service?id=' + seaServiceId;
            return apiHttpService.GET(url);
        }

        function saveSeaService(data) {
            var url = dataConstants.SEA_SERVICE_URL + 'save-sea-service';
            return apiHttpService.POST(url, data);
        }

        function updateSeaService(seaServiceId, data) {
            var url = dataConstants.SEA_SERVICE_URL + 'update-sea-service/' + seaServiceId;
            return apiHttpService.PUT(url, data);
        }

        function deleteSeaService(seaServiceId) {
            var url = dataConstants.SEA_SERVICE_URL + 'delete-sea-service/' + seaServiceId;
            return apiHttpService.DELETE(url);
        }
        
    }
})();