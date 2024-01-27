(function () {

    'use strict';
    var controllerId = 'categoriesController';
    angular.module('app').controller(controllerId, categoriesController);
    categoriesController.$inject = ['$state', 'categoryService', 'notificationService', '$location'];

    function categoriesController($state, categoryService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.categories = [];
        vm.addCategory = addCategory;
        vm.updateCategory = updateCategory;
        vm.deleteCategory = deleteCategory;
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
            categoryService.getCategories(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.categories = data.result;
                vm.total = data.total; vm.permission = data.permission;
                vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addCategory() {
            $state.go('category-create');
        }

        function updateCategory(category) {
            $state.go('category-modify', { id: category.categoryId});
        }

        function deleteCategory(category) {
            categoryService.deleteCategory(category.categoryId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('categories', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
