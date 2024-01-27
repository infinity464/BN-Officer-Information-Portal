(function () {

    'use strict';

    var controllerId = 'boardsController';
    angular.module('app').controller(controllerId, boardsController);
    boardsController.$inject = ['$state', 'boardService', 'notificationService', '$location'];

    function boardsController($state, boardService, notificationService, location) {
        /* jshint validthis:true */
        var vm = this;
        vm.boards = [];
        vm.addBoard = addBoard;
        vm.updateBoard = updateBoard;
        vm.deleteBoard = deleteBoard;
        vm.pageChanged = pageChanged;
        vm.searchText = '';
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;

        if (location.search().ps !== undefined && location.search().ps != null && location.search().ps != '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn != null && location.search().pn != '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q != null && location.search().q != '') {
            vm.searchText = location.search().q;
        }
        init();
        function init() {
            boardService.getBoards(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.boards = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addBoard() {
            $state.go('board-create');
        }

        function updateBoard(board) {
            $state.go('board-modify', { id: board.boardId });
        }

        function deleteBoard(board) {
            boardService.deleteBoard(board.boardId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('boards', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
