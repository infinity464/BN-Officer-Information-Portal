(function () {

    'use strict';
    var controllerId = 'upazilasController';
    angular.module('app').controller(controllerId, upazilasController);
    upazilasController.$inject = ['$state', 'upazilaService', 'notificationService', '$location'];

    function upazilasController($state, upazilaService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.upazilas = [];
        vm.addUpazila = addUpazila;
        vm.updateUpazila = updateUpazila;
        vm.deleteUpazila = deleteUpazila;
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
            upazilaService.getUpazilas(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.upazilas = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addUpazila() {
            $state.go('upazila-create');
        }

        function updateUpazila(upazila) {
            $state.go('upazila-modify', { id: upazila.upazilaId});
        }

        function deleteUpazila(upazila) {
            upazilaService.deleteUpazila(upazila.upazilaId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('upazilas', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
