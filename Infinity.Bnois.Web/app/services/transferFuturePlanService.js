(function () {
    'use strict';
    angular.module('app').service('transferFuturePlanService', ['dataConstants', 'apiHttpService', transferFuturePlanService]);

    function transferFuturePlanService(dataConstants, apiHttpService) {
        var service = {
            getTransferFuturePlans: getTransferFuturePlans,
            getTransferFuturePlan: getTransferFuturePlan,
            saveTransferFuturePlan: saveTransferFuturePlan,
            updateTransferFuturePlan: updateTransferFuturePlan,
            deleteTransferFuturePlan: deleteTransferFuturePlan
        };

        return service;
        function getTransferFuturePlans(pNo) {
            var url = dataConstants.EMPLOYEE_TRANSFER_FUTURE_PLAN_URL + 'get-transfer-future-plans?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getTransferFuturePlan(transferFuturePlanId) {
            var url = dataConstants.EMPLOYEE_TRANSFER_FUTURE_PLAN_URL + 'get-transfer-future-plan?id=' + transferFuturePlanId;
            return apiHttpService.GET(url);
        }

        function saveTransferFuturePlan(data) {
            var url = dataConstants.EMPLOYEE_TRANSFER_FUTURE_PLAN_URL + 'save-transfer-future-plan';
            return apiHttpService.POST(url, data);
        }

        function updateTransferFuturePlan(transferFuturePlanId, data) {
            var url = dataConstants.EMPLOYEE_TRANSFER_FUTURE_PLAN_URL + 'update-transfer-future-plan/' + transferFuturePlanId;
            return apiHttpService.PUT(url, data);
        }

        function deleteTransferFuturePlan(transferFuturePlanId) {
            var url = dataConstants.EMPLOYEE_TRANSFER_FUTURE_PLAN_URL + 'delete-transfer-future-plan/' + transferFuturePlanId;
            return apiHttpService.DELETE(url);
        }
        
    }
})();