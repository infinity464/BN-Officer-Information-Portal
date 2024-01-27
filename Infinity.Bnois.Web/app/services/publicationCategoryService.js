(function () {
	'use strict';
	angular.module('app').service('publicationCategoryService', ['dataConstants', 'apiHttpService', publicationCategoryService]);

	function publicationCategoryService(dataConstants, apiHttpService) {
		var service = {
            getPublicationCategories: getPublicationCategories,
            getPublicationCategory: getPublicationCategory,
            savePublicationCategory: savePublicationCategory,
            updatePublicationCategory: updatePublicationCategory,
            deletePublicationCategory: deletePublicationCategory
		};

		return service;
        function getPublicationCategories(pageSize, pageNumber, searchText) {
            var url = dataConstants.PUBLICATION_CATEGORY_URL + 'get-publication-categories?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

        function getPublicationCategory(publicationCategoryId) {
            var url = dataConstants.PUBLICATION_CATEGORY_URL + 'get-publication-category?id=' + publicationCategoryId;

			return apiHttpService.GET(url);
		}

        function savePublicationCategory(data) {
            var url = dataConstants.PUBLICATION_CATEGORY_URL + 'save-publication-category';
			return apiHttpService.POST(url, data);
		}

        function updatePublicationCategory(publicationCategoryId, data) {
            var url = dataConstants.PUBLICATION_CATEGORY_URL + 'update-publication-category/' + publicationCategoryId;
			return apiHttpService.PUT(url, data);
		}

        function deletePublicationCategory(publicationCategoryId) {
            var url = dataConstants.PUBLICATION_CATEGORY_URL + 'delete-publication-category/' + publicationCategoryId;
			return apiHttpService.DELETE(url);
		}


	}
})();