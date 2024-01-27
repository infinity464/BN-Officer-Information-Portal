(function () {
    'use strict';
    angular.module('app').service('proposalCandidateService', ['dataConstants', 'apiHttpService', proposalCandidateService]);

    function proposalCandidateService(dataConstants, apiHttpService) {
        var service = {
            getProposalCandidates: getProposalCandidates,
            saveProposalCandidate: saveProposalCandidate,
            deleteProposalCandidate: deleteProposalCandidate
        };

        return service;
        function getProposalCandidates(proposalDetailId) {
            var url = dataConstants.PROPOSAL_CANDIDATE_URL + 'get-proposal-candidates/'+proposalDetailId;
            return apiHttpService.GET(url);
        }
        

        function saveProposalCandidate(proposalDetailId,data) {
            var url = dataConstants.PROPOSAL_CANDIDATE_URL + 'save-proposal-candidate/' + proposalDetailId;
            return apiHttpService.POST(url, data);
        }

        function deleteProposalCandidate(proposalCandidateId) {
            var url = dataConstants.PROPOSAL_CANDIDATE_URL + 'delete-proposal-candidate/' + proposalCandidateId;
            return apiHttpService.DELETE(url);
        }
    }
})();