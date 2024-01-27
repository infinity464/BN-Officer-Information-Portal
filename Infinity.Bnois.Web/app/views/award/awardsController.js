/// <reference path="../../services/awardService.js" />

(function () {

    'use strict';
    var controllerId = 'awardsController';
    angular.module('app').controller(controllerId, awardsController);
    awardsController.$inject = ['$state', 'awardService', 'notificationService', '$location'];

    function awardsController($state, awardService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.award = [];
        vm.addAward = addAward;
        vm.updateAward = updateAward;
        vm.deleteAward = deleteAward;
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
            awardService.getAwards(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.awards = data.result;
                console.log(data.result);
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addAward() {
            $state.go('award-create');
        }

        function updateAward(award) {
            $state.go('award-modify', { id: award.awardId });
        }

        function deleteAward(award) {
            awardService.deleteAward(award.awardId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('awards', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
