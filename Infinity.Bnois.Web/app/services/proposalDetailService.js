(function () {
    'use strict';
    angular.module('app').service('proposalDetailService', ['dataConstants', 'apiHttpService', proposalDetailService]);

    function proposalDetailService(dataConstants, apiHttpService) {
        var service = {
            getProposalDetails: getProposalDetails,
            getProposalDetail: getProposalDetail,
            saveProposalDetail: saveProposalDetail,
            updateProposalDetail: updateProposalDetail,
            deleteProposalDetail: deleteProposalDetail
        };

        return service;
        function getProposalDetails(transferProposalId, pageSize, pageNumber, searchText) {
            var url = dataConstants.PROPOSAL_DETAIL_URL + 'get-proposal-details?transferProposalId=' + transferProposalId + '&ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getProposalDetail(id) {
            var url = dataConstants.PROPOSAL_DETAIL_URL + 'get-proposal-detail?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveProposalDetail(transferProposalId,data) {
            var url = dataConstants.PROPOSAL_DETAIL_URL + 'save-proposal-detail/' + transferProposalId;
            return apiHttpService.POST(url, data);
        }

        function updateProposalDetail(id, data) {
            var url = dataConstants.PROPOSAL_DETAIL_URL + 'update-proposal-detail/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteProposalDetail(id) {
            var url = dataConstants.PROPOSAL_DETAIL_URL + 'delete-proposal-detail/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();