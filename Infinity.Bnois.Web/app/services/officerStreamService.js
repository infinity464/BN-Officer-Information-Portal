(function () {
    'use strict';
    angular.module('app').service('officerStreamService', ['dataConstants', 'apiHttpService', officerStreamService]);

    function officerStreamService(dataConstants, apiHttpService) {
        var service = {
            getOfficerStreams: getOfficerStreams,
            getOfficerStream: getOfficerStream,
            getOfficerStreamSelectModels: getOfficerStreamSelectModels,
            saveOfficerStream: saveOfficerStream,
            updateOfficerStream: updateOfficerStream,
            deleteOfficerStream: deleteOfficerStream
        };

        return service;
        function getOfficerStreams(pageSize, pageNumber, searchText) {
            var url = dataConstants.OFFICER_STREAM_URL + 'get-officer-streams?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getOfficerStream(id) {
            var url = dataConstants.OFFICER_STREAM_URL + 'get-officer-stream?id=' + id;
            return apiHttpService.GET(url);
        }

        function getOfficerStreamSelectModels() {
            var url = dataConstants.OFFICER_STREAM_URL + 'get-officer-stream-select-models';
            return apiHttpService.GET(url);
        }

        function saveOfficerStream(data) {
            var url = dataConstants.OFFICER_STREAM_URL + 'save-officer-stream';
            return apiHttpService.POST(url, data);
        }

        function updateOfficerStream(id, data) {
            var url = dataConstants.OFFICER_STREAM_URL + 'update-officer-stream/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteOfficerStream(id) {
            var url = dataConstants.OFFICER_STREAM_URL + 'delete-officer-stream/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();