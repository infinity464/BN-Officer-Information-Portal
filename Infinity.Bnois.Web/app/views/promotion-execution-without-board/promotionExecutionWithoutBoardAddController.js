(function () {

    'use strict';

    var controllerId = 'promotionExecutionWithoutBoardAddController';

    angular.module('app').controller(controllerId, promotionExecutionWithoutBoardAddController);
    promotionExecutionWithoutBoardAddController.$inject = ['$stateParams', 'promotionExecutionService', 'backLogService','notificationService', '$state'];

    function promotionExecutionWithoutBoardAddController($stateParams, promotionExecutionService, backLogService, notificationService, $state) {
        var vm = this;
        vm.promotionNominationId = 0;
        vm.title = 'ADD MODE';
        vm.promotionNomination = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.promotionExecutionWithoutBoardForm = {};
        vm.transfers = [];
        vm.isBackLogChecked = isBackLogChecked;

        if ($stateParams.promotionNominationId !== undefined && $stateParams.promotionNominationId !== null) {
            vm.promotionNominationId = $stateParams.promotionNominationId;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            promotionExecutionService.getPromotionExecutionWithoutBoard(vm.promotionNominationId).then(function (data) {
                vm.promotionNomination = data.result.promotionNomination;
                if (vm.promotionNomination.promotionNominationId > 0) {
                    isBackLogChecked(vm.promotionNomination.isBackLog);
                }
                if (vm.promotionNomination.effectiveDate!=null) {
                    vm.promotionNomination.effectiveDate = new Date(vm.promotionNomination.effectiveDate);
                }
                vm.ranks = data.result.ranks;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.promotionNominationId !== 0 && vm.promotionNominationId !== '') {
                updatePromotionExecutionWithoutBoard();
            } else {  
                insertPromotionExecutionWithoutBoard();
            }
        }

        function insertPromotionExecutionWithoutBoard() {
            promotionExecutionService.savePromotionExecutionWithoutBoard(vm.promotionNomination).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updatePromotionExecutionWithoutBoard() {
            promotionExecutionService.updatePromotionExecutionWithoutBoard(vm.promotionNominationId, vm.promotionNomination).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('promotion-execution-without-boards', {pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function isBackLogChecked(isBackLog) {
            if (isBackLog) {
                if (vm.promotionNomination.employee.employeeId > 0) {
                    backLogService.getBackLogSelectModels(vm.promotionNomination.employee.employeeId).then(function (data) {
                        vm.transfers = data.result.transfers;
                    });
                }
            }
        }
    }
})();
