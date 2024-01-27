(function () {
	'use strict';
	angular.module('app').service('employeeLprService', ['dataConstants', 'apiHttpService', employeeLprService]);

	function employeeLprService(dataConstants, apiHttpService) {
		var service = {
			getEmployeeLprs: getEmployeeLprs,
			getEmployeeLpr: getEmployeeLpr,
			saveEmployeeLpr: saveEmployeeLpr,
			updateEmployeeLpr: updateEmployeeLpr,
			deleteEmployeeLpr: deleteEmployeeLpr,
			getRetiredDate: getRetiredDate
		};

		return service;
		function getEmployeeLprs(pageSize, pageNumber, searchText) {
			var url = dataConstants.EMPLOYEE_LPR_URL + 'get-employee-lprs?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

		function getEmployeeLpr(Id) {
			var url = dataConstants.EMPLOYEE_LPR_URL + 'get-employee-lpr?Id=' + Id;
			return apiHttpService.GET(url);
		}

		function saveEmployeeLpr(data) {
			var url = dataConstants.EMPLOYEE_LPR_URL + 'save-employee-lpr';
			return apiHttpService.POST(url, data);
		}

		function updateEmployeeLpr(Id, data) {
			var url = dataConstants.EMPLOYEE_LPR_URL + 'update-employee-lpr/' + Id;
			return apiHttpService.PUT(url, data);
		}

		function deleteEmployeeLpr(Id) {
			var url = dataConstants.EMPLOYEE_LPR_URL + 'delete-employee-lpr/' + Id;
			return apiHttpService.DELETE(url);
		}

		function getRetiredDate(Id) {
		var url = dataConstants.AGE_SERVICE_POLICY_URL + 'get-lpr-date?Id=' + Id;
			return apiHttpService.GET(url);
		}

	}
})();