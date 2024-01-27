(function () {

    'use strict';

    var controllerId = 'executionRemarkAddController';

    angular.module('app').controller(controllerId, executionRemarkAddController);
    executionRemarkAddController.$inject = ['$stateParams', 'executionRemarkService', 'notificationService', '$state'];

    function executionRemarkAddController($stateParams, executionRemarkService, notificationService, $state) {
        var vm = this;
        vm.executionRemarkId = 0;
        vm.title = 'ADD MODE';
        vm.executionRemark = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.executionRemarkIdForm = {};

        vm.type = 0;
      


        if ($stateParams.type !== undefined && $stateParams.type !== null) {
            vm.type = $stateParams.type;
           
        }

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.executionRemarkId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            executionRemarkService.getExecutionRemark(vm.executionRemarkId).then(function (data) {
                vm.executionRemark = data.result;
                    vm.executionRemark.type = vm.type;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.executionRemarkId !== 0 && vm.executionRemarkId !== '') {
                updateExecutionRemark();
            } else {  
                insertExecutionRemark();
            }
        }

        function insertExecutionRemark() {
            executionRemarkService.saveExecutionRemark(vm.executionRemark).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateExecutionRemark() {
            executionRemarkService.updateExecutionRemark(vm.executionRemarkId, vm.executionRemark).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('execution-remarks', {type:vm.type});
        }
    }
})();
