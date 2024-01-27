

(function () {

    'use strict';
    var controllerId = 'employeeServiceExamResultsController';
    angular.module('app').controller(controllerId, employeeServiceExamResultsController);
    employeeServiceExamResultsController.$inject = ['$state', 'employeeServiceExamResultService', 'notificationService', '$location'];

    function employeeServiceExamResultsController($state, employeeServiceExamResultService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.employeeServiceExamResults = [];
        vm.addEmployeeServiceExamResult = addEmployeeServiceExamResult;
        vm.updateEmployeeServiceExamResult = updateEmployeeServiceExamResult;
        vm.deleteEmployeeServiceExamResult = deleteEmployeeServiceExamResult;
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
            employeeServiceExamResultService.getEmployeeServiceExamResults(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.employeeServiceExamResults = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addEmployeeServiceExamResult() {
            $state.go('employee-service-exam-result-create');
        }

        function updateEmployeeServiceExamResult(employeeServiceExamResult) {
            $state.go('employee-service-exam-result-modify', { id: employeeServiceExamResult.employeeServiceExamResultId });
        }

        function deleteEmployeeServiceExamResult(employeeServiceExamResult) {
            employeeServiceExamResultService.deleteEmployeeServiceExamResult(employeeServiceExamResult.employeeServiceExamResultId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('employee-service-exam-results', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();

