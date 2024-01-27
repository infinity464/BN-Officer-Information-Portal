

(function () {

    'use strict';

    var controllerId = 'dashboardLeaveController';

    angular.module('app').controller(controllerId, dashboardLeaveController);
    dashboardLeaveController.$inject = ['$stateParams','dashboardService' ,'notificationService', '$state'];

    function dashboardLeaveController($stateParams, dashboardService,notificationService, $state) {
        var vm = this;
        
        vm.dashboardLeaves = [];
        vm.dashboardExBDLeaves = [];
        vm.getOfficerList = getOfficerList;
        vm.getExBDOfficerList = getExBDOfficerList;
       

        Init();
        function Init() {

           
            dashboardService.getDashboardLeave().then(function (data) {
                vm.dashboardLeaves = data.result;
                    
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });


            dashboardService.getDashboardExBDLeave().then(function (data) {
                    vm.dashboardExBDLeaves = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

        }

        function getOfficerList(leaveType, rankLevel) {
            $state.goNewTab('leave-officer', { leaveType: leaveType, rankLevel: rankLevel,type:1 });

        }

        function getExBDOfficerList( rankLevel) {
            $state.goNewTab('leave-officer', { leaveType: 0, rankLevel: rankLevel,type:2 });

        }

       
    }
})();
