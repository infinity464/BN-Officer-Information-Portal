(function () {
	'use strict';
	angular.module('app').service('serviceExamCategoryService', ['dataConstants', 'apiHttpService', serviceExamCategoryService]);

	function serviceExamCategoryService(dataConstants, apiHttpService) {
		var service = {
            getServiceExamCategories: getServiceExamCategories,
            getServiceExamCategory: getServiceExamCategory,
            saveServiceExamCategory: saveServiceExamCategory,
            updateServiceExamCategory: updateServiceExamCategory,
            deleteServiceExamCategory: deleteServiceExamCategory
		};

		return service;
        function getServiceExamCategories(pageSize, pageNumber, searchText) {
            var url = dataConstants.SERVICE_EXAM_CATEGORIES_URL + 'get-service-exam-categories?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

        function getServiceExamCategory(serviceExamCategoryId) {
            var url = dataConstants.SERVICE_EXAM_CATEGORIES_URL + 'get-service-exam-category?id=' + serviceExamCategoryId;

			return apiHttpService.GET(url);
		}

        function saveServiceExamCategory(data) {
            var url = dataConstants.SERVICE_EXAM_CATEGORIES_URL + 'save-service-exam-category';
			return apiHttpService.POST(url, data);
		}

        function updateServiceExamCategory(serviceExamCategoryId, data) {
            var url = dataConstants.SERVICE_EXAM_CATEGORIES_URL + 'update-service-exam-category/' + serviceExamCategoryId;
			return apiHttpService.PUT(url, data);
		}

        function deleteServiceExamCategory(serviceExamCategoryId) {
            var url = dataConstants.SERVICE_EXAM_CATEGORIES_URL + 'delete-service-exam-category/' + serviceExamCategoryId;
			return apiHttpService.DELETE(url);
		}


	}
})();