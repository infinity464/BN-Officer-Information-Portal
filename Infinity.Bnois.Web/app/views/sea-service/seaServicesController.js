

(function () {

    'use strict';
    var controllerId = 'seaServicesController';
    angular.module('app').controller(controllerId, seaServicesController);
    seaServicesController.$inject = ['$state', 'seaServiceService', 'notificationService', '$location'];

    function seaServicesController($state, seaServiceService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.seaServices = [];
        vm.addSeaService = addSeaService;
        vm.updateSeaService = updateSeaService;
        vm.deleteSeaService = deleteSeaService;
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
            seaServiceService.getSeaServices(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.seaServices = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addSeaService() {
            $state.go('sea-service-create');
        }

        function updateSeaService(seaService) {
            $state.go('sea-service-modify', { id: seaService.seaServiceId });
        }

        function deleteSeaService(seaService) {
            seaServiceService.deleteSeaService(seaService.seaServiceId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('sea-services', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();

