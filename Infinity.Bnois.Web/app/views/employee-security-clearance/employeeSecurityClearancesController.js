(function () {

    'use strict';
    var controllerId = 'employeeSecurityClearancesController';
    angular.module('app').controller(controllerId, employeeSecurityClearancesController);
    employeeSecurityClearancesController.$inject = ['$state', 'employeeSecurityClearanceService', 'notificationService', '$location'];

    function employeeSecurityClearancesController($state, employeeSecurityClearanceService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.employeeSecurityClearances = [];
        vm.addEmployeeSecurityClearance = addEmployeeSecurityClearance;
        vm.updateEmployeeSecurityClearance = updateEmployeeSecurityClearance;
        vm.deleteEmployeeSecurityClearance = deleteEmployeeSecurityClearance;
        vm.reasonList = reasonList;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
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
        init();
        function init() {
            employeeSecurityClearanceService.getEmployeeSecurityClearances(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.employeeSecurityClearances = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addEmployeeSecurityClearance() {
            $state.go('employee-security-clearance-create');
        }

        function updateEmployeeSecurityClearance(employeeSecurityClearance) {
            $state.go('employee-security-clearance-modify', { id: employeeSecurityClearance.employeeSecurityClearanceId });
        }

        function deleteEmployeeSecurityClearance(employeeSecurityClearance) {
            employeeSecurityClearanceService.deleteEmployeeSecurityClearance(employeeSecurityClearance.employeeSecurityClearanceId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('employee-security-clearances', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function reasonList() {
            $state.go('security-clearance-reasons');
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
