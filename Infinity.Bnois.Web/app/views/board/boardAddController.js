(function () {

    'use strict';

    var controllerId = 'boardAddController';

    angular.module('app').controller(controllerId, boardAddController);
    boardAddController.$inject = ['$stateParams', 'boardService', 'notificationService', '$state'];

    function boardAddController($stateParams, boardService, notificationService, $state) {
        var vm = this;
        vm.boardId = 0;
        vm.title = 'ADD MODE';
        vm.board = {};
        vm.boardTypes = [];

        //vm.boardTypes = [{ text: "Board", value: 1 }, {text:"University",value:2}]
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.boardForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.boardId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            boardService.getBoard(vm.boardId).then(function (data) {
                vm.board = data.result.board;
                vm.boardTypes = data.result.boardTypes;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {

            if (vm.boardId !== 0 && vm.boardId !== '') {
                updateBoard();
            } else {
                insertBoard();
            }
        }

        function insertBoard() {
            boardService.saveBoard(vm.board).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateBoard() {
            boardService.updateBoard(vm.boardId, vm.board).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
      

        function close() {
            $state.go('boards');
        }

    }
})();
