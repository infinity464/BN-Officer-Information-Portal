(function () {
	'use strict';
	angular.module('app').service('occupationService', ['dataConstants', 'apiHttpService', occupationService]);

	function occupationService(dataConstants, apiHttpService) {
		var service = {
			getOccupations: getOccupations,
			getOccupation: getOccupation,
			saveOccupation: saveOccupation,
			deleteOccupation: deleteOccupation,
			updateOccupation: updateOccupation
		};

		return service;
		function getOccupations(pageSize, pageNumber, searchText) {
            var url = dataConstants.OCCUPATION_URL + 'get-occupations?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

        function getOccupation(occupationId) {         
            var url = dataConstants.OCCUPATION_URL + 'get-occupation?id=' + occupationId;
			return apiHttpService.GET(url);
		}

		function saveOccupation(data) {
            var url = dataConstants.OCCUPATION_URL + 'save-occupation';
			return apiHttpService.POST(url, data);
		}

        function deleteOccupation(id) {
            var url = dataConstants.OCCUPATION_URL + 'delete-occupation/' + id;
			return apiHttpService.DELETE(url);
		}
        function updateOccupation(id, data) {
            var url = dataConstants.OCCUPATION_URL + 'update-occupation/' + id;
			return apiHttpService.PUT(url, data);
		}

	}
})();