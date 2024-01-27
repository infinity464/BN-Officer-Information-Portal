(function () {
	'use strict';
	angular.module('app').service('punishmentCategoryService', ['dataConstants', 'apiHttpService', punishmentCategoryService]);

	function punishmentCategoryService(dataConstants, apiHttpService) {
		var service = {
            getPunishmentCategories: getPunishmentCategories,
            getPunishmentCategory: getPunishmentCategory,
            savePunishmentCategory: savePunishmentCategory,
            updatePunishmentCategory: updatePunishmentCategory,
            deletePunishmentCategory: deletePunishmentCategory
		};

		return service;
        function getPunishmentCategories(pageSize, pageNumber, searchText) {
            var url = dataConstants.PUNISHMENT_CATEGORY_URL + 'get-punishment-categories?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

        function getPunishmentCategory(punishmentCategoryId) {
            var url = dataConstants.PUNISHMENT_CATEGORY_URL + 'get-punishment-category?id=' + punishmentCategoryId;

			return apiHttpService.GET(url);
		}

        function savePunishmentCategory(data) {
            var url = dataConstants.PUNISHMENT_CATEGORY_URL + 'save-punishment-category';
			return apiHttpService.POST(url, data);
		}

        function updatePunishmentCategory(punishmentCategoryId, data) {
            var url = dataConstants.PUNISHMENT_CATEGORY_URL + 'update-punishment-category/' + punishmentCategoryId;
			return apiHttpService.PUT(url, data);
		}

        function deletePunishmentCategory(punishmentCategoryId) {
            var url = dataConstants.PUNISHMENT_CATEGORY_URL + 'delete-punishment-category/' + punishmentCategoryId;
			return apiHttpService.DELETE(url);
		}


	}
})();