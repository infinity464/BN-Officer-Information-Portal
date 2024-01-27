(function () {

    'use strict';
    var controllerId = 'commissionTypesController';
    angular.module('app').controller(controllerId, commissionTypesController);
    commissionTypesController.$inject = ['$state', 'commissionTypeService', 'notificationService', '$location'];

    function commissionTypesController($state, commissionTypeService, notificationService, location) {
        /* jshint validthis:true */
        var vm = this;
        vm.commissionTypes = [];
        vm.addCommissionType = addCommissionType;
        vm.updateCommissionType = updateCommissionType;
        vm.deleteCommissionType = deleteCommissionType;
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
            commissionTypeService.getCommissionTypes(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.commissionTypes = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addCommissionType() {
            $state.go('commission-type-create');
        }

        function updateCommissionType(commissionType) {
            $state.go('commission-type-modify', { id: commissionType.commissionTypeId});
        }

        function deleteCommissionType(commissionType) {
            commissionTypeService.deleteCommissionType(commissionType.commissionTypeId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('commission-types', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
