(function () {
    'use strict';
    angular.module('app').service('ageServicePolicyService', ['dataConstants', 'apiHttpService', ageServicePolicyService]);

    function ageServicePolicyService(dataConstants, apiHttpService) {
        var service = {
            getAgeServicePolicies: getAgeServicePolicies,
            getAgeServicePolicy: getAgeServicePolicy,
            saveAgeServicePolicy: saveAgeServicePolicy,
            updateAgeServicePolicy: updateAgeServicePolicy,
            deleteAgeServicePolicy: deleteAgeServicePolicy
        };

        return service;
        function getAgeServicePolicies(pageSize, pageNumber, searchText) {
            var url = dataConstants.AGE_SERVICE_POLICY_URL + 'get-age-service-policies?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getAgeServicePolicy(id) {
            var url = dataConstants.AGE_SERVICE_POLICY_URL + 'get-age-service-policy?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveAgeServicePolicy(data) {
            var url = dataConstants.AGE_SERVICE_POLICY_URL + 'save-age-service-policy';
            return apiHttpService.POST(url, data);
        }

        function updateAgeServicePolicy(id, data) {
            var url = dataConstants.AGE_SERVICE_POLICY_URL + 'update-age-service-policy/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteAgeServicePolicy(id) {
            var url = dataConstants.AGE_SERVICE_POLICY_URL + 'delete-age-service-policy/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();