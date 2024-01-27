(function () {

    'use strict';
    var controllerId = 'previousLeaveAddController';
    angular.module('app').controller(controllerId, previousLeaveAddController);
    previousLeaveAddController.$inject = ['$stateParams', '$state', 'previousLeaveService', 'notificationService'];

    function previousLeaveAddController($stateParams, $state, previousLeaveService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.previousLeaveId = 0;
        vm.previousLeave = {};
        vm.title = 'ADD MODE';
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.previousLeaveForm = {};
        vm.leaveTypes = [];


        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
        }
        if ($stateParams.previousLeaveId > 0) {
            vm.previousLeaveId = $stateParams.previousLeaveId;
            vm.title = 'UPDATE MODE';
            vm.saveButtonText = 'Update';
            
        }
        init();
        function init() {
            previousLeaveService.getPreviousLeave(vm.previousLeaveId).then(function (data) {
                vm.previousLeave = data.result.previousLeave;
                if (vm.previousLeave.fromDate != null) {
                    vm.previousLeave.fromDate = new Date(vm.previousLeave.fromDate);

                }
                    if (vm.previousLeave.toDate != null) {
                        vm.previousLeave.toDate = new Date(vm.previousLeave.toDate);

                    }
                vm.leaveTypes = data.result.leaveTypes;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        
        function save() {
            if (vm.previousLeaveId > 0 && vm.previousLeaveId !== '') {
                updatePreviousLeave();
            } else {
                insertPreviousLeave();
            }
        }
        function insertPreviousLeave() {
            previousLeaveService.savePreviousLeave(vm.employeeId, vm.previousLeave).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updatePreviousLeave() {
            previousLeaveService.updatePreviousLeave(vm.previousLeaveId, vm.previousLeave).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function close() {
            $state.go('employee-tabs.previous-leaves');
        }
    }

})();
