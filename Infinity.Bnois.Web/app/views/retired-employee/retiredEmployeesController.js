(function () {

    'use strict';
    var controllerId = 'retiredEmployeesController';
    angular.module('app').controller(controllerId, retiredEmployeesController);
    retiredEmployeesController.$inject = ['$state', 'retiredEmployeeService', 'notificationService', '$location'];

    function retiredEmployeesController($state, retiredEmployeeService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.retiredEmployees = [];
        vm.detailsRetiredEmployee = detailsRetiredEmployee; 
       
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
            retiredEmployeeService.getRetiredEmployees(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.retiredEmployees = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function detailsRetiredEmployee(retiredEmployee) {
             $state.go('retired-employee', { id: retiredEmployee.employeeId });

        }



       
       


        function pageChanged() {
            $state.go('retired-employees', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
