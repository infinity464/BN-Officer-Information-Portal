(function () {

    'use strict';
    var controllerId = 'employeesController';
    angular.module('app').controller(controllerId, employeesController);
    employeesController.$inject = ['$state', 'employeeService', 'notificationService', '$location'];

    function employeesController($state, employeeService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.empoloyees = [];
        vm.addEmployee = addEmployee;
        vm.updateEmployee = updateEmployee;
        vm.deleteEmployee = deleteEmployee;
       
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 100;
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
            employeeService.getEmployees(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.employees = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addEmployee() {
            $state.go('employee-create');
        }

        //function updateEmployee(employee) {
        //    $state.go('employee-modify', { id: employee.employeeId});
        //}

         function updateEmployee(employee) {
             $state.goNewTab('employee-tabs', { employeeId: employee.employeeId });



        }



        function deleteEmployee(employee) {
            employeeService.deleteEmployee(employee.employeeId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

       


        function pageChanged() {
            $state.go('employees', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
