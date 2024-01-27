(function () {
    'use strict';
    angular.module('app').service('batchService', ['dataConstants', 'apiHttpService', batchService]);

    function batchService(dataConstants, apiHttpService) {
        var service = {
            getBatches: getBatches,
            getBatch: getBatch,
            saveBatch: saveBatch,
            updateBatch: updateBatch,
            deleteBatch: deleteBatch
        };

        return service;
        function getBatches(pageSize, pageNumber, searchText) {
            var url = dataConstants.BATCH_URL + 'get-batches?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getBatch(id) {
            var url = dataConstants.BATCH_URL + 'get-batch?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveBatch(data) {
            var url = dataConstants.BATCH_URL + 'save-batch';
            return apiHttpService.POST(url, data);
        }

        function updateBatch(id, data) {
            var url = dataConstants.BATCH_URL + 'update-batch/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteBatch(id) {
            var url = dataConstants.BATCH_URL + 'delete-batch/' + id;
            return apiHttpService.DELETE(url);
        }
        
    }
})();