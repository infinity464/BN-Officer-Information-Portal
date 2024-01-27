(function () {

    'use strict';

    var controllerId = 'subBranchAddController';

    angular.module('app').controller(controllerId, subBranchAddController);
    subBranchAddController.$inject = ['$stateParams', 'subBranchService', 'notificationService', '$state'];

    function subBranchAddController($stateParams, subBranchService, notificationService, $state) {
        var vm = this;
        vm.subBranchId = 0;
        vm.title = 'ADD MODE';
        vm.subBranch = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.subBranchIdForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.subBranchId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            subBranchService.getSubBranch(vm.subBranchId).then(function (data) {
                vm.subBranch = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.subBranchId !== 0 && vm.subBranchId !== '') {
                updateSubBranch();
            } else {
                insertSubBranch();
            }
        }

        function insertSubBranch() {
            subBranchService.saveSubBranch(vm.subBranch).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateSubBranch() {
            subBranchService.updateSubBranch(vm.subBranchId, vm.subBranch).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('sub-branches');
        }
    }
})();
