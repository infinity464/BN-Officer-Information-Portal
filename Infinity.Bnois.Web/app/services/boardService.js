(function () {
    'use strict';
    angular.module('app').service('boardService', ['dataConstants', 'apiHttpService', boardService]);

    function boardService(dataConstants, apiHttpService) {
        var service = {
            getBoards: getBoards,
            getBoard: getBoard,
            saveBoard: saveBoard,
            deleteBoard: deleteBoard,
            updateBoard: updateBoard
        };

        return service;
        function getBoards(pageSize, pageNumber, searchText) {
            var url = dataConstants.BOARD_URL + 'get-boards?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getBoard(id) {
            var url = dataConstants.BOARD_URL + 'get-board?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveBoard(data) {
            var url = dataConstants.BOARD_URL + 'save-board';
            return apiHttpService.POST(url, data);
        }

        function deleteBoard(id) {
            var url = dataConstants.BOARD_URL + 'delete-board/' + id;
            return apiHttpService.DELETE(url);
        }
        function updateBoard(id, data) {
            var url = dataConstants.BOARD_URL + 'update-board/' + id;
            return apiHttpService.PUT(url, data);
        }
     

    }
})();