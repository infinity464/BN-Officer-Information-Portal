(function () {
    'use strict';
    angular.module('app').service('designationService', ['dataConstants', 'apiHttpService', designationService]);

    function designationService(dataConstants, apiHttpService) {
        var service = {
            getDesignations: getDesignations,
            getDesignation: getDesignation,
            saveDesignation: saveDesignation,
            updateDesignation: updateDesignation,
            deleteDesignation: deleteDesignation
        };

        return service;
        function getDesignations(pageSize, pageNumber, searchText) {
            var url = dataConstants.DESIGNATION_URL + 'get-designations?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getDesignation(designationId) {
            var url = dataConstants.DESIGNATION_URL + 'get-designation?designationId=' + designationId;
            return apiHttpService.GET(url);
        }

        function saveDesignation(data) {
            var url = dataConstants.DESIGNATION_URL + 'save-designation';
            return apiHttpService.POST(url, data);
        }

        function updateDesignation(designationId, data) {
            var url = dataConstants.DESIGNATION_URL + 'update-designation/' + designationId;
            return apiHttpService.PUT(url, data);
        }

        function deleteDesignation(designationId) {
            var url = dataConstants.DESIGNATION_URL + 'delete-designation/' + designationId;
            return apiHttpService.DELETE(url);
        }


    }
})();