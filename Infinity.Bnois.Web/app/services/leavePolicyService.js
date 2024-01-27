(function () {
	'use strict';
	angular.module('app').service('leavePolicyService', ['dataConstants', 'apiHttpService', leavePolicyService]);

	function leavePolicyService(dataConstants, apiHttpService) {
		var service = {
			getLeavePolicyies: getLeavePolicyies,
			getLeavePolicy: getLeavePolicy,
			saveLeavePolicy: saveLeavePolicy,
			updateLeavePolicy: updateLeavePolicy,
			deleteLeavePolicy: deleteLeavePolicy
		};

		return service;
		function getLeavePolicyies(pageSize, pageNumber, searchText) {
			var url = dataConstants.LEAVEPOLICY_URL + 'get-leavePolicyies?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

		function getLeavePolicy(id) {
            var url = dataConstants.LEAVEPOLICY_URL + 'get-leavePolicy?id=' + id;
			return apiHttpService.GET(url);
		}

		function saveLeavePolicy(data) {
			var url = dataConstants.LEAVEPOLICY_URL + 'save-leavePolicy';
			return apiHttpService.POST(url, data);
		}

		function updateLeavePolicy(id, data) {
			var url = dataConstants.LEAVEPOLICY_URL + 'update-leavePolicy/' + id;
			return apiHttpService.PUT(url, data);
		}

		function deleteLeavePolicy(id) {
			var url = dataConstants.LEAVEPOLICY_URL + 'delete-leavePolicy/' + id;
			return apiHttpService.DELETE(url);
		}
	}
})();