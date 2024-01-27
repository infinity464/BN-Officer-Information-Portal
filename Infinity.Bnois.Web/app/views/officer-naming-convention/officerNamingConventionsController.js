(function () {

    'use strict';
    var controllerId = 'officerNamingConventionsController';
    angular.module('app').controller(controllerId, officerNamingConventionsController);
    officerNamingConventionsController.$inject = ['$state', 'subCategoryService', 'notificationService', '$location'];

    function officerNamingConventionsController($state, subCategoryService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.subCategories = [];
        vm.updateSubCategory = updateSubCategory;
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

        function updateSubCategory(subCategory) {
            $state.go('officer-naming-convention-modify', { id: subCategory.subCategoryId});
        }



        function pageChanged() {
            $state.go('officer-naming-conventions', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
