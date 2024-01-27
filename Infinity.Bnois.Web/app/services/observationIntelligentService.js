(function () {
    'use strict';
    angular.module('app').service('observationIntelligentService', ['dataConstants', 'apiHttpService', observationIntelligentService]);

    function observationIntelligentService(dataConstants, apiHttpService) {
        var service = {
            getObservationIntelligents: getObservationIntelligents,
            getObservationIntelligent: getObservationIntelligent,
            saveObservationIntelligent: saveObservationIntelligent,
            updateObservationIntelligent: updateObservationIntelligent,
            deleteObservationIntelligent: deleteObservationIntelligent
        };

        return service;
        function getObservationIntelligents(pageSize, pageNumber, searchText) {
            var url = dataConstants.OBSERVATION_INTELLIGENT_URL + 'get-observation-intelligent-reports?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getObservationIntelligent(observationIntelligentId) {
            var url = dataConstants.OBSERVATION_INTELLIGENT_URL + 'get-observation-intelligent-report?id=' + observationIntelligentId;
            return apiHttpService.GET(url);
        }

        function saveObservationIntelligent(data) {
            var url = dataConstants.OBSERVATION_INTELLIGENT_URL + 'save-observation-intelligent-report';
            return apiHttpService.POST(url, data);
        }

        function updateObservationIntelligent(observationIntelligentId, data) {
            var url = dataConstants.OBSERVATION_INTELLIGENT_URL + 'update-observation-intelligent-report/' + observationIntelligentId;
            return apiHttpService.PUT(url, data);
        }

        function deleteObservationIntelligent(observationIntelligentId) {
            var url = dataConstants.OBSERVATION_INTELLIGENT_URL + 'delete-observation-intelligent-report/' + observationIntelligentId;
            return apiHttpService.DELETE(url);
        }


    }
})();