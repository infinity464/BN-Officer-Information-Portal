/// <reference path="../../services/courseService.js" />
(function () {

    'use strict';

    var controllerId = 'courseAddController';

    angular.module('app').controller(controllerId, courseAddController);
    courseAddController.$inject = ['$stateParams', 'courseService', 'notificationService', '$state'];

    function courseAddController($stateParams, courseService, notificationService, $state) {
        var vm = this;
        vm.courseId = 0;
        vm.title = 'ADD MODE';
        vm.course = {};
        vm.courseCategories = [];
        vm.courseSubCategories = [];
        vm.countries = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.courseForm = {};
        vm.getCourseSubCategory = getCourseSubCategory;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.courseId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            courseService.getCourse(vm.courseId,0).then(function (data) {
                vm.course = data.result.course;
                vm.countries = data.result.countries;
                vm.courseCategories = data.result.courseCategories;
                    if (vm.courseId !== 0 && vm.courseId !== '') {
                        vm.courseSubCategories = data.result.courseSubCategories;
                    }
                
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.courseId !== 0 && vm.courseId !== '') {
                updateCourse();
            } else {
                insertCourse();
            }
        }

        function insertCourse() {
            courseService.saveCourse(vm.course).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateCourse() {
            courseService.updateCourse(vm.courseId, vm.course).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('courses');
        }

        function getCourseSubCategory(categoryId) {
            courseService.getCourseSubCategories(categoryId).then(function (data) {
                vm.courseSubCategories = data.result.courseSubCategories;
            });
        }
    }
})();
