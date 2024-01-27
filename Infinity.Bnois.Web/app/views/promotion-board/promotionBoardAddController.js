(function () {

    'use strict';

    var controllerId = 'promotionBoardAddController';

    angular.module('app').controller(controllerId, promotionBoardAddController);
    promotionBoardAddController.$inject = ['$stateParams', 'promotionBoardService', 'notificationService', '$state'];

    function promotionBoardAddController($stateParams, promotionBoardService, notificationService, $state) {
        var vm = this;
        vm.promotionBoardId = 0;
        vm.isBoard = true;
        vm.title = 'ADD MODE';
        vm.promotionBoard = {};
        vm.fromConfirmRanks = [];
        vm.toActingRanks = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;

        vm.promotionBoardForm = {};

        vm.type = 0;
        vm.promotionHide = false;

        vm.ltCdrLevels = [{ text: 'Cdr & Above', value: 1 }, { text: 'Lt Cdr & Below', value: 2 }];



        if ($stateParams.type !== undefined && $stateParams.type !== null) {
            vm.type = $stateParams.type;
            if (vm.type == 1) {
                vm.typeName = "Promotion";
                vm.promotionHide = true;
            }
            else {
                vm.typeName = "SASB";
                vm.promotionHide = false;
            }
        }



        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.promotionBoardId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            promotionBoardService.getPromotionBoard(vm.promotionBoardId).then(function (data) {
                vm.promotionBoard = data.result.promotionBoard;
                vm.promotionBoard.type = vm.type;
                if (vm.promotionBoard.promotionBoardId == 0) {
                    vm.promotionBoard.isBoard = true;
                }
                if (vm.promotionBoard.formationDate != null) {
                    vm.promotionBoard.formationDate = new Date(vm.promotionBoard.formationDate);
                }
                vm.fromConfirmRanks = data.result.fromConfirmRanks;
                vm.toActingRanks = data.result.toActingRanks;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.promotionBoardId !== 0 && vm.promotionBoardId !== '') {
                updatePromotionBoard();
            } else {
                insertPromotionBoard();
            }
        }

        function insertPromotionBoard() {
            promotionBoardService.savePromotionBoard(vm.promotionBoard).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updatePromotionBoard() {
            promotionBoardService.updatePromotionBoard(vm.promotionBoardId, vm.promotionBoard).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('promotion-boards', { type: vm.type });
        }
    }
})();
