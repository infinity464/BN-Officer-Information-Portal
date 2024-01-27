(function () {
	'use strict';
	angular.module('app').service('religionService', ['dataConstants', 'apiHttpService', religionService]);

	function religionService(dataConstants, apiHttpService) {
		var service = {
			getReligions: getReligions,
			getReligion: getReligion,
			saveReligion: saveReligion,
			deleteReligion: deleteReligion,
			updateReligion: updateReligion
		};

		return service;
		function getReligions(pageSize, pageNumber, searchText) {
			var url = dataConstants.RELIGION_URL + 'get-religions?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

        function getReligion(religionId) {         
			var url = dataConstants.RELIGION_URL + 'get-religion?id=' + religionId;
			return apiHttpService.GET(url);
		}

		function saveReligion(data) {
			var url = dataConstants.RELIGION_URL + 'save-religion';
			return apiHttpService.POST(url, data);
		}

        function deleteReligion(id) {
            var url = dataConstants.RELIGION_URL + 'delete-religion/' + id;
			return apiHttpService.DELETE(url);
		}
        function updateReligion(id, data) {
            var url = dataConstants.RELIGION_URL + 'update-religion/' + id;
			return apiHttpService.PUT(url, data);
		}

	}
})();