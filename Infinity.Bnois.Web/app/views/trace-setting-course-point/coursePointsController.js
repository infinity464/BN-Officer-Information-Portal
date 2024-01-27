(function () {

    'use strict';
    var controllerId = 'coursePointsController';
    angular.module('app').controller(controllerId, coursePointsController);
    coursePointsController.$inject = ['$stateParams', '$state', 'traceSettingService', 'notificationService'];

    function coursePointsController($stateParams, $state, traceSettingService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.traceSettingId = 0;
        vm.coursePointId = 0;
        vm.coursePoints = [];
        vm.title = 'Course Points';
        vm.addCoursePoint = addCoursePoint;
        vm.updateCoursePoint = updateCoursePoint;
        vm.close = close;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
        }

        init();
        function init() {
            traceSettingService.getCoursePoints(vm.traceSettingId).then(function (data) {
                vm.coursePoints = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function addCoursePoint() {
            $state.go('trace-setting-tabs.course-point-create', { traceSettingId: vm.traceSettingId,coursePointId: vm.coursePointId });
        }
        
        function updateCoursePoint(coursePoint) {
            $state.go('trace-setting-tabs.course-point-modify', { id: vm.traceSettingId, coursePointId: coursePoint.coursePointId });
        }
        function close() {
            $state.go('trace-setting-tabs.course-points')
        }
    }

})();
