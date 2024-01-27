(function () {

    'use strict';
    var controllerId = 'coursePointAddController';
    angular.module('app').controller(controllerId, coursePointAddController);
    coursePointAddController.$inject = ['$stateParams', '$state', 'traceSettingService', 'notificationService'];

    function coursePointAddController($stateParams, $state, traceSettingService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.coursePointId = 0;
        vm.traceSettingId = 0;
        vm.CoursePoint = {};
        vm.courseCategories = [];
        vm.title = 'ADD MODE';
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.coursePointForm = {};


        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
        }
        if ($stateParams.coursePointId > 0) {
            vm.coursePointId = $stateParams.coursePointId;
            vm.title = 'UPDATE MODE';
        }
        init();
        function init() {
            traceSettingService.getCoursePoint(vm.traceSettingId,vm.coursePointId).then(function (data) {
                vm.coursePoint = data.result.coursePoint;
                vm.courseCategories = data.result.courseCategories;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function save() {
            if (vm.coursePointId > 0 && vm.coursePointId !== '') {
                updateCoursePoint();
            } else {  
                insertCoursePoint();
            }
        }
        function insertCoursePoint() {
            traceSettingService.saveCoursePoint(vm.traceSettingId, vm.coursePoint).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateCoursePoint() {
            traceSettingService.updateCoursePoint(vm.coursePointId, vm.coursePoint).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('trace-setting-tabs.course-points');
        }
    }

})();
