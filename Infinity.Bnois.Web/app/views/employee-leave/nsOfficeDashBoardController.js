(function () {

    'use strict';
    var controllerId = 'nsOfficeDashBoardController';
    angular.module('app').controller(controllerId, nsOfficeDashBoardController);
    nsOfficeDashBoardController.$inject = ['$state', 'employeeLeaveService', 'notificationService', '$location'];

    function nsOfficeDashBoardController($state, employeeLeaveService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.empoloyeeLeaves = [];
        vm.pno = '';
        vm.leaveTypeId = 0;
        vm.leaveTypes = [];
        vm.searchText = "";
        vm.IndividualEmployeeLeave = IndividualEmployeeLeave;
        //vm.addEmployeeLeave = addEmployeeLeave;
        //vm.updateEmployeeLeave = updateEmployeeLeave;
        //vm.deleteEmployeeLeave = deleteEmployeeLeave;
        //vm.pageChanged = pageChanged;
        //vm.leaveApprove = leaveApprove;
        //vm.leaveDeny = leaveDeny;


        vm.onSearch = onSearch;

        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;

        init();
        function init() {

            employeeLeaveService.GetEmployeeLeavesForNsUser(vm.pno, vm.leaveTypeId).then(function (data) {
                vm.empoloyeeLeaves = data.result.employeeLeaveForNS;
                vm.leaveTypes = data.result.leaveTypes;
                vm.total = vm.empoloyeeLeaves.total;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        //function addEmployeeLeave() {

        //    $state.go('employeeLeave-create');
        //}

        //function updateEmployeeLeave(employeeLeave) {
        //    $state.go('employeeLeave-modify', { id: employeeLeave.empLeaveId });
        //}
        //function leaveApprove(EL) {
        //    employeeLeaveService.employeeLeaveApprove(EL.empLeaveId, EL.empLeaveDetailId).then(function (data) {
        //        notificationService.displaySuccess(data.result);
        //        init();
        //    },
        //        function (errorMessage) {
        //            notificationService.displayError(errorMessage.message);
        //        });
        //}
        //function leaveDeny() {

        //}
        //function deleteEmployeeLeave(empoloyeeLeaves) {
        //    employeeService.deleteEmployee(employee.employeeId).then(function (data) {
        //        $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        //    });
        //}

        function IndividualEmployeeLeave(empLeaveId) {
            $state.go('employee-leave-details', { empLeaveId: empLeaveId });
        }

        function onSearch() {
            employeeLeaveService.GetEmployeeLeavesForNsUser(vm.searchText, vm.leaveTypeId).then(function (data) {
                vm.empoloyeeLeaves = data.result.employeeLeaveForNS;
                vm.leaveTypes = data.result.leaveTypes;
                vm.total = vm.empoloyeeLeaves.total;
                vm.leaveTypeId = 0;
                vm.searchText = "";
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
    }

})();
