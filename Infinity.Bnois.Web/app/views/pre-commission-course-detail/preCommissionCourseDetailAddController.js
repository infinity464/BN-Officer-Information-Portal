(function () {

    'use strict';

    var controllerId = 'preCommissionCourseDetailAddController';

    angular.module('app').controller(controllerId, preCommissionCourseDetailAddController);
    preCommissionCourseDetailAddController.$inject = ['$stateParams', 'preCommissionCourseDetailService', 'notificationService', '$state'];

    function preCommissionCourseDetailAddController($stateParams, preCommissionCourseDetailService, notificationService, $state) {
        var vm = this;
        vm.preCommissionCourseDetailId = 0;
        vm.preCommissionCourseId = 0;
        vm.title = 'ADD MODE';
        vm.preCommissionCourseDetail = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.preCommissionCourseDetailForm = {};

        if ($stateParams.preCommissionCourseDetailId !== undefined && $stateParams.preCommissionCourseDetailId !== null && $stateParams.preCommissionCourseDetailId>0) {
            vm.preCommissionCourseDetailId = $stateParams.preCommissionCourseDetailId;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }
        if ($stateParams.preCommissionCourseId !== undefined && $stateParams.preCommissionCourseId !== null) {
            vm.preCommissionCourseId = $stateParams.preCommissionCourseId;
        }

        Init();
        function Init() {
            preCommissionCourseDetailService.getPreCommissionCourseDetail(vm.preCommissionCourseId, vm.preCommissionCourseDetailId).then(function (data) {
                vm.preCommissionCourseDetail = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.preCommissionCourseDetailId >0 && vm.preCommissionCourseDetailId !== '') {
                updatePreCommissionCourseDetail();
            } else {
                insertPreCommissionCourseDetail();
            }
        }

        function insertPreCommissionCourseDetail() {
            vm.preCommissionCourseDetail.preCommissionCourseId = vm.preCommissionCourseId;
            preCommissionCourseDetailService.savePreCommissionCourseDetail(vm.preCommissionCourseDetail).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updatePreCommissionCourseDetail() {
            preCommissionCourseDetailService.updatePreCommissionCourseDetail(vm.preCommissionCourseDetailId, vm.preCommissionCourseDetail).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('employee-tabs.pre-commission-course-details', { preCommissionCourseId: vm.preCommissionCourseId });
        }
    }
})();
