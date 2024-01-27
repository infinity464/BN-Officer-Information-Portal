(function () {

    'use strict';
    var controllerId = 'preCommissionCourseDetailsController';
    angular.module('app').controller(controllerId, preCommissionCourseDetailsController);
    preCommissionCourseDetailsController.$inject = ['$state', '$stateParams', 'preCommissionCourseDetailService', 'notificationService', '$location'];

    function preCommissionCourseDetailsController($state, $stateParams, preCommissionCourseDetailService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.preCommissionCourseId = 0;
        vm.preCommissionCourseDetailId = 0;
        vm.preCommissionCourseDetail = [];
        vm.addPreCommissionCourseDetail = addPreCommissionCourseDetail;
        vm.updatePreCommissionCourseDetail = updatePreCommissionCourseDetail;
        vm.backToPreCommissionCourse = backToPreCommissionCourse;
    
       

        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
            vm.searchText = location.search().q;
        }

        if ($stateParams.preCommissionCourseId !== undefined && $stateParams.preCommissionCourseId !== null) {
            vm.preCommissionCourseId = $stateParams.preCommissionCourseId;
        }
        init();
        function init() {
            preCommissionCourseDetailService.getPreCommissionCourseDetails(vm.preCommissionCourseId, vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.preCommissionCourseDetails = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addPreCommissionCourseDetail() {
            $state.go('employee-tabs.pre-commission-course-detail-create', { preCommissionCourseId: vm.preCommissionCourseId , preCommissionCourseDetailId: vm.preCommissionCourseDetailId });
        }

        function updatePreCommissionCourseDetail(preCommissionCourseDetail) {
            $state.go('employee-tabs.pre-commission-course-detail-modify', { preCommissionCourseId: vm.preCommissionCourseId, preCommissionCourseDetailId: preCommissionCourseDetail.preCommissionCourseDetailId });
        }

        
        function backToPreCommissionCourse() {
            $state.go('employee-tabs.pre-commission-courses');
        }
    }

})();
