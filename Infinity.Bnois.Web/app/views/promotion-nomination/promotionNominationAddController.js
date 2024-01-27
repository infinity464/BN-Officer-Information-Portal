(function () {

    'use strict';

    var controllerId = 'promotionNominationAddController';

    angular.module('app').controller(controllerId, promotionNominationAddController);
    promotionNominationAddController.$inject = ['$stateParams', 'promotionNominationService', 'notificationService', '$state'];

    function promotionNominationAddController($stateParams, promotionNominationService, notificationService, $state) {
        var vm = this;
        vm.executionRemarks = [];
        vm.promotionNominationId = 0;
        vm.promotionBoardId = 0;
        vm.action = 'ADD MODE';
        vm.promotionNomination = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.promotionNominationForm = {};

        vm.type = 0;
        vm.promotionHide = false;

        vm.title = '';

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

        if ($stateParams.title !== undefined && $stateParams.title !== null) {
            vm.title = $stateParams.title;

        }

        if ($stateParams.promotionNominationId !== undefined && $stateParams.promotionNominationId !== null) {
            vm.promotionNominationId = $stateParams.promotionNominationId;
            vm.saveButtonText = 'Update';
            vm.action = 'UPDATE MODE';
        }

        if ($stateParams.promotionBoardId !== undefined && $stateParams.promotionBoardId !== null) {
            vm.promotionBoardId = $stateParams.promotionBoardId;
        }
        Init();
        function Init() {
            promotionNominationService.getPromotionNomination(vm.promotionBoardId,vm.promotionNominationId).then(function (data) {
                vm.promotionNomination = data.result.promotionNomination;
                    vm.promotionNomination.type = vm.type;
                vm.executionRemarks = data.result.executionRemarks;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.promotionNominationId !== 0 && vm.promotionNominationId !== '') {
                updatePromotionNomination();
            } else {  
                insertPromotionNomination();
            }
        }

        function insertPromotionNomination() {
            vm.promotionNomination.promotionBoardId = vm.promotionBoardId;
            promotionNominationService.savePromotionNomination(vm.promotionNomination).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updatePromotionNomination() {
            promotionNominationService.updatePromotionNomination(vm.promotionNominationId, vm.promotionNomination).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('promotion-nominations', { type: vm.type, title: vm.title, promotionBoardId: vm.promotionBoardId, pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }
    }
})();
