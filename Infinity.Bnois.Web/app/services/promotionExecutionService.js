(function () {
    'use strict';
    angular.module('app').service('promotionExecutionService', ['dataConstants', 'apiHttpService', promotionExecutionService]);

    function promotionExecutionService(dataConstants, apiHttpService) {
        var service = {
            getPromotionExecutions: getPromotionExecutions,
            getPromotionExecution: getPromotionExecution,
            savePromotionExecution: savePromotionExecution,
            updatePromotionExecution: updatePromotionExecution,
            deletePromotionExecution: deletePromotionExecution,
            getPromotionExecutedList: getPromotionExecutedList,
            updatePromotionExecutionList: updatePromotionExecutionList,

            getPromotionExecutionWithoutBoards: getPromotionExecutionWithoutBoards,
            getPromotionExecutionWithoutBoard: getPromotionExecutionWithoutBoard,
            savePromotionExecutionWithoutBoard: savePromotionExecutionWithoutBoard,
            updatePromotionExecutionWithoutBoard: updatePromotionExecutionWithoutBoard,
        };

        return service;
        function getPromotionExecutions(pageSize, pageNumber, searchText,type) {
            var url = dataConstants.PROMOTION_EXECUTION_URL + 'get-promotion-executions?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText+"&type="+type;
            return apiHttpService.GET(url);
        }

        function getPromotionExecution(promotionBoardId, promotionExecutionId) {
            var url = dataConstants.PROMOTION_EXECUTION_URL + 'get-promotion-execution?promotionBoardId=' + promotionBoardId + '&promotionExecutionId=' + promotionExecutionId;
            return apiHttpService.GET(url);
        }


        function getPromotionExecutedList(promotionBoardId,type) {
            var url = dataConstants.PROMOTION_EXECUTION_URL + 'get-promotion-nominations-by-promotion-board?promotionBoardId=' + promotionBoardId+'&type='+type;
            return apiHttpService.GET(url);
        }

       

        function savePromotionExecution(data) {
            var url = dataConstants.PROMOTION_EXECUTION_URL + 'save-promotion-Execution';
            return apiHttpService.POST(url, data);
        }

        function updatePromotionExecution(promotionExecutionId, data) {
            var url = dataConstants.PROMOTION_EXECUTION_URL + 'update-promotion-execution/' + promotionExecutionId;
            return apiHttpService.PUT(url, data);
        }
        function updatePromotionExecutionList(promotionBoardId,data) {
            var url = dataConstants.PROMOTION_EXECUTION_URL + 'update-promotion-executed-list/' + promotionBoardId;
            return apiHttpService.PUT(url, data);
        }
        

        function deletePromotionExecution(promotionExecutionId) {
            var url = dataConstants.PROMOTION_EXECUTION_URL + 'delete-promotion-execution/' + promotionExecutionId;
            return apiHttpService.DELETE(url);
        }

        function getPromotionExecutionWithoutBoards(pageSize, pageNumber, searchText) {
            var url = dataConstants.PROMOTION_EXECUTION_URL + 'get-promotion-execution-without-boards?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }
        function getPromotionExecutionWithoutBoard(promotionNominationId) {
            var url = dataConstants.PROMOTION_EXECUTION_URL + 'get-promotion-execution-without-board?promotionNominationId=' + promotionNominationId;
            return apiHttpService.GET(url);
        }

        function savePromotionExecutionWithoutBoard(data) {
            var url = dataConstants.PROMOTION_EXECUTION_URL + 'save-promotion-nomination-without-board';
            return apiHttpService.POST(url, data);
        }

        
        function updatePromotionExecutionWithoutBoard(promotionNominationId, data) {
            var url = dataConstants.PROMOTION_EXECUTION_URL + 'update-promotion-execution-without-board/' + promotionNominationId;
            return apiHttpService.PUT(url, data);
        }
    }
})();