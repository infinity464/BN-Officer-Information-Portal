(function () {

    'use strict';

    var controllerId = 'commissionTypeAddController';
    angular.module('app').controller(controllerId, commissionTypeAddController);
    commissionTypeAddController.$inject = ['$stateParams', 'commissionTypeService', 'notificationService', '$state'];
    function commissionTypeAddController($stateParams, commissionTypeService, notificationService, $state) {
        var vm = this;
        vm.commissionTypeId = 0;
        vm.title = 'ADD MODE';
        vm.commissionType = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.commissionTypeForm = {};
        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.commissionTypeId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            commissionTypeService.getCommissionType(vm.commissionTypeId).then(function (data) {
                vm.commissionType = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.commissionTypeId !== 0 && vm.commissionTypeId !== '') {
                updateCommissionType();
            } else {
                insertCommissionType();
            }
        }

        function insertCommissionType() {
            commissionTypeService.saveCommissionType(vm.commissionType).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateCommissionType() {
            commissionTypeService.updateCommissionType(vm.commissionTypeId, vm.commissionType).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('commission-types');
        }
    }
})();
