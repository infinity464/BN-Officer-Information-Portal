(function () {

    'use strict';
    var controllerId = 'employeeHajjDetailsController';
    angular.module('app').controller(controllerId, employeeHajjDetailsController);
    employeeHajjDetailsController.$inject = ['$state', 'employeeHajjDetailService', 'notificationService', '$location'];

    function employeeHajjDetailsController($state, employeeHajjDetailService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.employeeHajjDetails = [];
        vm.addEmployeeHajjDetail = addEmployeeHajjDetail;
        vm.updateEmployeeHajjDetail = updateEmployeeHajjDetail;
        vm.deleteEmployeeHajjDetail = deleteEmployeeHajjDetail;
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
            employeeHajjDetailService.getEmployeeHajjDetails(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {

                vm.employeeHajjDetails = data.result;
                vm.total = data.total;
                vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addEmployeeHajjDetail() {
            $state.go('employee-hajj-detail-create');
        }

        function updateEmployeeHajjDetail(employeeHajjDetail) {
            $state.go('employee-hajj-detail-modify', { id: employeeHajjDetail.employeeHajjDetailId });
        }

        function deleteEmployeeHajjDetail(employeeHajjDetail) {
            employeeHajjDetailService.deleteEmployeeHajjDetail(employeeHajjDetail.employeeHajjDetailId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('employee-hajj-detail', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });

        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
