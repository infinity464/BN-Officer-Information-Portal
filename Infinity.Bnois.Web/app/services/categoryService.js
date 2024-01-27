(function () {
    'use strict';
    angular.module('app').service('categoryService', ['dataConstants', 'apiHttpService', categoryService]);

    function categoryService(dataConstants, apiHttpService) {
        var service = {
            getCategories: getCategories,
            getCategory: getCategory,
            saveCategory: saveCategory,
            getOfficerCategorySelectModels: getOfficerCategorySelectModels,
            updateCategory: updateCategory,
            deleteCategory: deleteCategory
        };

        return service;
        function getCategories(pageSize, pageNumber, searchText) {
            var url = dataConstants.CATEGORY_URL + 'get-categories?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getCategory(id) {
            var url = dataConstants.CATEGORY_URL + 'get-category?id=' + id;
            return apiHttpService.GET(url);
        }
        function getOfficerCategorySelectModels() {
            var url = dataConstants.CATEGORY_URL + 'get-category-select-models';
            return apiHttpService.GET(url);
        }

        function saveCategory(data) {
            var url = dataConstants.CATEGORY_URL + 'save-category';
            return apiHttpService.POST(url, data);
        }

        function updateCategory(id, data) {
            var url = dataConstants.CATEGORY_URL + 'update-category/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteCategory(id) {
            var url = dataConstants.CATEGORY_URL + 'delete-category/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();