(function () {
    'use strict';
    angular.module('app').service('previousMissionService', ['dataConstants', 'apiHttpService', previousMissionService]);

    function previousMissionService(dataConstants, apiHttpService) {
        var service = {
            getPreviousMissions: getPreviousMissions,
            getPreviousMission: getPreviousMission,
            savePreviousMission: savePreviousMission,
            updatePreviousMission: updatePreviousMission,
            deletePreviousMission: deletePreviousMission
        };

        return service;


        function getPreviousMissions(employeeId) {
            var url = dataConstants.PREVIOUS_MISSION_URL + 'get-previous-missions?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function getPreviousMission(previousMissionId) {
            var url = dataConstants.PREVIOUS_MISSION_URL + 'get-previous-mission?&previousMissionId=' + previousMissionId;
            return apiHttpService.GET(url);
        }

        function savePreviousMission(employeeId, data) {
            var url = dataConstants.PREVIOUS_MISSION_URL + 'save-previous-mission/' + employeeId;
            return apiHttpService.POST(url, data);
        }
        function updatePreviousMission(previousMissionId, data) {
            var url = dataConstants.PREVIOUS_MISSION_URL + 'update-previous-mission/' + previousMissionId;
            return apiHttpService.PUT(url, data);
        }

        function deletePreviousMission(id) {
            var url = dataConstants.PREVIOUS_MISSION_URL + 'delete-previous-mission/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();