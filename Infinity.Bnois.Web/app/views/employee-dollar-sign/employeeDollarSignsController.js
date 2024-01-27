(function () {

    'use strict';
    var controllerId = 'employeeDollarSignsController';
    angular.module('app').controller(controllerId, employeeDollarSignsController);
    employeeDollarSignsController.$inject = ['$state', 'employeeService', 'notificationService', '$location'];

    function employeeDollarSignsController($state, employeeService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.employeeDollarSigns = [];
        vm.addEmployeeDollarSign = addEmployeeDollarSign;
        vm.updateEmployeeDollarSign = updateEmployeeDollarSign;
        vm.deleteEmployeeDollarSign = deleteEmployeeDollarSign;
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
            employeeService.getEmployeesByDollarSign(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.employeeDollarSigns = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addEmployeeDollarSign() {
            $state.go('employee-dollar-sign-create');
        }

        function updateEmployeeDollarSign(employeeDollarSign) {
            $state.go('employee-dollar-sign-modify', { employeeId: employeeDollarSign.employeeId });
        }

        function deleteEmployeeDollarSign(employeeDollarSign) {
            employeeService.deleteEmployeeDollarSign(employeeDollarSign.employeeId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('employee-dollar-signs', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
