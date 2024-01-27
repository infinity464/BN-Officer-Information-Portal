(function () {
    'use strict';
    angular.module('app').service('physicalConditionService', ['dataConstants', 'apiHttpService', physicalConditionService]);

    function physicalConditionService(dataConstants, apiHttpService) {
        var service = {
            getPhysicalConditions: getPhysicalConditions,
            getPhysicalCondition: getPhysicalCondition,
            updatePhysicalCondition: updatePhysicalCondition,
            imageUploadUrl: imageUploadUrl
        };

        return service;
        function imageUploadUrl(employeeId) {
            var url = dataConstants.PHYSICAL_CONDITION_URL + 'upload-medical-category-image?employeeId=' + employeeId;
            return url;
        }

        function getPhysicalConditions(employeeId) {
            var url = dataConstants.PHYSICAL_CONDITION_URL + 'get-physical-conditions?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }
        function getPhysicalCondition(employeeId) {
            var url = dataConstants.PHYSICAL_CONDITION_URL + 'get-physical-condition?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function updatePhysicalCondition(employeeId, data) {
            var url = dataConstants.PHYSICAL_CONDITION_URL + 'update-physical-condition/' + employeeId;
            return apiHttpService.PUT(url, data);
        }
    }
})();