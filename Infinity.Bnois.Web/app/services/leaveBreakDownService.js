(function () {
	'use strict';
	angular.module('app').service('leaveBreakDownService', ['dataConstants', 'apiHttpService', leaveBreakDownService]);

	function leaveBreakDownService(dataConstants, apiHttpService) {
		var service = {
			getEmployeeLeaveBreakDown: getEmployeeLeaveBreakDown
		};
		return service;
		
		function getEmployeeLeaveBreakDown(employeeId) {

			var url = dataConstants.LEAVE_BREAK_DOWN_URL + 'get-leave-break-downs?employeeId=' + employeeId;
			return apiHttpService.GET(url);
		}

	}
})();