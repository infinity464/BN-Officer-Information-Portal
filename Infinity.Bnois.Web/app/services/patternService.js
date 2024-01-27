(function () {
	'use strict';
	angular.module('app').service('patternService', ['dataConstants', 'apiHttpService', patternService]);

	function patternService(dataConstants, apiHttpService) {
		var service = {
			getPatterns: getPatterns,
			getPattern: getPattern,
			savePattern: savePattern,
			updatePattern: updatePattern,
			deletePattern: deletePattern
		};

		return service;
		function getPatterns(pageSize, pageNumber, searchText) {
			var url = dataConstants.Pattern_URL + 'get-patterns?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

		function getPattern(patternId) {
			var url = dataConstants.Pattern_URL + 'get-pattern?id=' + patternId;

			return apiHttpService.GET(url);
		}

		function savePattern(data) {
			var url = dataConstants.Pattern_URL + 'save-pattern';
			return apiHttpService.POST(url, data);
		}

		function updatePattern(id, data) {
			var url = dataConstants.Pattern_URL + 'update-pattern/' + id;
			return apiHttpService.PUT(url, data);
		}

		function deletePattern(id) {
			var url = dataConstants.Pattern_URL + 'delete-pattern/' + id;
			return apiHttpService.DELETE(url);
		}


	}
})();