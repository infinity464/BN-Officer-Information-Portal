(function () {
	'use strict';
	angular.module('app').service('leaveTypeService', ['dataConstants', 'apiHttpService', leaveTypeService]);

	function leaveTypeService(dataConstants, apiHttpService) {
		var service = {
			getLeaveTypes: getLeaveTypes,
			getLeaveType: getLeaveType,
			saveLeaveType: saveLeaveType,
			updateLeaveType: updateLeaveType,
			deleteLeaveType: deleteLeaveType
		};

		return service;
		function getLeaveTypes(pageSize, pageNumber, searchText) {
			var url = dataConstants.LEAVETYPE_URL + 'get-leave-types?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

		function getLeaveType(id) {
			var url = dataConstants.LEAVETYPE_URL + 'get-leave-type?id=' + id;
			return apiHttpService.GET(url);
		}

		function saveLeaveType(data) {
			var url = dataConstants.LEAVETYPE_URL + 'save-leave-type';
			return apiHttpService.POST(url, data);
		}

		function updateLeaveType(id, data) {
			var url = dataConstants.LEAVETYPE_URL + 'update-leave-type/' + id;
			return apiHttpService.PUT(url, data);
		}

		function deleteLeaveType(id) {
			var url = dataConstants.LEAVETYPE_URL + 'delete-leave-type/' + id;
			return apiHttpService.DELETE(url);
		}
	}
})();