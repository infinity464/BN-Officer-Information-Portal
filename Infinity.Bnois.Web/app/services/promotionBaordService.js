(function () {
    'use strict';
    angular.module('app').service('promotionBoardService', ['dataConstants', 'apiHttpService', promotionBoardService]);

    function promotionBoardService(dataConstants, apiHttpService) {
        var service = {
            getPromotionBoards: getPromotionBoards,
            getPromotionBoard: getPromotionBoard,
            savePromotionBoard: savePromotionBoard,
            updatePromotionBoard: updatePromotionBoard,
            deletePromotionBoard: deletePromotionBoard,
            calculateTrace: calculateTrace
        };

        return service;
        function getPromotionBoards(pageSize, pageNumber,searchText,type) {
            var url = dataConstants.PROMOTION_BOARD_URL + 'get-promotion-boards?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText+"&type="+type;
            return apiHttpService.GET(url);
        }

        function getPromotionBoard(id) {
            var url = dataConstants.PROMOTION_BOARD_URL + 'get-promotion-board?id=' + id;
            return apiHttpService.GET(url);
        }

        function calculateTrace(promotionBoardId) {
            var url = dataConstants.PROMOTION_BOARD_URL + 'calculate-trace?promotionBoardId=' + promotionBoardId;
            return apiHttpService.GET(url);
        }


        function savePromotionBoard(data) {
            var url = dataConstants.PROMOTION_BOARD_URL + 'save-promotion-board';
            return apiHttpService.POST(url, data);
        }

        function updatePromotionBoard(id, data) {
            var url = dataConstants.PROMOTION_BOARD_URL + 'update-promotion-board/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deletePromotionBoard(id) {
            var url = dataConstants.PROMOTION_BOARD_URL + 'delete-promotion-board/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();