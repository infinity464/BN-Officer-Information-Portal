(function () {
	'use strict';
	angular.module('app').service('appointmentNatureService', ['dataConstants', 'apiHttpService', appointmentNatureService]);

	function appointmentNatureService(dataConstants, apiHttpService) {
		var service = {
			getAppointmentNatures: getAppointmentNatures,
			getAppointmentNature: getAppointmentNature,
			saveAppointmentNature: saveAppointmentNature,
			updateAppointmentNature: updateAppointmentNature,
			deleteAppointmentNature: deleteAppointmentNature
		};

		return service;
		function getAppointmentNatures(pageSize, pageNumber, searchText) {
			var url = dataConstants.Appointment_Nature_URL + 'get-appointment-Natures?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

		function getAppointmentNature(appointmentNatureId) {
			var url = dataConstants.Appointment_Nature_URL + 'get-appointment-Nature?id=' + appointmentNatureId;

			return apiHttpService.GET(url);
		}

		function saveAppointmentNature(data) {
			var url = dataConstants.Appointment_Nature_URL + 'save-appointment-Nature';
			return apiHttpService.POST(url, data);
		}

		function updateAppointmentNature(id, data) {
			var url = dataConstants.Appointment_Nature_URL + 'update-appointment-Nature/' + id;
			return apiHttpService.PUT(url, data);
		}

		function deleteAppointmentNature(id) {
			var url = dataConstants.Appointment_Nature_URL + 'delete-appointment-Nature/' + id;
			return apiHttpService.DELETE(url);
		}


	}
})();