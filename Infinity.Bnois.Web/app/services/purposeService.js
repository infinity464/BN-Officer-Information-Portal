(function () {
	'use strict';
	angular.module('app').service('purposeService', ['dataConstants', 'apiHttpService', purposeService]);

	function purposeService(dataConstants, apiHttpService) {
		var service = {
			getPurposes: getPurposes,
			getPurpose: getPurpose,
			savePurpose: savePurpose,
			updatePurpose: updatePurpose,
			deletePurpose: deletePurpose
		};

		return service;
		function getPurposes(pageSize, pageNumber, searchText) {
			var url = dataConstants.PURPOSE_URL + 'get-purposes?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

		function getPurpose(id) {
			var url = dataConstants.PURPOSE_URL + 'get-purpose?id=' + id;
			return apiHttpService.GET(url);
		}

		function savePurpose(data) {
			var url = dataConstants.PURPOSE_URL + 'save-purpose';
			return apiHttpService.POST(url, data);
		}

		function updatePurpose(id, data) {
			var url = dataConstants.PURPOSE_URL + 'update-purpose/' + id;
			return apiHttpService.PUT(url, data);
		}

		function deletePurpose(id) {
			var url = dataConstants.PURPOSE_URL + 'delete-purpose/' + id;
			return apiHttpService.DELETE(url);
		}
	}
})();