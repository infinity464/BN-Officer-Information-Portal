(function () {

    'use strict';
    var controllerId = 'IndividualEmployeeLeaveController';
    angular.module('app').controller(controllerId, IndividualEmployeeLeaveController);
    IndividualEmployeeLeaveController.$inject = ['$state', '$stateParams', 'employeeLeaveService', 'notificationService', '$location'];

    function IndividualEmployeeLeaveController($state, $stateParams, employeeLeaveService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.leaveDetails = [];
        vm.leaveStatus = [];
        vm.pno = '';
        vm.empLeaveId = 0;
        vm.leaveTypes = [];
        vm.searchText = "";
        vm.msg = '';
        vm.close = close;
        vm.approvedEmployeeLeave = approvedEmployeeLeave;
        vm.CancelEmployeeLeaveByNs = CancelEmployeeLeaveByNs;
        if ($stateParams.empLeaveId !== undefined) {
            vm.empLeaveId = $stateParams.empLeaveId;
        }

        init();
        function init() {

            employeeLeaveService.GetEmployeeLeaveStatusDatabyIndividualStatusId(vm.empLeaveId).then(function (data) {
                vm.leaveDetails = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
            employeeLeaveService.GetEmployeeLeaveStatusData(vm.empLeaveId).then(function (data) {
                vm.leaveStatus = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function close() {
            $state.go('ns-dashboard');
        }
        function approvedEmployeeLeave() {
            employeeLeaveService.ApproveEmployeeLeaveByNs(vm.empLeaveId, vm.remark).then(function (data) {
                vm.msg = data.result.msg;
                notificationService.displaySuccess(vm.msg);
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function CancelEmployeeLeaveByNs() {
            employeeLeaveService.CancelEmployeeLeaveByNs(vm.empLeaveId, vm.remark).then(function (data) {
                vm.msg = data.result.msg;
                notificationService.displaySuccess(vm.msg);
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        
    }

})();
