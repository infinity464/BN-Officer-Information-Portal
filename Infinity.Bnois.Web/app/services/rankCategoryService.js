(function () {
    'use strict';
    angular.module('app').service('rankCategoryService', ['dataConstants', 'apiHttpService', rankCategoryService]);

    function rankCategoryService(dataConstants, apiHttpService) {
        var service = {
            getRankCategories: getRankCategories,
            getRankCategory: getRankCategory,
            saveRankCategory: saveRankCategory,
            updateRankCategory: updateRankCategory,
            deleteRankCategory: deleteRankCategory
        };

        return service;
        function getRankCategories(pageSize, pageNumber,searchText) {
            var url = dataConstants.RANK_CATEGORY_URL + 'get-rank-categories?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getRankCategory(id) {
            var url = dataConstants.RANK_CATEGORY_URL + 'get-rank-category?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveRankCategory(data) {
            var url = dataConstants.RANK_CATEGORY_URL + 'save-rank-category';
            return apiHttpService.POST(url, data);
        }

        function updateRankCategory(id, data) {
            var url = dataConstants.RANK_CATEGORY_URL + 'update-rank-category/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteRankCategory(id) {
            var url = dataConstants.RANK_CATEGORY_URL + 'delete-rank-category/' + id;
            return apiHttpService.DELETE(url);
        }


    }
})();