(function () {

    'use strict';
    var controllerId = 'previousLeavesController';
    angular.module('app').controller(controllerId, previousLeavesController);
    previousLeavesController.$inject = ['$stateParams', '$state', 'previousLeaveService', 'notificationService'];

    function previousLeavesController($stateParams, $state, previousLeaveService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.employeeId = 0;
        vm.previousLeaveId = 0;
        vm.previousLeaves = [];
        vm.title = 'Previous Leave';
        vm.addPreviousLeave = addPreviousLeave;
        vm.updatePreviousLeave = updatePreviousLeave;
        vm.deletePreviousLeave = deletePreviousLeave;
        vm.back = back;

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        init();
        function init() {
            previousLeaveService.getPreviousLeaves(vm.employeeId).then(function (data) {
                vm.previousLeaves = data.result;  
                    vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        
        function addPreviousLeave() {
            $state.go('employee-tabs.previous-leave-create', { id: vm.employeeId, previousLeaveId: vm.previousLeaveId });
        }
        
        function updatePreviousLeave(previousLeave) {
            $state.go('employee-tabs.previous-leave-modify', { id: vm.employeeId, previousLeaveId: previousLeave.previousLeaveId });
        }


        function deletePreviousLeave(previousLeave) {
            previousLeaveService.deletePreviousLeave(previousLeave.previousLeaveId).then(function (data) {

                previousLeaveService.getPreviousLeaves(vm.employeeId).then(function(data) {
                    vm.previousLeaves = data.result;
                });
            });
        }


        function back() {
            $state.go('employee-tabs.employee-previous-experiences');
        }

    }

})();
