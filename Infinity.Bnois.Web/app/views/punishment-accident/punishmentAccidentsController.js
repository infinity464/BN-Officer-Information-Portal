

(function () {

    'use strict';
    var controllerId = 'punishmentAccidentsController';
    angular.module('app').controller(controllerId, punishmentAccidentsController);
    punishmentAccidentsController.$inject = ['$state', 'punishmentAccidentService', 'notificationService', '$location'];

    function punishmentAccidentsController($state, punishmentAccidentService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.punishmentAccidents = [];
        vm.addPunishmentAccident = addPunishmentAccident;
        vm.updatePunishmentAccident = updatePunishmentAccident;
        vm.deletePunishmentAccident = deletePunishmentAccident;
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
            punishmentAccidentService.getPunishmentAccidents(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.punishmentAccidents = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addPunishmentAccident() {
            $state.go('punishment-accident-create');
        }

        function updatePunishmentAccident(punishmentAccident) {
            $state.go('punishment-accident-modify', { id: punishmentAccident.punishmentAccidentId });
        }

        function deletePunishmentAccident(punishmentAccident) {
            punishmentAccidentService.deletePunishmentAccident(punishmentAccident.punishmentAccidentId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('punishment-accidents', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();

