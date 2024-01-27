(function () {
    'use strict';
    angular.module('app').service('previousPunishmentService', ['dataConstants', 'apiHttpService', previousPunishmentService]);

    function previousPunishmentService(dataConstants, apiHttpService) {
        var service = {
            getPreviousPunishments: getPreviousPunishments,
            getPreviousPunishment: getPreviousPunishment,
            savePreviousPunishment: savePreviousPunishment,
            updatePreviousPunishment: updatePreviousPunishment,
            deletePreviousPunishment: deletePreviousPunishment
        };

        return service;


        function getPreviousPunishments(employeeId) {
            var url = dataConstants.PREVIOUS_PUNISHMENT_URL + 'get-previous-punishments?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function getPreviousPunishment(previousPunishmentId) {
            var url = dataConstants.PREVIOUS_PUNISHMENT_URL + 'get-previous-punishment?&previousPunishmentId=' + previousPunishmentId;
            return apiHttpService.GET(url);
        }

        function savePreviousPunishment(employeeId, data) {
            var url = dataConstants.PREVIOUS_PUNISHMENT_URL + 'save-previous-punishment/' + employeeId;
            return apiHttpService.POST(url, data);
        }
        function updatePreviousPunishment(previousPunishmentId, data) {
            var url = dataConstants.PREVIOUS_PUNISHMENT_URL + 'update-previous-punishment/' + previousPunishmentId;
            return apiHttpService.PUT(url, data);
        }

        function deletePreviousPunishment(id) {
            var url = dataConstants.PREVIOUS_PUNISHMENT_URL + 'delete-previous-punishment/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();