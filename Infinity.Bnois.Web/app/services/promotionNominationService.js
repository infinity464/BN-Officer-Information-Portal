(function () {
    'use strict';
    angular.module('app').service('promotionNominationService', ['dataConstants', 'apiHttpService', promotionNominationService]);

    function promotionNominationService(dataConstants, apiHttpService) {
        var service = {
            getPromotionNominations: getPromotionNominations,
            getPromotionNomination: getPromotionNomination,
            savePromotionNomination: savePromotionNomination,
            updatePromotionNomination: updatePromotionNomination,
            deletePromotionNomination: deletePromotionNomination
        };

        return service;
        function getPromotionNominations(promotionBoardId, pageSize, pageNumber, searchText,type) {
            var url = dataConstants.PROMOTION_NOMINATION_URL + 'get-promotion-nominations?promotionBoardId=' + promotionBoardId + '&ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText+"&type="+type;
            return apiHttpService.GET(url);
        }

        function getPromotionNomination(promotionBoardId, promotionNominationId) {
            var url = dataConstants.PROMOTION_NOMINATION_URL + 'get-promotion-nomination?promotionBoardId=' + promotionBoardId + '&promotionNominationId=' + promotionNominationId;
            return apiHttpService.GET(url);
        }

        function savePromotionNomination(data) {
            var url = dataConstants.PROMOTION_NOMINATION_URL + 'save-promotion-nomination';
            return apiHttpService.POST(url, data);
        }

        function updatePromotionNomination(promotionNominationId, data) {
            var url = dataConstants.PROMOTION_NOMINATION_URL + 'update-promotion-nomination/' + promotionNominationId;
            return apiHttpService.PUT(url, data);
        }

        function deletePromotionNomination(promotionNominationId) {
            var url = dataConstants.PROMOTION_NOMINATION_URL + 'delete-promotion-nomination/' + promotionNominationId;
            return apiHttpService.DELETE(url);
        }

    }
})();