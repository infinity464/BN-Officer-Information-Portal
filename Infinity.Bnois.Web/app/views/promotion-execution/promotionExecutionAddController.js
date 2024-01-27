(function () {

    'use strict';

    var controllerId = 'promotionExecutionAddController';

    angular.module('app').controller(controllerId, promotionExecutionAddController);
    promotionExecutionAddController.$inject = ['$stateParams', 'promotionExecutionService', 'notificationService', '$state'];

    function promotionExecutionAddController($stateParams, promotionExecutionService, notificationService, $state) {
        var vm = this;
        vm.executionRemarks = [];
        vm.promotionExecutionId = 0;
        vm.promotionBoardId = 0;
        vm.title = 'ADD MODE';
        vm.promotionExecution = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.promotionExecutionForm = {};

        if ($stateParams.promotionExecutionId !== undefined && $stateParams.promotionExecutionId !== null) {
            vm.promotionExecutionId = $stateParams.promotionExecutionId;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        if ($stateParams.promotionBoardId !== undefined && $stateParams.promotionBoardId !== null) {
            vm.promotionBoardId = $stateParams.promotionBoardId;
        }
        Init();
        function Init() {
            promotionExecutionService.getPromotionExecution(vm.promotionBoardId,vm.promotionExecutionId).then(function (data) {
                vm.promotionExecution = data.result.promotionExecution;
                vm.executionRemarks = data.result.executionRemarks;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.promotionExecutionId !== 0 && vm.promotionExecutionId !== '') {
                updatePromotionExecution();
            } else {  
                insertPromotionExecution();
            }
        }

        function insertPromotionExecution() {
            vm.promotionExecution.promotionBoardId = vm.promotionBoardId;
            promotionExecutionService.savePromotionExecution(vm.promotionExecution).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updatePromotionExecution() {
            promotionExecutionService.updatePromotionExecution(vm.promotionExecutionId, vm.promotionExecution).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('promotion-executions', {type:vm.type, promotionBoardId: vm.promotionBoardId, pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }
    }
})();
