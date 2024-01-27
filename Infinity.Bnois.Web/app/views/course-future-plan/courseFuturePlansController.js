

(function () {

    'use strict';
    var controllerId = 'courseFuturePlansController';
    angular.module('app').controller(controllerId, courseFuturePlansController);
    courseFuturePlansController.$inject = ['$state','$stateParams', 'courseFuturePlanService', 'notificationService', '$location'];

    function courseFuturePlansController($state, $stateParams, courseFuturePlanService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.courseFuturePlans = [];
        vm.addCourseFuturePlan = addCourseFuturePlan;
        vm.updateCourseFuturePlan = updateCourseFuturePlan;
        vm.deleteCourseFuturePlan = deleteCourseFuturePlan;
        vm.searchText = "";



        if ($stateParams.pno !== undefined && $stateParams.pno !== null) {
            vm.pNo = $stateParams.pno;
        }
        init();
        function init() {
            courseFuturePlanService.getCourseFuturePlans(vm.pNo).then(function (data) {
                vm.courseFuturePlans = data.result;
                    vm.permission = data.permission;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addCourseFuturePlan() {
            $state.go('current-status-tab.course-future-plan-create');
        }

        function updateCourseFuturePlan(courseFuturePlan) {
            
            $state.go('current-status-tab.course-future-plan-modify', { id: courseFuturePlan.employeeCoursePlanId });
        }

        function deleteCourseFuturePlan(courseFuturePlan) {

            courseFuturePlanService.deleteCourseFuturePlan(courseFuturePlan.employeeCoursePlanId).then(function (data) {
                courseFuturePlanService.getCourseFuturePlans(vm.pNo).then(function(data) {
                    vm.courseFuturePlans = data.result;
                });
            });
        }

      
    }

})();

