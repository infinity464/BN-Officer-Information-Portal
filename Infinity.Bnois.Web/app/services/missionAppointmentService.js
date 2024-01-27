(function () {
    'use strict';
    angular.module('app').service('missionAppointmentService', ['dataConstants', 'apiHttpService', missionAppointmentService]);

    function missionAppointmentService(dataConstants, apiHttpService) {
        var service = {
            getMissionAppointments: getMissionAppointments,
            getMissionAppointment: getMissionAppointment,
            getMissionAppointmentByMission: getMissionAppointmentByMission,
            saveMissionAppointment: saveMissionAppointment,
            updateMissionAppointment: updateMissionAppointment,
            deleteMissionAppointment: deleteMissionAppointment,
            getMissionSchedule: getMissionSchedule
        };

        return service;
        function getMissionAppointments(pageSize, pageNumber, searchText, missionId) {
            var url = dataConstants.MISSION_APPOINTMENTS_URL + 'get-mission-appointments?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText ;
            return apiHttpService.GET(url);
        }

        function getMissionAppointment(missionAppointmentId) {
            var url = dataConstants.MISSION_APPOINTMENTS_URL + 'get-mission-appointment?id=' + missionAppointmentId;
            return apiHttpService.GET(url);
        }

        function getMissionAppointmentByMission(missionId) {
            var url = dataConstants.MISSION_APPOINTMENTS_URL + 'get-mission-appointment-by-mission?missionId=' + missionId;
            return apiHttpService.GET(url);
        }
        function getMissionSchedule(missionId) {
            var url = dataConstants.MISSION_APPOINTMENTS_URL + 'get-mission-schedule?missionId=' + missionId;
            return apiHttpService.GET(url);
        }

        function saveMissionAppointment(data) {
            var url = dataConstants.MISSION_APPOINTMENTS_URL + 'save-mission-appointment';
            return apiHttpService.POST(url, data);
        }

        function updateMissionAppointment(missionAppointmentId, data) {
            var url = dataConstants.MISSION_APPOINTMENTS_URL + 'update-mission-appointment/' + missionAppointmentId;
            return apiHttpService.PUT(url, data);
        }

        function deleteMissionAppointment(missionAppointmentId) {
            var url = dataConstants.MISSION_APPOINTMENTS_URL + 'delete-mission-appointment/' + missionAppointmentId;
            return apiHttpService.DELETE(url);
        }

    }
})();