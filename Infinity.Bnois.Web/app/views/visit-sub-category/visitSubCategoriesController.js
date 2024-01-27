(function () {

    'use strict';
    var controllerId = 'visitSubCategoriesController';
    angular.module('app').controller(controllerId, visitSubCategoriesController);
    visitSubCategoriesController.$inject = ['$state', 'visitSubCategoryService', 'notificationService', '$location'];

    function visitSubCategoriesController($state, visitSubCategoryService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.visitSubCategories = [];
        vm.addVisitSubCategory = addVisitSubCategory;
        vm.updateVisitSubCategory = updateVisitSubCategory;
        vm.deleteVisitSubCategory = deleteVisitSubCategory;
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
            visitSubCategoryService.getVisitSubCategories(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.visitSubCategories = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addVisitSubCategory() {
            $state.go('visit-sub-category-create');
        }

        function updateVisitSubCategory(visitSubCategory) {
            $state.go('visit-sub-category-modify', { id: visitSubCategory.visitSubCategoryId});
        }

        function deleteVisitSubCategory(visitSubCategory) {
            visitSubCategoryService.deleteVisitSubCategory(visitSubCategory.visitSubCategoryId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('visit-sub-categories', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
