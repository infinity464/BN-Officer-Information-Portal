(function () {
	'use strict';
	angular.module('app').service('visitCategoryService', ['dataConstants', 'apiHttpService', visitCategoryService]);

	function visitCategoryService(dataConstants, apiHttpService) {
		var service = {
            getVisitCategories: getVisitCategories,
            getVisitCategory: getVisitCategory,
            saveVisitCategory: saveVisitCategory,
            updateVisitCategory: updateVisitCategory,
            deleteVisitCategory: deleteVisitCategory
		};

		return service;
        function getVisitCategories(pageSize, pageNumber, searchText) {
            var url = dataConstants.VISIT_CATEGORIES_URL + 'get-visit-categories?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

        function getVisitCategory(visitCategoryId) {
            var url = dataConstants.VISIT_CATEGORIES_URL + 'get-visit-category?id=' + visitCategoryId;

			return apiHttpService.GET(url);
		}

        function saveVisitCategory(data) {
            var url = dataConstants.VISIT_CATEGORIES_URL + 'save-visit-category';
			return apiHttpService.POST(url, data);
		}

        function updateVisitCategory(visitCategoryId, data) {
            var url = dataConstants.VISIT_CATEGORIES_URL + 'update-visit-category/' + visitCategoryId;
			return apiHttpService.PUT(url, data);
		}

        function deleteVisitCategory(visitCategoryId) {
            var url = dataConstants.VISIT_CATEGORIES_URL + 'delete-visit-category/' + visitCategoryId;
			return apiHttpService.DELETE(url);
		}


	}
})();