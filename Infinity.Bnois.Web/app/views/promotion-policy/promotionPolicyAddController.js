(function () {

    'use strict';

    var controllerId = 'promotionPolicyAddController';

    angular.module('app').controller(controllerId, promotionPolicyAddController);
    promotionPolicyAddController.$inject = ['$stateParams', 'promotionPolicyService', 'notificationService', '$state'];

    function promotionPolicyAddController($stateParams, promotionPolicyService, notificationService, $state) {
        var vm = this;
        vm.promotionPolicyId = 0;
        vm.title = 'ADD MODE';
        vm.promotionPolicy = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.promotionPolicyForm = {};
        vm.ranks = [];

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.promotionPolicyId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            promotionPolicyService.getPromotionPolicy(vm.promotionPolicyId).then(function (data) {
                vm.promotionPolicy = data.result.promotionPolicy;
                vm.ranks = data.result.ranks;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.promotionPolicyId !== 0 && vm.promotionPolicyId !== '') {
                updatePromotionPolicy();
            } else {
                insertPromotionPolicy();
            }
        }

        function insertPromotionPolicy() {
            promotionPolicyService.savePromotionPolicy(vm.promotionPolicy).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updatePromotionPolicy() {
            promotionPolicyService.updatePromotionPolicy(vm.promotionPolicyId, vm.promotionPolicy).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('promotion-policies');
        }
    }
})();
