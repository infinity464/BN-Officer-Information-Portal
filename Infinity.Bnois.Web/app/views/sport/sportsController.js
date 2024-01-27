/// <reference path="../../services/sportService.js" />

(function () {

    'use strict';
    var controllerId = 'sportsController';
    angular.module('app').controller(controllerId, sportsController);
    sportsController.$inject = ['$state', 'sportService', 'notificationService', '$location'];

    function sportsController($state, sportService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.sports = [];
        vm.addSport = addSport;
        vm.updateSport = updateSport;
        vm.deleteSport = deleteSport;
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
            sportService.getSports(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.sports = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addSport() {
            $state.go('sport-create');
        }

        function updateSport(sport) {
            $state.go('sport-modify', { id: sport.sportId });
        }

        function deleteSport(sport) {
            sportService.deleteSport(sport.sportId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('sports', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
