(function () {
    'use strict';
    angular.module('app').service('siblingService', ['dataConstants', 'apiHttpService', siblingService]);

    function siblingService(dataConstants, apiHttpService) {
        var service = {
            getSiblings: getSiblings,
            getSibling: getSibling,
            saveSibling: saveSibling,
            updateSibling: updateSibling,
            deleteSibling: deleteSibling,
            imageUploadUrl: imageUploadUrl
        };

        return service;

        

        function imageUploadUrl(employeeId,siblingId) {
            var url = dataConstants.SIBLING_URL + 'upload-sibling-image?employeeId=' + employeeId + '&siblingId=' + siblingId;
            return url;
        }
        function getSiblings(employeeId) {
            var url = dataConstants.SIBLING_URL + 'get-siblings?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function getSibling(employeeId, siblingId) {
            var url = dataConstants.SIBLING_URL + 'get-sibling?employeeId=' + employeeId + '&siblingId=' + siblingId;
            return apiHttpService.GET(url);
        }

        function saveSibling(employeeId,data) {
            var url = dataConstants.SIBLING_URL + 'save-sibling/' + employeeId;
            return apiHttpService.POST(url, data);
        }

        function updateSibling(siblingId, data) {
            var url = dataConstants.SIBLING_URL + 'update-sibling/' + siblingId;
            return apiHttpService.PUT(url, data);
        }

        function deleteSibling(id) {
            var url = dataConstants.SIBLING_URL + 'delete-sibling/' + id;
            return apiHttpService.DELETE(url);
        }


    }
})();