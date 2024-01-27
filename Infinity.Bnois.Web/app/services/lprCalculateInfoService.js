(function () {
	'use strict';
	angular.module('app').service('lprCalculateInfoService', ['dataConstants', 'apiHttpService', lprCalculateInfoService]);

	function lprCalculateInfoService(dataConstants, apiHttpService) {
		var service = {
			getLprCalculateInfoes: getLprCalculateInfoes,
			getLprCalculateInfo: getLprCalculateInfo,
			saveLprCalculateInfo: saveLprCalculateInfo,
			updateLprCalculateInfo: updateLprCalculateInfo,
			deleteLprCalculateInfo: deleteLprCalculateInfo
		};

		return service;
		function getLprCalculateInfoes(pageSize, pageNumber, searchText) {
			var url = dataConstants.LPR_CALCULATE_INFO_URL + 'get-lprCalculateInfoes?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

		function getLprCalculateInfo(id) {
			var url = dataConstants.LPR_CALCULATE_INFO_URL + 'get-lprCalculateInfo?id=' + id;
			return apiHttpService.GET(url);
		}

		function saveLprCalculateInfo(data) {
			var url = dataConstants.LPR_CALCULATE_INFO_URL + 'save-lprCalculateInfo';
			return apiHttpService.POST(url, data);
		}

		function updateLprCalculateInfo(id, data) {
			var url = dataConstants.LPR_CALCULATE_INFO_URL + 'update-lprCalculateInfo/' + id;
			return apiHttpService.PUT(url, data);
		}

		function deleteLprCalculateInfo(id) {
			var url = dataConstants.LPR_CALCULATE_INFO_URL + 'delete-lprCalculateInfo/' + id;
			return apiHttpService.DELETE(url);
		}

	}
})();