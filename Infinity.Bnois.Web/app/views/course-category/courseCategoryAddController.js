(function () {

    'use strict';

    var controllerId = 'courseCategoryAddController';

    angular.module('app').controller(controllerId, courseCategoryAddController);
    courseCategoryAddController.$inject = ['$stateParams', 'courseCategoryService', 'notificationService', '$state'];

    function courseCategoryAddController($stateParams, courseCategoryService, notificationService, $state) {
        var vm = this;
        vm.courseCategoryId = 0;
        vm.title = 'ADD MODE';
        vm.courseCategory = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.courseCategoryForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.courseCategoryId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
           courseCategoryService.getCourseCategory(vm.courseCategoryId).then(function (data) {
                vm.courseCategory = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.courseCategoryId !== 0 && vm.courseCategoryId !== '') {
                updateCourseCategory();
            } else {
                insertCourseCategory();
            }
        }

        function insertCourseCategory() {
            courseCategoryService.saveCourseCategory(vm.courseCategory).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateCourseCategory() {
            courseCategoryService.updateCourseCategory(vm.courseCategoryId, vm.courseCategory).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('course-categories');
        }
    }
})();
