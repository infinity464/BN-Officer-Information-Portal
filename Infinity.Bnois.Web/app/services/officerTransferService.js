(function () {
    'use strict';
    angular.module('app').service('officerTransferService', ['dataConstants', 'apiHttpService', officerTransferService]);

    function officerTransferService(dataConstants, apiHttpService) {
        var service = {
            getOfficerTransfers: getOfficerTransfers,
            getOfficerTransfer: getOfficerTransfer,
            getOfficerFromSelectModel: getOfficerFromSelectModel,
            getLastOfficerTransfer: getLastOfficerTransfer,
            saveOfficerTransfer: saveOfficerTransfer,
            updateOfficerTransfer: updateOfficerTransfer,
            deleteOfficerTransfer: deleteOfficerTransfer,
            fileUploadUrl: fileUploadUrl,
            downloadTransferFile: downloadTransferFile
        };

        return service;

        

        function downloadTransferFile(transferId) {
            var url = dataConstants.OFFICER_TRANSFERS + 'downlaod-transfer-file?id=' + transferId;
            return url;
        }

        function fileUploadUrl(employeeId, type) {
            var url = dataConstants.OFFICER_TRANSFERS + 'upload-transfer-file';
            return url;
        }

        function getOfficerTransfers(employeeId,type) {
            var url = dataConstants.OFFICER_TRANSFERS + 'get-officer-transfers?employeeId=' + employeeId+'&type='+type;
            return apiHttpService.GET(url);
        }
        function getOfficerFromSelectModel() {
            var url = dataConstants.OFFICER_TRANSFERS + 'get-officer-from-select-models';
            return apiHttpService.GET(url);
        }

        function getOfficerTransfer(officerTransferId) {
            var url = dataConstants.OFFICER_TRANSFERS + 'get-officer-transfer?id=' + officerTransferId;
            return apiHttpService.GET(url);
        }

        function getLastOfficerTransfer(employeeId) {
            var url = dataConstants.EMPLOYEELEAVE_URL + 'get-last-officer-transfer?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function saveOfficerTransfer(data) {
            var url = dataConstants.OFFICER_TRANSFERS + 'save-officer-transfer';
            return apiHttpService.POST(url, data);
        }

        function updateOfficerTransfer(officerTransferId, data) {
            var url = dataConstants.OFFICER_TRANSFERS + 'update-officer-transfer/' + officerTransferId;
            return apiHttpService.PUT(url, data);
        }

        function deleteOfficerTransfer(officerTransferId) {
            var url = dataConstants.OFFICER_TRANSFERS + 'delete-officer-transfer/' + officerTransferId;
            return apiHttpService.DELETE(url);
        }
        
    }
})();