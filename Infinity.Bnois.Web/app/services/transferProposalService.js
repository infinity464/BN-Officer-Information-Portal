(function () {
    'use strict';
    angular.module('app').service('transferProposalService', ['dataConstants', 'apiHttpService', transferProposalService]);

    function transferProposalService(dataConstants, apiHttpService) {
        var service = {
            getTransferProposals: getTransferProposals,
            getTransferProposal: getTransferProposal,
            saveTransferProposal: saveTransferProposal,
            updateTransferProposal: updateTransferProposal,
            deleteTransferProposal: deleteTransferProposal
        };

        return service;
        function getTransferProposals(pageSize, pageNumber, searchText) {
            var url = dataConstants.TRANSFER_PROPOSAL_URL + 'get-transfer-proposals?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getTransferProposal(id) {
            var url = dataConstants.TRANSFER_PROPOSAL_URL + 'get-transfer-proposal?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveTransferProposal(data) {
            var url = dataConstants.TRANSFER_PROPOSAL_URL + 'save-transfer-proposal';
            return apiHttpService.POST(url, data);
        }

        function updateTransferProposal(id, data) {
            var url = dataConstants.TRANSFER_PROPOSAL_URL + 'update-transfer-proposal/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteTransferProposal(id) {
            var url = dataConstants.TRANSFER_PROPOSAL_URL + 'delete-transfer-proposal/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();