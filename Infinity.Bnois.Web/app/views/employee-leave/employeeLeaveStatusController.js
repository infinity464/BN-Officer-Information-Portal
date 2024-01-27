(function () {

    'use strict';
    var controllerId = 'employeeLeaveStatusController';
    angular.module('app').controller(controllerId, employeeLeaveStatusController);
    employeeLeaveStatusController.$inject = ['$state', 'employeeLeaveService', 'notificationService', '$location'];

    function employeeLeaveStatusController($state, employeeLeaveService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.empoloyeeLeaves = [];
        vm.addEmployeeLeave = addEmployeeLeave;
        vm.updateEmployeeLeave = updateEmployeeLeave;
        vm.deleteEmployeeLeave = deleteEmployeeLeave;
        vm.pageChanged = pageChanged;
        vm.leaveTypes = [];
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.leaveTypeId = 0;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;

        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
            vm.searchText = location.search().q;
        }
        if (location.search().le !== undefined && location.search().le !== null && location.search().le !== '') {
            vm.leaveTypeId = location.search().le;
        }
        init();
        function init() {

            employeeLeaveService.GetEmployeeLeavesForAuthorizedUser(vm.pageSize, vm.pageNumber, vm.searchText, vm.leaveTypeId).then(function (data) {
                vm.empoloyeeLeaves = data.result;
                vm.total = vm.empoloyeeLeaves.total;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
            employeeLeaveService.getEmployeeLeaves(vm.pageSize, vm.pageNumber, vm.searchText, vm.leaveTypeId).then(function (data) {
                vm.leaveTypes = data.result.leaveTypes;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addEmployeeLeave() {

            $state.go('employeeLeave-create');
        }

        function updateEmployeeLeave(employeeLeave) {
            $state.go('employeeLeave-modify', { id: employeeLeave.empLeaveId });
        }

        function deleteEmployeeLeave(empoloyeeLeaves) {
            employeeService.deleteEmployee(employee.employeeId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('employee-approved-leaves', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText, le: vm.leaveTypeId }, { reload: true, inherit: false });
        }

        function onSearch() {

            vm.pageNumber = 1;
            pageChanged();
            vm.searchText = '';
        }
    }

})();
