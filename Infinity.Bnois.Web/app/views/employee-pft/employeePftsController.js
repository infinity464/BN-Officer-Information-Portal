

(function () {

    'use strict';
    var controllerId = 'employeePftsController';
    angular.module('app').controller(controllerId, employeePftsController);
    employeePftsController.$inject = ['$state', 'employeePftService', 'notificationService', '$location'];

    function employeePftsController($state, employeePftService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.employeePfts = [];
        vm.addEmployeePft = addEmployeePft;
        vm.updateEmployeePft = updateEmployeePft;
        vm.deleteEmployeePft = deleteEmployeePft;
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
            employeePftService.getEmployeePfts(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.employeePfts = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addEmployeePft() {
            $state.go('employee-pft-create');
        }

        function updateEmployeePft(employeePft) {
            $state.go('employee-pft-modify', { id: employeePft.employeePftId });
        }

        function deleteEmployeePft(employeePft) {
            employeePftService.deleteEmployeePft(employeePft.employeePftId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('employee-pfts', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();

