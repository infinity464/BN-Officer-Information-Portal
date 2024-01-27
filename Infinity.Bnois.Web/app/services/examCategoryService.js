	(function () {
		'use strict';
		angular.module('app').service('examCategoryService', ['dataConstants', 'apiHttpService', examCategoryService]);

		function examCategoryService(dataConstants, apiHttpService) {
			var service = {
				getExamCategories: getExamCategories,
				getExamCategory: getExamCategory,
				saveExamCategory: saveExamCategory,
				deleteExamCategory: deleteExamCategory,
				updateExamCategory: updateExamCategory
			};

			return service;
			function getExamCategories(pageSize, pageNumber,searchText) {
				var url = dataConstants.EXAM_CATEGORY_URL + 'get-exam-categories?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
				return apiHttpService.GET(url);
			}

			function getExamCategory(id) {
				var url = dataConstants.EXAM_CATEGORY_URL + 'get-exam-category?id=' + id;
				return apiHttpService.GET(url);
			}

			function saveExamCategory(data) {
				var url = dataConstants.EXAM_CATEGORY_URL + 'save-exam-category';
				return apiHttpService.POST(url, data);
			}

			function deleteExamCategory(id) {
				var url = dataConstants.EXAM_CATEGORY_URL + 'delete-exam-category/' + id;
				return apiHttpService.DELETE(url);
			}
			function updateExamCategory(id, data) {
				var url = dataConstants.EXAM_CATEGORY_URL + 'update-exam-category/' + id;
				return apiHttpService.PUT(url, data);
			}

		}
	})();