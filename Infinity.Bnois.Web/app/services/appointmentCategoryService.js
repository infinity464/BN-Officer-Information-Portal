(function () {
	'use strict';
	angular.module('app').service('appointmentCategoryService', ['dataConstants', 'apiHttpService', appointmentCategoryService]);

	function appointmentCategoryService(dataConstants, apiHttpService) {
		var service = {
            getAppointmentCategories: getAppointmentCategories,
			getAppointmentCategory: getAppointmentCategory,
			saveAppointmentCategory: saveAppointmentCategory,
			updateAppointmentCategory: updateAppointmentCategory,
			deleteAppointmentCategory: deleteAppointmentCategory
		};

		return service;
		function getAppointmentCategories(pageSize, pageNumber, searchText) {
			var url = dataConstants.Appointment_Category_URL + 'get-appointment-Categorys?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

		function getAppointmentCategory(appointmentNatureId) {
			var url = dataConstants.Appointment_Category_URL + 'get-appointment-Category?id=' + appointmentNatureId;

			return apiHttpService.GET(url);
		}

		function saveAppointmentCategory(data) {
			var url = dataConstants.Appointment_Category_URL + 'save-appointment-Category';
			return apiHttpService.POST(url, data);
		}

		function updateAppointmentCategory(id, data) {
			var url = dataConstants.Appointment_Category_URL + 'update-appointment-Category/' + id;
			return apiHttpService.PUT(url, data);
		}

		function deleteAppointmentCategory(id) {
			var url = dataConstants.Appointment_Category_URL + 'delete-appointment-Category/' + id;
			return apiHttpService.DELETE(url);
		}


	}
})();