(function () {
    'use strict';
    angular.module('app').service('boardMemberService', ['dataConstants', 'apiHttpService', boardMemberService]);

    function boardMemberService(dataConstants, apiHttpService) {
        var service = {
               getBoardMembers: getBoardMembers,
               getBoardMember: getBoardMember,
            saveBoardMember: saveBoardMember,
            updateBoardMember: updateBoardMember,
            deleteBoardMember: deleteBoardMember
        };

        return service;
        function getBoardMembers(promotionBoardId, pageSize, pageNumber, searchText,type) {
            var url = dataConstants.BOARD_MEMBER_URL + 'get-board-members?promotionBoardId=' + promotionBoardId + '&ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText+"&type="+type;
            return apiHttpService.GET(url);
        }

        function getBoardMember(promotionBoardId, boardMemberId) {
            var url = dataConstants.BOARD_MEMBER_URL + 'get-board-member?promotionBoardId=' + promotionBoardId + '&boardMemberId=' + boardMemberId;
            return apiHttpService.GET(url);
        }

        function saveBoardMember(data) {
            var url = dataConstants.BOARD_MEMBER_URL + 'save-board-member';
            return apiHttpService.POST(url, data);
        }

        function updateBoardMember(boardMemberId, data) {
            var url = dataConstants.BOARD_MEMBER_URL + 'update-board-member/' + boardMemberId;
            return apiHttpService.PUT(url, data);
        }

        function deleteBoardMember(boardMemberId) {
            var url = dataConstants.BOARD_MEMBER_URL + 'delete-board-member/' + boardMemberId;
            return apiHttpService.DELETE(url);
        }

    }
})();