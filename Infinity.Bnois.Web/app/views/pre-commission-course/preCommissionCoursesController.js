(function () {

    'use strict';
    var controllerId = 'preCommissionCoursesController';
    angular.module('app').controller(controllerId, preCommissionCoursesController);
    preCommissionCoursesController.$inject = ['$stateParams', '$state', 'preCommissionCourseService', 'notificationService'];

    function preCommissionCoursesController($stateParams, $state, preCommissionCourseService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.preCommissionCourseId = 0;
        vm.preCommissionCourses = [];
        vm.title = 'Pre Commission Test';
        vm.addPreCommissionCourse = addPreCommissionCourse;
        vm.updatePreCommissionCourse = updatePreCommissionCourse;
        vm.getPreCommissionCourseDetails = getPreCommissionCourseDetails;
        vm.deletePreCommissionCourse = deletePreCommissionCourse;

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        init();
        function init() {
            preCommissionCourseService.getPreCommissionCourses(vm.employeeId).then(function (data) {
                vm.preCommissionCourses = data.result;
                    vm.permission = data.permission;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function addPreCommissionCourse() {
            $state.go('employee-tabs.pre-commission-course-create', { id: vm.employeeId, preCommissionCourseId: vm.preCommissionCourseId });
        }
        
        function updatePreCommissionCourse(preCommissionCourse) {
            $state.go('employee-tabs.pre-commission-course-modify', { id: vm.employeeId, preCommissionCourseId: preCommissionCourse.preCommissionCourseId });
        }

        function getPreCommissionCourseDetails(preCommissionCourse) {
            $state.go('employee-tabs.pre-commission-course-details', {  preCommissionCourseId: preCommissionCourse.preCommissionCourseId });
        }


        function deletePreCommissionCourse(preCommissionCourse) {
            preCommissionCourseService.deletePreCommissionCourse(preCommissionCourse.preCommissionCourseId).then(function (data) {
                preCommissionCourseService.getPreCommissionCourses(vm.employeeId).then(function (data) {
                    vm.preCommissionCourses = data.result;
                });
                $state.go('employee-tabs.pre-commission-courses');
            });
        }

    }

})();
