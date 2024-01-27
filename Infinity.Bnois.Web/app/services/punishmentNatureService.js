(function () {
	'use strict';
	angular.module('app').service('punishmentNatureService', ['dataConstants', 'apiHttpService', punishmentNatureService]);

	function punishmentNatureService(dataConstants, apiHttpService) {
		var service = {
            getPunishmentNatures: getPunishmentNatures,
            getPunishmentNature: getPunishmentNature,
            savePunishmentNature: savePunishmentNature,
            updatePunishmentNature: updatePunishmentNature,
            deletePunishmentNature: deletePunishmentNature
		};

		return service;
        function getPunishmentNatures(pageSize, pageNumber, searchText) {
            var url = dataConstants.PUNISHMENT_NATURE_URL + 'get-punishment-natures?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

        function getPunishmentNature(punishmentNatureId) {
            var url = dataConstants.PUNISHMENT_NATURE_URL + 'get-punishment-nature?id=' + punishmentNatureId;

			return apiHttpService.GET(url);
		}

        function savePunishmentNature(data) {
            var url = dataConstants.PUNISHMENT_NATURE_URL + 'save-punishment-nature';
			return apiHttpService.POST(url, data);
		}

        function updatePunishmentNature(punishmentNatureId, data) {
            var url = dataConstants.PUNISHMENT_NATURE_URL + 'update-punishment-nature/' + punishmentNatureId;
			return apiHttpService.PUT(url, data);
		}

        function deletePunishmentNature(punishmentNatureId) {
            var url = dataConstants.PUNISHMENT_NATURE_URL + 'delete-punishment-nature/' + punishmentNatureId;
			return apiHttpService.DELETE(url);
		}


	}
})();