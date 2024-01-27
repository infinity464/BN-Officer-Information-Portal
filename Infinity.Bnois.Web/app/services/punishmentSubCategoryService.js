(function () {
    'use strict';
    angular.module('app').service('punishmentSubCategoryService', ['dataConstants', 'apiHttpService', punishmentSubCategoryService]);

    function punishmentSubCategoryService(dataConstants, apiHttpService) {
        var service = {
            getPunishmentSubCategories: getPunishmentSubCategories,
            getPunishmentSubCategory: getPunishmentSubCategory,
            savePunishmentSubCategory: savePunishmentSubCategory,
            updatePunishmentSubCategory: updatePunishmentSubCategory,
            deletePunishmentSubCategory: deletePunishmentSubCategory,
            getPunishmentSubCategorySelectModelsByPunishmentCategory: getPunishmentSubCategorySelectModelsByPunishmentCategory
        };

        return service;
        function getPunishmentSubCategories(pageSize, pageNumber, searchText) {
            var url = dataConstants.PUNISHMENT_SUB_CATEGORY_URL + 'get-punishment-sub-categories?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getPunishmentSubCategory(id) {
            var url = dataConstants.PUNISHMENT_SUB_CATEGORY_URL + 'get-punishment-sub-category?id=' + id;
            return apiHttpService.GET(url);
        }
        function getPunishmentSubCategorySelectModelsByPunishmentCategory(punishmentCategoryId) {
            var url = dataConstants.PUNISHMENT_SUB_CATEGORY_URL + 'get-punishment-sub-categories-by-punishment-category?punishmentCategoryId=' + punishmentCategoryId;
            return apiHttpService.GET(url);
        }


        function savePunishmentSubCategory(data) {
            var url = dataConstants.PUNISHMENT_SUB_CATEGORY_URL + 'save-punishment-sub-category';
            return apiHttpService.POST(url, data);
        }

        function updatePunishmentSubCategory(id, data) {
            var url = dataConstants.PUNISHMENT_SUB_CATEGORY_URL + 'update-punishment-sub-category/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deletePunishmentSubCategory(id) {
            var url = dataConstants.PUNISHMENT_SUB_CATEGORY_URL + 'delete-punishment-sub-category/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();