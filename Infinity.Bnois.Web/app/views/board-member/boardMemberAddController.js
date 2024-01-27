(function () {

    'use strict';

    var controllerId = 'boardMemberAddController';

    angular.module('app').controller(controllerId, boardMemberAddController);
    boardMemberAddController.$inject = ['$stateParams', 'boardMemberService', 'notificationService', '$state'];

    function boardMemberAddController($stateParams, boardMemberService, notificationService, $state) {
        var vm = this;
        vm.memberRoles = [];
        vm.boardMemberId = 0;
        vm.promotionBoardId = 0;
        vm.action = 'ADD MODE';
        vm.boardMember = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.boardMemberForm = {};

        vm.type = 0;
        vm.title = '';
        vm.promotionHide = true;



        if ($stateParams.type !== undefined && $stateParams.type !== null) {
            vm.type = $stateParams.type;
            if (vm.type == 1) {
                vm.typeName = "Promotion";
                vm.promotionHide = false;
            }
            else {

                vm.typeName = "SASB";
                vm.promotionHide = true;
            }
        }


        if ($stateParams.boardMemberId !== undefined && $stateParams.boardMemberId !== null) {
            vm.boardMemberId = $stateParams.boardMemberId;
            vm.saveButtonText = 'Update';
            vm.action = 'UPDATE MODE';
        }

        if ($stateParams.title !== undefined && $stateParams.title !== null) {
            vm.title = $stateParams.title;

        }


        if ($stateParams.promotionBoardId !== undefined && $stateParams.promotionBoardId !== null) {
            vm.promotionBoardId = $stateParams.promotionBoardId;
        }
        Init();
        function Init() {
            boardMemberService.getBoardMember(vm.promotionBoardId,vm.boardMemberId).then(function (data) {
                vm.boardMember = data.result.boardMember;
                vm.memberRoles = data.result.memberRoles;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.boardMemberId !== 0 && vm.boardMemberId !== '') {
                updateBoardMember();
            } else {  
                insertBoardMember();
            }
        }

        function insertBoardMember() {
            vm.boardMember.promotionBoardId = vm.promotionBoardId;
            boardMemberService.saveBoardMember(vm.boardMember).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateBoardMember() {
            boardMemberService.updateBoardMember(vm.boardMemberId, vm.boardMember).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('board-members', { type: vm.type,title:vm.title,promotionBoardId: vm.promotionBoardId, pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }
    }
})();
