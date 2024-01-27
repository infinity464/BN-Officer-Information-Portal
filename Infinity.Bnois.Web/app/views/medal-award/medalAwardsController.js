

(function () {

    'use strict';
    var controllerId = 'medalAwardsController';
    angular.module('app').controller(controllerId, medalAwardsController);
    medalAwardsController.$inject = ['$state', 'medalAwardService', 'notificationService', '$location'];

    function medalAwardsController($state, medalAwardService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.medalAwards = [];
        vm.addMedalAward = addMedalAward;
        vm.updateMedalAward = updateMedalAward;
        vm.deleteMedalAward = deleteMedalAward;
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
            medalAwardService.getMedalAwards(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.medalAwards = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addMedalAward() {
            $state.go('medal-award-create');
        }

        function updateMedalAward(medalAward) {
            $state.go('medal-award-modify', { id: medalAward.medalAwardId });
        }

        function deleteMedalAward(medalAward) {
            medalAwardService.deleteMedalAward(medalAward.medalAwardId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('medal-awards', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();

