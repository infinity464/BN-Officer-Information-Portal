(function () {
    'use strict';
    angular.module('app').service('extraAppointmentService', ['dataConstants', 'apiHttpService', extraAppointmentService]);

    function extraAppointmentService(dataConstants, apiHttpService) {
        var service = {
            getExtraAppointments: getExtraAppointments,
            getExtraAppointment: getExtraAppointment,
            saveExtraAppointment: saveExtraAppointment,
            updateExtraAppointment: updateExtraAppointment,
            deleteExtraAppointment: deleteExtraAppointment

        };

        return service;

     


        function getExtraAppointments(pageSize, pageNumber, searchText) {
            var url = dataConstants.EXTRA_APPOINTMENT_URL + 'get-extra-appointments?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getExtraAppointment(extraAppointmentId) {
            var url = dataConstants.EXTRA_APPOINTMENT_URL + 'get-extra-appointment?id=' + extraAppointmentId;
            return apiHttpService.GET(url);
        }


        function saveExtraAppointment(data) {
            var url = dataConstants.EXTRA_APPOINTMENT_URL + 'save-extra-appointment';
            return apiHttpService.POST(url, data);
        }

        function updateExtraAppointment(extraAppointmentId, data) {
            var url = dataConstants.EXTRA_APPOINTMENT_URL + 'update-extra-appointment/' + extraAppointmentId;
            return apiHttpService.PUT(url, data);
        }

        function deleteExtraAppointment(extraAppointmentId) {
            var url = dataConstants.EXTRA_APPOINTMENT_URL + 'delete-extra-appointment/' + extraAppointmentId;
            return apiHttpService.DELETE(url);
        }

    }
})();