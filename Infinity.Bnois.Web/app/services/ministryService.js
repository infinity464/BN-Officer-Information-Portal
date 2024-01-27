(function () {
    'use strict';
    angular.module('app').service('ministryService', ['dataConstants', 'apiHttpService', ministryService]);

    function ministryService(dataConstants, apiHttpService) {
        var service = {
            getMinistries: getMinistries,
            getMinistry: getMinistry,
            saveMinistry: saveMinistry,
            updateMinistry: updateMinistry,
            deleteMinistry: deleteMinistry
        };

        return service;
        function getMinistries(pageSize, pageNumber, searchText) {
            var url = dataConstants.MINISTRY_URL + 'get-ministries?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getMinistry(ministryId) {
            var url = dataConstants.MINISTRY_URL + 'get-ministry?ministryId=' + ministryId;
     
            return apiHttpService.GET(url);
        }

        function saveMinistry(data) {
            var url = dataConstants.MINISTRY_URL + 'save-ministry';
            return apiHttpService.POST(url, data);
        }

        function updateMinistry(ministryId, data) {
            var url = dataConstants.MINISTRY_URL + 'update-ministry/' + ministryId;
            return apiHttpService.PUT(url, data);
        }

        function deleteMinistry(ministryId) {
            var url = dataConstants.MINISTRY_URL + 'delete-ministry/' + ministryId;
            return apiHttpService.DELETE(url);
        }


    }
})();