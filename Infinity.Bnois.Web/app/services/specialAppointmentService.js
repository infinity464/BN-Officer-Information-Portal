(function () {
    'use strict';
    angular.module('app').service('specialAppointmentService', ['dataConstants', 'apiHttpService', specialAppointmentService]);

    function specialAppointmentService(dataConstants, apiHttpService) {
        var service = {
            getSpecialAppointments: getSpecialAppointments,
            getSpecialAppointment: getSpecialAppointment,
            saveSpecialAppointment: saveSpecialAppointment,
            updateSpecialAppointment: updateSpecialAppointment,
            deleteSpecialAppointment: deleteSpecialAppointment
        };

        return service;
        function getSpecialAppointments(pageSize, pageNumber, searchText) {
            var url = dataConstants.SPECIAL_APPOINTMENT_URL + 'get-special-apt-types?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getSpecialAppointment(id) {
            var url = dataConstants.SPECIAL_APPOINTMENT_URL + 'get-special-apt-type?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveSpecialAppointment(data) {
            var url = dataConstants.SPECIAL_APPOINTMENT_URL + 'save-special-apt-type';
            return apiHttpService.POST(url, data);
        }

        function updateSpecialAppointment(id, data) {
            var url = dataConstants.SPECIAL_APPOINTMENT_URL + 'update-special-apt-type/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteSpecialAppointment(id) {
            var url = dataConstants.SPECIAL_APPOINTMENT_URL + 'delete-special-apt-type/' + id;
            return apiHttpService.DELETE(url);
        }
        
    }
})();