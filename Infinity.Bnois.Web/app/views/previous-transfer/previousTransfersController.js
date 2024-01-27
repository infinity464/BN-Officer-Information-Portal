(function () {

    'use strict';
    var controllerId = 'previousTransfersController';
    angular.module('app').controller(controllerId, previousTransfersController);
    previousTransfersController.$inject = ['$stateParams', '$state', 'previousTransferService', 'notificationService'];

    function previousTransfersController($stateParams, $state, previousTransferService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.employeeId = 0;
        vm.previousTransferId = 0;
        vm.previousTransfers = [];
        vm.title = 'Previous Transfer';
        vm.addPreviousTransfer = addPreviousTransfer;
        vm.updatePreviousTransfer = updatePreviousTransfer;
        vm.deletePreviousTransfer = deletePreviousTransfer;
        vm.back = back;

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        init();
        function init() {
            previousTransferService.getPreviousTransfers(vm.employeeId).then(function (data) {
                vm.previousTransfers = data.result;  
                    vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        
        function addPreviousTransfer() {
            $state.go('employee-tabs.previous-transfer-create', { id: vm.employeeId, previousTransferId: vm.previousTransferId });
        }
        
        function updatePreviousTransfer(previousTransfer) {
            $state.go('employee-tabs.previous-transfer-modify', { id: vm.employeeId, previousTransferId: previousTransfer.previousTransferId });
        }


        function deletePreviousTransfer(previousTransfer) {
            previousTransferService.deletePreviousTransfer(previousTransfer.previousTransferId).then(function (data) {

                previousTransferService.getPreviousTransfers(vm.employeeId).then(function(data) {
                    vm.previousTransfers = data.result;
                });
            });
        }


        function back() {
            $state.go('employee-tabs.employee-previous-experiences');
        }

    }

})();
