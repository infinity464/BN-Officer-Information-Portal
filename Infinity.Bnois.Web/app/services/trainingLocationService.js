(function () {
    'use strict';
    angular.module('app').service('trainingLocationService', ['dataConstants', 'apiHttpService', trainingLocationService]);

    function trainingLocationService(dataConstants, apiHttpService) {
        var service = {
            getTrainingLocations: getTrainingLocations,
            getTrainingLocation: getTrainingLocation,
            saveTrainingLocation: saveTrainingLocation,
            updateTrainingLocation: updateTrainingLocation,
            deleteTrainingLocation: deleteTrainingLocation
          
        };

        return service;
        function getTrainingLocations(pageSize, pageNumber,searchText) {
            var url = dataConstants.LOCATION_URL + 'get-locations?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getTrainingLocation(locationId) {
            var url = dataConstants.LOCATION_URL + 'get-location?locationId=' + locationId;
            return apiHttpService.GET(url);
        }

        function saveTrainingLocation(data) {
            var url = dataConstants.LOCATION_URL + 'save-location';
            return apiHttpService.POST(url, data);
        }

        function updateTrainingLocation(locationId, data) {
            var url = dataConstants.LOCATION_URL + 'update-location/' + locationId;
            return apiHttpService.PUT(url, data);
        }

        function deleteTrainingLocation(locationId) {
            var url = dataConstants.LOCATION_URL + 'delete-location/' + locationId;
            return apiHttpService.DELETE(url);
        }
        
    }
})();