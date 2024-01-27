(function () {

    'use strict';

    var controllerId = 'batchAddController';

    angular.module('app').controller(controllerId, batchAddController);
    batchAddController.$inject = ['$stateParams', 'batchService', 'notificationService', '$state'];

    function batchAddController($stateParams, batchService, notificationService, $state) {
        var vm = this;
        vm.batchId = 0;
        vm.title = 'ADD MODE';
        vm.batch = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.batchForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.batchId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            batchService.getBatch(vm.batchId).then(function (data) {
                vm.batch = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.batchId !== 0 && vm.batchId !== '') {
                updateBatch();
            } else {  
                insertBatch();
            }
        }

        function insertBatch() {
            batchService.saveBatch(vm.batch).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateBatch() {
            batchService.updateBatch(vm.batchId, vm.batch).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('batches');
        }
    }
})();
