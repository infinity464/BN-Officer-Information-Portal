(function () {
    'use strict';
    angular.module('app').service('resultService', ['dataConstants', 'apiHttpService', resultService]);
    function resultService(dataConstants, apiHttpService) {
        var service = {
            getResults: getResults,
            getResult:getResult,
            saveResult: saveResult,
            updateResult:updateResult,
            deleteResult: deleteResult
        };

        return service;
        function getResults(pageSize, pageNumber, searchString) {
            var url = dataConstants.RESULT_URL + 'get-results?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchString;
            return apiHttpService.GET(url);
        }

        function getResult(resultId) {
            var url = dataConstants.RESULT_URL + 'get-result?resultId=' + resultId;
            return apiHttpService.GET(url);
        }

        function saveResult(data) {
            var url = dataConstants.RESULT_URL + 'save-result';
            return apiHttpService.POST(url, data);
        }

        function updateResult(resultId, data) {
            var url = dataConstants.RESULT_URL + 'update-result/' + resultId;
            return apiHttpService.PUT(url, data);
        }

        function deleteResult(resultId) {
            var url = dataConstants.RESULT_URL + 'delete-result/' + resultId;
            return apiHttpService.DELETE(url);
        }

    }
})();