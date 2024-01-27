(function () {
    'use strict';
    angular.module('app').service('visitSubCategoryService', ['dataConstants', 'apiHttpService', visitSubCategoryService]);

    function visitSubCategoryService(dataConstants, apiHttpService) {
        var service = {
            getVisitSubCategories: getVisitSubCategories,
            getVisitSubCategory: getVisitSubCategory,
            saveVisitSubCategory: saveVisitSubCategory,
            updateVisitSubCategory: updateVisitSubCategory,
            deleteVisitSubCategory: deleteVisitSubCategory,
            getVisitSubCategorySelectModelsByVisitCategory: getVisitSubCategorySelectModelsByVisitCategory
        };

        return service;
        function getVisitSubCategories(pageSize, pageNumber, searchText) {
            var url = dataConstants.VISIT_SUB_CATEGORIES_URL + 'get-visit-sub-categories?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getVisitSubCategory(id) {
            var url = dataConstants.VISIT_SUB_CATEGORIES_URL + 'get-visit-sub-category?id=' + id;
            return apiHttpService.GET(url);
        }
        function getVisitSubCategorySelectModelsByVisitCategory(visitCategoryId) {
            var url = dataConstants.VISIT_SUB_CATEGORIES_URL + 'get-visit-sub-categories-by-visit-category?visitCategoryId=' + visitCategoryId;
            return apiHttpService.GET(url);
        }


        function saveVisitSubCategory(data) {
            var url = dataConstants.VISIT_SUB_CATEGORIES_URL + 'save-visit-sub-category';
            return apiHttpService.POST(url, data);
        }

        function updateVisitSubCategory(id, data) {
            var url = dataConstants.VISIT_SUB_CATEGORIES_URL + 'update-visit-sub-category/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteVisitSubCategory(id) {
            var url = dataConstants.VISIT_SUB_CATEGORIES_URL + 'delete-visit-sub-category/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();