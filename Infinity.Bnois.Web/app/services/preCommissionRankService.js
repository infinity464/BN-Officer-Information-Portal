(function () {
    'use strict';
    angular.module('app').service('preCommissionRankService', ['dataConstants', 'apiHttpService', preCommissionRankService]);

    function preCommissionRankService(dataConstants, apiHttpService) {
        var service = {
            getPreCommissionRanks: getPreCommissionRanks,
            getPreCommissionRank: getPreCommissionRank,
            savePreCommissionRank: savePreCommissionRank,
            updatePreCommissionRank: updatePreCommissionRank,
            deletePreCommissionRank: deletePreCommissionRank
        };

        return service;
        function getPreCommissionRanks(pageSize, pageNumber, searchText) {
            var url = dataConstants.PRE_COMMISSION_RANK_URL + 'get-pre-commission-ranks?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getPreCommissionRank(preCommissionRankId) {
            var url = dataConstants.PRE_COMMISSION_RANK_URL + 'get-pre-commission-rank?id=' + preCommissionRankId;
            return apiHttpService.GET(url);
        }

        function savePreCommissionRank(data) {
            var url = dataConstants.PRE_COMMISSION_RANK_URL + 'save-pre-commission-rank';
            return apiHttpService.POST(url, data);
        }

        function updatePreCommissionRank(preCommissionRankId, data) {
            var url = dataConstants.PRE_COMMISSION_RANK_URL + 'update-pre-commission-rank/' + preCommissionRankId;
            return apiHttpService.PUT(url, data);
        }

        function deletePreCommissionRank(preCommissionRankId) {
            var url = dataConstants.PRE_COMMISSION_RANK_URL + 'delete-pre-commission-rank/' + preCommissionRankId;
            return apiHttpService.DELETE(url);
        }


    }
})();