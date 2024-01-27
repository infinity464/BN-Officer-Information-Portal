(function () {
    'use strict';
    angular.module('app').service('nominationScheduleService', ['dataConstants', 'apiHttpService', nominationScheduleService]);

    function nominationScheduleService(dataConstants, apiHttpService) {
        var service = {
            getNominationSchedules: getNominationSchedules,
            getNominationSchedule: getNominationSchedule,
            saveNominationSchedule: saveNominationSchedule,
            updateNominationSchedule: updateNominationSchedule,
            deleteNominationSchedule: deleteNominationSchedule
        };

        return service;
        function getNominationSchedules(pageSize, pageNumber,searchText,type) {
            var url = dataConstants.NOMINATION_SCHEDULES_URL + 'get-nomination-schedules?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText+"&type="+type;
            return apiHttpService.GET(url);
        }

        function getNominationSchedule(nominationScheduleId) {
            var url = dataConstants.NOMINATION_SCHEDULES_URL + 'get-nomination-schedule?id=' + nominationScheduleId;
            return apiHttpService.GET(url);
        }


        function saveNominationSchedule(data) {
            var url = dataConstants.NOMINATION_SCHEDULES_URL + 'save-nomination-schedule';
            return apiHttpService.POST(url, data);
        }

        function updateNominationSchedule(nominationScheduleId, data) {
            var url = dataConstants.NOMINATION_SCHEDULES_URL + 'update-nomination-schedule/' + nominationScheduleId;
            return apiHttpService.POST(url, data);
        }

        function deleteNominationSchedule(nominationScheduleId) {
            var url = dataConstants.NOMINATION_SCHEDULES_URL + 'delete-nomination-schedule/' + nominationScheduleId;
            return apiHttpService.DELETE(url);
        }
        
    }
})();