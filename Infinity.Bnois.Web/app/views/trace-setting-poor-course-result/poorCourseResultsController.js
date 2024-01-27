(function () {

    'use strict';
    var controllerId = 'poorCourseResultsController';
    angular.module('app').controller(controllerId, poorCourseResultsController);
    poorCourseResultsController.$inject = ['$stateParams', '$state', 'traceSettingService', 'notificationService'];

    function poorCourseResultsController($stateParams, $state, traceSettingService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.traceSettingId = 0;
        vm.poorCourseResultId = 0;
        vm.poorCourseResults = [];
        vm.title = 'Course Points';
        vm.addPoorCourseResult = addPoorCourseResult;
        vm.updatePoorCourseResult = updatePoorCourseResult;
        vm.close = close;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
        }

        init();
        function init() {
            traceSettingService.getPoorCourseResults(vm.traceSettingId).then(function (data) {
                vm.poorCourseResults = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function addPoorCourseResult() {
            $state.go('trace-setting-tabs.poor-course-result-create', { traceSettingId: vm.traceSettingId,poorCourseResultId: vm.poorCourseResultId });
        }
        
        function updatePoorCourseResult(poorCourseResult) {
            $state.go('trace-setting-tabs.poor-course-result-modify', { traceSettingId: vm.traceSettingId, poorCourseResultId: poorCourseResult.poorCourseResultId });
        }
        function close() {
            $state.go('trace-setting-tabs.poor-course-results')
        }
    }

})();
