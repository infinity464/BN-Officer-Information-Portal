(function () {
	'use strict';
	angular.module('app').service('commendationService', ['dataConstants', 'apiHttpService', commendationService]);

	function commendationService(dataConstants, apiHttpService) {
		var service = {
            getCommendations: getCommendations,
            getCommendation: getCommendation,
            saveCommendation: saveCommendation,
            updateCommendation: updateCommendation,
            deleteCommendation: deleteCommendation
		};

		return service;
        function getCommendations(pageSize, pageNumber, searchText) {
            var url = dataConstants.COMMENDATION_URL + 'get-commendations?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

        function getCommendation(commendationId) {
            var url = dataConstants.COMMENDATION_URL + 'get-commendation?id=' + commendationId;

			return apiHttpService.GET(url);
		}

        function saveCommendation(data) {
            var url = dataConstants.COMMENDATION_URL + 'save-commendation';
			return apiHttpService.POST(url, data);
		}

        function updateCommendation(commendationId, data) {
            var url = dataConstants.COMMENDATION_URL + 'update-commendation/' + commendationId;
			return apiHttpService.PUT(url, data);
		}

        function deleteCommendation(commendationId) {
            var url = dataConstants.COMMENDATION_URL + 'delete-commendation/' + commendationId;
			return apiHttpService.DELETE(url);
		}


	}
})();