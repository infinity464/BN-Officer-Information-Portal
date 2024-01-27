(function () {
    'use strict';
    angular.module('app').service('terminationTypeService', ['dataConstants', 'apiHttpService', terminationTypeService]);

    function terminationTypeService(dataConstants, apiHttpService) {
        var service = {
            getTerminationTypes: getTerminationTypes,
            getTerminationType: getTerminationType,
            saveTerminationType: saveTerminationType,
            updateTerminationType: updateTerminationType,
            deleteTerminationType: deleteTerminationType
        };

        return service;
        function getTerminationTypes(pageSize, pageNumber, searchText) {
            var url = dataConstants.TERMINATION_TYPE_URL + 'get-termination-types?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getTerminationType(terminationTypeId) {
            var url = dataConstants.TERMINATION_TYPE_URL + 'get-termination-type?id=' + terminationTypeId;
            return apiHttpService.GET(url);
        }

        function saveTerminationType(data) {
            var url = dataConstants.TERMINATION_TYPE_URL + 'save-termination-type';
            return apiHttpService.POST(url, data);
        }

        function updateTerminationType(terminationTypeId, data) {
            var url = dataConstants.TERMINATION_TYPE_URL + 'update-termination-type/' + terminationTypeId;
            return apiHttpService.PUT(url, data);
        }

        function deleteTerminationType(terminationTypeId) {
            var url = dataConstants.TERMINATION_TYPE_URL + 'delete-termination-type/' + terminationTypeId;
            return apiHttpService.DELETE(url);
        }


    }
})();