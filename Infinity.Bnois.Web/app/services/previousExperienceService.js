(function () {
    'use strict';
    angular.module('app').service('previousExperienceService', ['dataConstants', 'apiHttpService', previousExperienceService]);

    function previousExperienceService(dataConstants, apiHttpService) {
        var service = {
            getPreviousExperience: getPreviousExperience,
            updatePreviousExperience: updatePreviousExperience
        };

        return service;

        function getPreviousExperience(employeeId) {
            var url = dataConstants.PREVIOUS_EXPERIENCE_URL + 'get-employee-previous-experience?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }
        
        function updatePreviousExperience(employeeId, data) {
            var url = dataConstants.PREVIOUS_EXPERIENCE_URL + 'update-employee-previous-experience/' + employeeId;
            return apiHttpService.PUT(url, data);
        }
    }
})();