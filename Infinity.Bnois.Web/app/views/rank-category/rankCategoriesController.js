(function () {

    'use strict';
    var controllerId = 'rankCategoriesController';
    angular.module('app').controller(controllerId, rankCategoriesController);
    rankCategoriesController.$inject = ['$state', 'rankCategoryService', 'notificationService', '$location'];

    function rankCategoriesController($state, rankCategoryService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.rankCategories = [];
        vm.addRankCategory = addRankCategory;
        vm.updateRankCategory = updateRankCategory;
        vm.deleteRankCategory = deleteRankCategory;
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
            rankCategoryService.getRankCategories(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.rankCategories = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addRankCategory() {
            $state.go('rank-category-create');
        }

        function updateRankCategory(rankCategory) {
            $state.go('rank-category-modify', { id: rankCategory.rankCategoryId});
        }

        function deleteRankCategory(rankCategory) {
            rankCategoryService.deleteRankCategory(rankCategory.rankCategoryId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('rank-categories', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
