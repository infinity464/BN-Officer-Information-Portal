(function () {
    'use strict';
    angular.module('app').service('promotionPolicyService', ['dataConstants', 'apiHttpService', promotionPolicyService]);

    function promotionPolicyService(dataConstants, apiHttpService) {
        var service = {
            getPromotionPolicies: getPromotionPolicies,
            getPromotionPolicy: getPromotionPolicy,
            savePromotionPolicy: savePromotionPolicy,
            updatePromotionPolicy: updatePromotionPolicy,
            deletePromotionPolicy: deletePromotionPolicy
        };

        return service;
        function getPromotionPolicies(pageSize, pageNumber, searchText) {
            var url = dataConstants.PROMOTION_POLICY_URL + 'get-promotion-policies';
            return apiHttpService.GET(url);
        }

        function getPromotionPolicy(id) {
            var url = dataConstants.PROMOTION_POLICY_URL + 'get-promotion-policy?id=' + id;
            return apiHttpService.GET(url);
        }

        function savePromotionPolicy(data) {
            var url = dataConstants.PROMOTION_POLICY_URL + 'save-promotion-policy';
            return apiHttpService.POST(url, data);
        }

        function updatePromotionPolicy(id, data) {
            var url = dataConstants.PROMOTION_POLICY_URL + 'update-promotion-policy/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deletePromotionPolicy(id) {
            var url = dataConstants.PROMOTION_POLICY_URL + 'delete-promotion-policy/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();