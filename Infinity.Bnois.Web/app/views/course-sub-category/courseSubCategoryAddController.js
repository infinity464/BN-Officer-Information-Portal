/// <reference path="../../services/employeegeneralservice.js" />
(function () {

    'use strict';

    var controllerId = 'courseSubCategoryAddController';

    angular.module('app').controller(controllerId, courseSubCategoryAddController);
    courseSubCategoryAddController.$inject = ['$stateParams', 'courseSubCategoryService', 'notificationService', '$state'];

    function courseSubCategoryAddController($stateParams, courseSubCategoryService, notificationService, $state) {
        var vm = this;
        vm.courseSubCategoryId = 0;
        vm.title = 'ADD MODE';
        vm.courseSubCategory = {};
        vm.courseCategories = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.courseSubCategoryForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.courseSubCategoryId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            courseSubCategoryService.getCourseSubCategory(vm.courseSubCategoryId).then(function (data) {
                vm.courseSubCategory = data.result.courseSubCategory;
                vm.courseCategories = data.result.courseCategories;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.courseSubCategoryId !== 0 && vm.courseSubCategoryId !== '') {
                updateCourseSubCategory();
            } else {
                insertCourseSubCategory();
            }
        }

        function insertCourseSubCategory() {
            courseSubCategoryService.saveCourseSubCategory(vm.courseSubCategory).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateCourseSubCategory() {
            courseSubCategoryService.updateCourseSubCategory(vm.courseSubCategoryId, vm.courseSubCategory).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('course-sub-categories');
        }
    }
})();
