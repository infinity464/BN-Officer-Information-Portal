(function () {

    'use strict';
    var controllerId = 'poorCourseResultAddController';
    angular.module('app').controller(controllerId, poorCourseResultAddController);
    poorCourseResultAddController.$inject = ['$stateParams', '$state', 'traceSettingService', 'notificationService'];

    function poorCourseResultAddController($stateParams, $state, traceSettingService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.poorCourseResultId = 0;
        vm.traceSettingId = 0;
        vm.poorCourseResult = {};
        vm.resultTypes = [];
        vm.title = 'ADD MODE';
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.poorCourseResultForm = {};


        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
        }
        if ($stateParams.poorCourseResultId > 0) {
            vm.poorCourseResultId = $stateParams.poorCourseResultId;
            vm.title = 'UPDATE MODE';
        }
        init();
        function init() {
            traceSettingService.getPoorCourseResult(vm.traceSettingId,vm.poorCourseResultId).then(function (data) {
                vm.poorCourseResult = data.result.poorCourseResult;
                vm.resultTypes = data.result.resultTypes;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function save() {
            if (vm.poorCourseResultId > 0 && vm.poorCourseResultId !== '') {
                updatePoorCourseResult();
            } else {  
                insertPoorCourseResult();
            }
        }
        function insertPoorCourseResult() {
            traceSettingService.savePoorCourseResult(vm.traceSettingId, vm.poorCourseResult).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updatePoorCourseResult() {
            traceSettingService.updatePoorCourseResult(vm.poorCourseResultId, vm.poorCourseResult).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('trace-setting-tabs.poor-course-results');
        }
    }

})();
