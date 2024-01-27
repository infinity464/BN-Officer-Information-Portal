(function () {
    'use strict';
    angular.module('app').service('rankService', ['dataConstants', 'apiHttpService', rankService]);

    function rankService(dataConstants, apiHttpService) {
        var service = {
            getRanks: getRanks,
            getRank: getRank,
            saveRank: saveRank,
            updateRank: updateRank,
            deleteRank: deleteRank,
            getRankByRankCategory: getRankByRankCategory,
            getRankSelectModel: getRankSelectModel
        };

        return service;
        function getRanks(pageSize, pageNumber,searchText) {
            var url = dataConstants.RANK_URL + 'get-ranks?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getRank(id) {
            var url = dataConstants.RANK_URL + 'get-rank?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveRank(data) {
            var url = dataConstants.RANK_URL + 'save-rank';
            return apiHttpService.POST(url, data);
        }

        function updateRank(id, data) {
            var url = dataConstants.RANK_URL + 'update-rank/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteRank(id) {
            var url = dataConstants.RANK_URL + 'delete-rank/' + id;
            return apiHttpService.DELETE(url);
        }

        function getRankByRankCategory(rankCategoryId) {
            var url = dataConstants.RANK_URL + 'get-ranks-by-rank-category?rankCategoryId=' + rankCategoryId;
            return apiHttpService.GET(url);
        }
        function getRankSelectModel() {
            var url = dataConstants.RANK_URL + 'get-rank-select-models';
            return apiHttpService.GET(url);
        }
        
    }
})();