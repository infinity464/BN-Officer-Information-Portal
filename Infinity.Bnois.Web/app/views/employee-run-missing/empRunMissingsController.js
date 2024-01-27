(function () {

    'use strict';
    var controllerId = 'empRunMissingsController';
    angular.module('app').controller(controllerId, empRunMissingsController);
    empRunMissingsController.$inject = ['$state', 'empRunMissingService', 'notificationService', '$location'];

    function empRunMissingsController($state, empRunMissingService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.empRunMissings = [];
        vm.addEmpRunMissing = addEmpRunMissing;
        vm.updateEmpRunMissing = updateEmpRunMissing;
        vm.deleteEmpRunMissing = deleteEmpRunMissing;
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
            empRunMissingService.getEmpRunMissings(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.empRunMissings = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addEmpRunMissing() {
            $state.go('employee-run-missing-create');
        }

        function updateEmpRunMissing(empRunMissing) {
            $state.go('employee-run-missing-modify', { id: empRunMissing.empRunMissingId});
        }

        function deleteEmpRunMissing(empRunMissing) {
            empRunMissingService.deleteEmpRunMissing(empRunMissing.empRunMissingId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('employee-run-missings', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
