(function () {
    'use strict';
    angular.module('app').service('resultTypeService', ['dataConstants', 'apiHttpService', resultTypeService]);

    function resultTypeService(dataConstants, apiHttpService) {
        var service = {
            getResultTypes: getResultTypes,
            getResultType: getResultType,
            saveResultType: saveResultType,
            updateResultType: updateResultType,
            deleteResultType: deleteResultType
        };

        return service;
        function getResultTypes(pageSize, pageNumber, searchText) {
            var url = dataConstants.RESULT_TYPE_URL + 'get-result-types?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getResultType(id) {
            var url = dataConstants.RESULT_TYPE_URL + 'get-result-type?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveResultType(data) {
            var url = dataConstants.RESULT_TYPE_URL + 'save-result-type';
            return apiHttpService.POST(url, data);
        }

        function updateResultType(id, data) {
            var url = dataConstants.RESULT_TYPE_URL + 'update-result-type/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteResultType(id) {
            var url = dataConstants.RESULT_TYPE_URL + 'delete-result-type/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();