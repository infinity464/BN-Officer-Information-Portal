(function () {
    'use strict';
    angular.module('app').service('previousTransferService', ['dataConstants', 'apiHttpService', previousTransferService]);

    function previousTransferService(dataConstants, apiHttpService) {
        var service = {
            getPreviousTransfers: getPreviousTransfers,
            getPreviousTransfer: getPreviousTransfer,
            savePreviousTransfer: savePreviousTransfer,
            updatePreviousTransfer: updatePreviousTransfer,
            deletePreviousTransfer: deletePreviousTransfer
        };

        return service;


        function getPreviousTransfers(employeeId) {
            var url = dataConstants.PREVIOUS_TRANSFER_URL + 'get-previous-transfers?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function getPreviousTransfer(previousTransferId) {
            var url = dataConstants.PREVIOUS_TRANSFER_URL + 'get-previous-transfer?&previousTransferId=' + previousTransferId;
            return apiHttpService.GET(url);
        }

        function savePreviousTransfer(employeeId, data) {
            var url = dataConstants.PREVIOUS_TRANSFER_URL + 'save-previous-transfer/' + employeeId;
            return apiHttpService.POST(url, data);
        }
        function updatePreviousTransfer(previousTransferId, data) {
            var url = dataConstants.PREVIOUS_TRANSFER_URL + 'update-previous-transfer/' + previousTransferId;
            return apiHttpService.PUT(url, data);
        }

        function deletePreviousTransfer(id) {
            var url = dataConstants.PREVIOUS_TRANSFER_URL + 'delete-previous-transfer/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();