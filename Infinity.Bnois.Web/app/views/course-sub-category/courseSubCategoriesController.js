(function () {

    'use strict';
    var controllerId = 'courseSubCategoriesController';
    angular.module('app').controller(controllerId, courseSubCategoriesController);
    courseSubCategoriesController.$inject = ['$state', 'courseSubCategoryService', 'notificationService', '$location'];

    function courseSubCategoriesController($state, courseSubCategoryService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.courseSubCategories = [];
        vm.addCourseSubCategory = addCourseSubCategory;
        vm.updateCourseSubCategory = updateCourseSubCategory;
        vm.deleteCourseSubCategory = deleteCourseSubCategory;
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
            courseSubCategoryService.getCourseSubCategories(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.courseSubCategories = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addCourseSubCategory() {
            $state.go('course-sub-category-create');
        }

        function updateCourseSubCategory(courseSubCategory) {
            $state.go('course-sub-category-modify', { id: courseSubCategory.courseSubCategoryId});
        }

        function deleteCourseSubCategory(courseSubCategory) {
            courseSubCategoryService.deleteCourseSubCategory(courseSubCategory.courseSubCategoryId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('course-sub-categories', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
