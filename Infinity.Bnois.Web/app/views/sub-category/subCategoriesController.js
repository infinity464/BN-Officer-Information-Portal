(function () {

    'use strict';
    var controllerId = 'subCategoriesController';
    angular.module('app').controller(controllerId, subCategoriesController);
    subCategoriesController.$inject = ['$state', 'subCategoryService', 'notificationService', '$location'];

    function subCategoriesController($state, subCategoryService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.subCategories = [];
        vm.addSubCategory = addSubCategory;
        vm.updateSubCategory = updateSubCategory;
        vm.deleteSubCategory = deleteSubCategory;
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
            subCategoryService.getSubCategories(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.subCategories = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addSubCategory() {
            $state.go('sub-category-create');
        }

        function updateSubCategory(subCategory) {
            $state.go('sub-category-modify', { id: subCategory.subCategoryId});
        }

        function deleteSubCategory(subCategory) {
            subCategoryService.deleteSubCategory(subCategory.subCategoryId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('sub-categories', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
