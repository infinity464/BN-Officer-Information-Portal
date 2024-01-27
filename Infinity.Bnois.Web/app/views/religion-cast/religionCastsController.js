(function () {

    'use strict';
    var controllerId = 'religionCastsController';
    angular.module('app').controller(controllerId, religionCastsController);
    religionCastsController.$inject = ['$state', 'religionCastService', 'notificationService', '$location'];

    function religionCastsController($state, religionCastService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.religionCasts = [];
        vm.addReligionCast = addReligionCast;
        vm.updateReligionCast = updateReligionCast;
        vm.deleteReligionCast = deleteReligionCast;
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
            religionCastService.getReligionCasts(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.religionCasts = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addReligionCast() {
            $state.go('religion-cast-create');
        }

        function updateReligionCast(religionCast) {
            $state.go('religion-cast-modify', { id: religionCast.religionCastId});
        }

        function deleteReligionCast(religionCast) {
            religionCastService.deleteReligionCast(religionCast.religionCastId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('religion-casts', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
