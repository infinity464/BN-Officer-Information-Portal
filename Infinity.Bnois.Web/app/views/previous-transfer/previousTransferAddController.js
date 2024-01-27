(function () {

    'use strict';
    var controllerId = 'previousTransferAddController';
    angular.module('app').controller(controllerId, previousTransferAddController);
    previousTransferAddController.$inject = ['$stateParams', '$state', 'previousTransferService', 'notificationService'];

    function previousTransferAddController($stateParams, $state, previousTransferService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.previousTransferId = 0;
        vm.previousTransfer = {};
        vm.title = 'ADD MODE';
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.previousTransferForm = {};
        vm.ranks = [];
       

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
        }
        if ($stateParams.previousTransferId > 0) {
            vm.previousTransferId = $stateParams.previousTransferId;
            vm.title = 'UPDATE MODE';
            vm.saveButtonText = 'Update';
            
        }
        init();
        function init() {
            previousTransferService.getPreviousTransfer(vm.previousTransferId).then(function (data) {
                vm.previousTransfer = data.result.previousTransfer;
                vm.ranks = data.result.ranks;
                if (vm.previousTransfer.fromDate != null) {
                    vm.previousTransfer.fromDate = new Date(vm.previousTransfer.fromDate);

                }
                    if (vm.previousTransfer.toDate != null) {
                        vm.previousTransfer.toDate = new Date(vm.previousTransfer.toDate);

                    }
                
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        
        function save() {
            if (vm.previousTransferId > 0 && vm.previousTransferId !== '') {
                updatePreviousTransfer();
            } else {
                insertPreviousTransfer();
            }
        }
        function insertPreviousTransfer() {
            previousTransferService.savePreviousTransfer(vm.employeeId, vm.previousTransfer).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updatePreviousTransfer() {
            previousTransferService.updatePreviousTransfer(vm.previousTransferId, vm.previousTransfer).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function close() {
            $state.go('employee-tabs.previous-transfers');
        }
    }

})();
