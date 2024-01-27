(function () {
    'use strict';
    angular.module('app').service('oprAppointmentService', ['dataConstants', 'apiHttpService', oprAppointmentService]);

    function oprAppointmentService(dataConstants, apiHttpService) {
        var service = {
            getoprAppointments: getoprAppointments,
            getoprAppointment: getoprAppointment,
            saveoprAppointment: saveoprAppointment,
            deleteoprAppointment: deleteoprAppointment
        };

        return service;
        function getoprAppointments(employeeOprId) {
            var url = dataConstants.OPR_APPOINTMENT_URL + 'get-opr-appointments?id=' + employeeOprId;
            return apiHttpService.GET(url);
        }

        function getoprAppointment(oprAppointmentId) {
            var url = dataConstants.OPR_APPOINTMENT_URL + 'get-opr-appointment?id=' + oprAppointmentId;
            return apiHttpService.GET(url);
        }


        function saveoprAppointment(data) {
            var url = dataConstants.OPR_APPOINTMENT_URL + 'save-opr-appointment';
            return apiHttpService.POST(url, data);
        }

        function deleteoprAppointment(oprAppointmentId) {
            var url = dataConstants.OPR_APPOINTMENT_URL + 'delete-opr-appointment/' + oprAppointmentId;
            return apiHttpService.DELETE(url);
        }
    }
})();