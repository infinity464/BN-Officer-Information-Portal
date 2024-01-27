(function () {
    'use strict';
    angular.module('app').service('officeAppointmentService', ['dataConstants', 'apiHttpService', officeAppointmentService]);

    function officeAppointmentService(dataConstants, apiHttpService) {
        var service = {
            getOfficeAppointments: getOfficeAppointments,
            getOfficeAppointment: getOfficeAppointment,
            getOfficeAppointmentByOffice: getOfficeAppointmentByOffice,
            getAdditionalAppointmentByOffice: getAdditionalAppointmentByOffice,
            saveOfficeAppointment: saveOfficeAppointment,
            saveOfficeAdditionalAppointment: saveOfficeAdditionalAppointment,
            updateOfficeAppointment: updateOfficeAppointment,
            updateOfficeAdditionalAppointment: updateOfficeAdditionalAppointment,
            deleteOfficeAppointment: deleteOfficeAppointment,
            getCategoryByNature: getCategoryByNature,
            getAppointmentByOrganizationPattern: getAppointmentByOrganizationPattern,
            getAppointmentByShipType: getAppointmentByShipType
        };

        return service;
        function getOfficeAppointments(pageSize, pageNumber, searchText, officeId,type) {
            var url = dataConstants.OFFICE_APPOINTMENT_URL + 'get-office-appointments?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText + "&officeId=" + officeId +"&type="+type;
            return apiHttpService.GET(url);
        }

        function getOfficeAppointment(officeAppointmentId, officeId) {
            var url = dataConstants.OFFICE_APPOINTMENT_URL + 'get-office-appointment?id=' + officeAppointmentId + "&officeId=" + officeId;
            return apiHttpService.GET(url);
        }

        function getOfficeAppointmentByOffice(officeId) {
            var url = dataConstants.OFFICE_APPOINTMENT_URL + 'get-office-appointment-by-office?officeId=' + officeId;
            return apiHttpService.GET(url);
        }


        function getAppointmentByOrganizationPattern(officeId) {
            var url = dataConstants.OFFICE_APPOINTMENT_URL + 'get-appointment-by-organization-pattern?officeId=' + officeId;
            return apiHttpService.GET(url);
        }

        function getAppointmentByShipType(shipType) {
            var url = dataConstants.OFFICE_APPOINTMENT_URL + 'get-appointment-by-ship-type?shipType=' + shipType;
            return apiHttpService.GET(url);
        }

        function getAdditionalAppointmentByOffice(officeId) {
            var url = dataConstants.OFFICE_APPOINTMENT_URL + 'get-additional-appointment-by-office?officeId=' + officeId;
            return apiHttpService.GET(url);
        }


        function saveOfficeAppointment(data) {
            var url = dataConstants.OFFICE_APPOINTMENT_URL + 'save-office-appointment';
            return apiHttpService.POST(url, data);
        }
        function saveOfficeAdditionalAppointment(data) {
            var url = dataConstants.OFFICE_APPOINTMENT_URL + 'save-office-additional-appointment';
            return apiHttpService.POST(url, data);
        }

        function updateOfficeAppointment(officeAppointmentId, data) {
            var url = dataConstants.OFFICE_APPOINTMENT_URL + 'update-office-appointment/' + officeAppointmentId;
            return apiHttpService.PUT(url, data);
        }

        function updateOfficeAdditionalAppointment(officeAppointmentId, data) {
            var url = dataConstants.OFFICE_APPOINTMENT_URL + 'update-office-additional-appointment/' + officeAppointmentId;
            return apiHttpService.PUT(url, data);
        }

        function deleteOfficeAppointment(officeAppointmentId) {
            var url = dataConstants.OFFICE_APPOINTMENT_URL + 'delete-office-appointment/' + officeAppointmentId;
            return apiHttpService.DELETE(url);
        }

        function getCategoryByNature(appNatId) {
            var url = dataConstants.OFFICE_APPOINTMENT_URL + 'get-category-by-nature?id=' + appNatId;
            return apiHttpService.GET(url);
        }

       
    }
})();