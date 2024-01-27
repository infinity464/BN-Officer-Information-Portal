/// <reference path="../../services/medalService.js" />

(function () {

    'use strict';
    var controllerId = 'medalsController';
    angular.module('app').controller(controllerId, medalsController);
    medalsController.$inject = ['$state', 'medalService', 'notificationService', '$location'];

    function medalsController($state, medalService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.medal = [];
        vm.addMedal = addMedal;
        vm.updateMedal = updateMedal;
        vm.deleteMedal = deleteMedal;
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
            medalService.getMedals(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.medals = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addMedal() {
            $state.go('medal-create');
        }

        function updateMedal(medal) {
            $state.go('medal-modify', { id: medal.medalId });
        }

        function deleteMedal(medal) {
            medalService.deleteMedal(medal.medalId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('medals', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
