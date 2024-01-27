(function () {

    'use strict';
    var controllerId = 'ranksController';
    angular.module('app').controller(controllerId, ranksController);
    ranksController.$inject = ['$state', 'rankService', 'notificationService', '$location'];

    function ranksController($state, rankService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.ranks = [];
        vm.permission = {};
        vm.addRank = addRank;
        vm.updateRank = updateRank;
        vm.deleteRank = deleteRank;
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
            rankService.getRanks(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.ranks = data.result;
                vm.total = data.total; vm.permission = data.permission;
                vm.permission = data.permission;
               
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addRank() {
            $state.go('rank-create');
        }

        function updateRank(rank) {
            $state.go('rank-modify', { id: rank.rankId});
        }

        function deleteRank(rank) {
            rankService.deleteRank(rank.rankId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('ranks', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
          
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
