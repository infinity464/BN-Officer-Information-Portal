(function () {
    'use strict';
    angular.module('app').service('socialAttributeService', ['dataConstants', 'apiHttpService', socialAttributeService]);

    function socialAttributeService(dataConstants, apiHttpService) {
        var service = {
            getSocialAttribute: getSocialAttribute,
            updateSocialAttribute: updateSocialAttribute
        };

        return service;

        function getSocialAttribute(employeeId) {
            var url = dataConstants.SOCIAL_ATTRIBUTE_URL + 'get-employee-social-attribute?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }
        
        function updateSocialAttribute(employeeId, data) {
            var url = dataConstants.SOCIAL_ATTRIBUTE_URL + 'update-employee-social-attribute/' + employeeId;
            return apiHttpService.PUT(url, data);
        }
    }
})();