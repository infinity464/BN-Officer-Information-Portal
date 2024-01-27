(function () {
    'use strict';
    angular.module('app').service('subCategoryService', ['dataConstants', 'apiHttpService', subCategoryService]);

    function subCategoryService(dataConstants, apiHttpService) {
        var service = {
            getSubCategories: getSubCategories,
            getSubCategory: getSubCategory,
            getSubCategorySelectModels: getSubCategorySelectModels,
            saveSubCategory: saveSubCategory,
            updateSubCategory: updateSubCategory,
            deleteSubCategory: deleteSubCategory
        };

        return service;
        function getSubCategories(pageSize, pageNumber, searchText) {
            var url = dataConstants.SUB_CATEGORY_URL + 'get-sub-categories?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getSubCategory(id) {
            var url = dataConstants.SUB_CATEGORY_URL + 'get-sub-category?id=' + id;
            return apiHttpService.GET(url);
        }
        function getSubCategorySelectModels() {
            var url = dataConstants.SUB_CATEGORY_URL + 'get-sub-category-select-models';
            return apiHttpService.GET(url);
        }

        function saveSubCategory(data) {
            var url = dataConstants.SUB_CATEGORY_URL + 'save-sub-category';
            return apiHttpService.POST(url, data);
        }

        function updateSubCategory(id, data) {
            var url = dataConstants.SUB_CATEGORY_URL + 'update-sub-category/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteSubCategory(id) {
            var url = dataConstants.SUB_CATEGORY_URL + 'delete-sub-category/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();