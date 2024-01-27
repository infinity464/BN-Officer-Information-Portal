(function () {

    'use strict';
    var controllerId = 'empBackToUnitsController';
    angular.module('app').controller(controllerId, empBackToUnitsController);
    empBackToUnitsController.$inject = ['$state', 'empRunMissingService', 'notificationService', '$location'];

    function empBackToUnitsController($state, empRunMissingService, notificationService, location) {
        /* jshint validthis:true */
        var vm = this;
        vm.empBackToUnits = [];
        vm.addEmpBackToUnit = addEmpBackToUnit;
        vm.updateEmployeeBacktoUnit = updateEmployeeBacktoUnit;
        vm.deleteEmpBackToUnit = deleteEmpBackToUnit;
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
            empRunMissingService.getEmpBackToUnits(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.empBackToUnits = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addEmpBackToUnit() {
            $state.go('emp-back-to-unit-create');
        }

        function updateEmployeeBacktoUnit(empBackToUnit) {
            $state.go('emp-back-to-unit-modify', { id: empBackToUnit.empRunMissingId});
        }

        function deleteEmpBackToUnit(empBackToUnit) {
            empRunMissingService.deleteEmpBackToUnit(empBackToUnit.empRunMissingId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('emp-back-to-units', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
