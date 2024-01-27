(function () {

    'use strict';
    var controllerId = 'braCtryCoursePointsController';
    angular.module('app').controller(controllerId, braCtryCoursePointsController);
    braCtryCoursePointsController.$inject = ['$stateParams', '$state', 'traceSettingService', 'notificationService'];

    function braCtryCoursePointsController($stateParams, $state, traceSettingService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.traceSettingId = 0;
        vm.braCtryCoursePointId = 0;
        vm.braCtryCoursePoints = [];
        vm.title = 'Course Points';
        vm.addBraCtryCoursePoint = addBraCtryCoursePoint;
        vm.updateBraCtryCoursePoint = updateBraCtryCoursePoint;
        vm.close = close;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
        }

        init();
        function init() {
            traceSettingService.getBraCtryCoursePoints(vm.traceSettingId).then(function (data) {
                vm.braCtryCoursePoints = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function addBraCtryCoursePoint() {
            $state.go('trace-setting-tabs.branch-country-course-point-create', { traceSettingId: vm.traceSettingId,braCtryCoursePointId: vm.braCtryCoursePointId });
        }
        
        function updateBraCtryCoursePoint(braCtryCoursePoint) {
            $state.go('trace-setting-tabs.branch-country-course-point-modify', { traceSettingId: vm.traceSettingId, braCtryCoursePointId: braCtryCoursePoint.braCtryCoursePointId });
        }
        function close() {
            $state.go('trace-setting-tabs.branch-country-course-points')
        }
    }

})();
