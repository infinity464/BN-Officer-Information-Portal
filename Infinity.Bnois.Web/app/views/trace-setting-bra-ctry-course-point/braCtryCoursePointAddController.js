(function () {

    'use strict';
    var controllerId = 'braCtryCoursePointAddController';
    angular.module('app').controller(controllerId, braCtryCoursePointAddController);
    braCtryCoursePointAddController.$inject = ['$stateParams', '$state', 'traceSettingService','courseService', 'notificationService'];

    function braCtryCoursePointAddController($stateParams, $state, traceSettingService, courseService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.braCtryCoursePointId = 0;
        vm.traceSettingId = 0;
        vm.braCtryCoursePoint = {};
        vm.courseCategories = [];
        vm.courseSubCategories = [];
        vm.rankCategories = [];
        vm.branches = [];
        vm.countries = [];
        vm.title = 'ADD MODE';
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.braCtryCoursePointForm = {};
        vm.getCourseSubCategoryByCourseCategory = getCourseSubCategoryByCourseCategory;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
        }
        if ($stateParams.braCtryCoursePointId > 0) {
            vm.braCtryCoursePointId = $stateParams.braCtryCoursePointId;
            vm.title = 'UPDATE MODE';
        }
        init();
        function init() {
            traceSettingService.getBraCtryCoursePoint(vm.traceSettingId,vm.braCtryCoursePointId).then(function (data) {
                vm.braCtryCoursePoint = data.result.braCtryCoursePoint;
                vm.courseCategories = data.result.courseCategories;
                vm.courseSubCategories = data.result.courseSubCategories;
                vm.rankCategories = data.result.rankCategories;
                vm.branches = data.result.branches;
                vm.countries = data.result.countries;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function save() {
            if (vm.braCtryCoursePointId > 0 && vm.braCtryCoursePointId !== '') {
                updateBraCtryCoursePoint();
            } else {  
                insertBraCtryCoursePoint();
            }
        }
        function insertBraCtryCoursePoint() {
            traceSettingService.saveBraCtryCoursePoint(vm.traceSettingId, vm.braCtryCoursePoint).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
  
        function updateBraCtryCoursePoint() {
            traceSettingService.updateBraCtryCoursePoint(vm.braCtryCoursePointId, vm.braCtryCoursePoint).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function getCourseSubCategoryByCourseCategory(courseCategoryId) {
            courseService.getCourseSubCategories(courseCategoryId).then(function (data) {
                vm.courseSubCategories = data.result.courseSubCategories;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('trace-setting-tabs.branch-country-course-points');
        }
    }

})();
