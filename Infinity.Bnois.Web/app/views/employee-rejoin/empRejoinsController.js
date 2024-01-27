(function () {

    'use strict';
    var controllerId = 'empRejoinsController';
    angular.module('app').controller(controllerId, empRejoinsController);
    empRejoinsController.$inject = ['$state', 'empRejoinService', 'notificationService', '$location'];

    function empRejoinsController($state, empRejoinService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.empRejoins = [];
        vm.addEmpRejoin = addEmpRejoin;
        vm.updateEmpRejoin = updateEmpRejoin;
        vm.deleteEmpRejoin = deleteEmpRejoin;
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
            empRejoinService.getEmpRejoins(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.empRejoins = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addEmpRejoin() {
            $state.go('employee-rejoin-create');
        }

        function updateEmpRejoin(empRejoin) {
            $state.go('employee-rejoin-modify', { id: empRejoin.empRejoinId });
        }

        function deleteEmpRejoin(empRejoin) {
            empRejoinService.deleteEmpRejoin(empRejoin.empRejoinId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('employee-rejoins', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
