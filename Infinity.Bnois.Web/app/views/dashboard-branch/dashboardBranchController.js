

(function () {

    'use strict';

    var controllerId = 'dashboardBranchController';

    angular.module('app').controller(controllerId, dashboardBranchController);
    dashboardBranchController.$inject = ['$stateParams','dashboardService' ,'officeService','notificationService', '$state'];

    function dashboardBranchController($stateParams, dashboardService, officeService,notificationService, $state) {
        var vm = this;
        
        vm.dashboardBranches = [];
        vm.getOfficerList = getOfficerList;

    

        Init();
        function Init() {

        
            dashboardService.getDashboardBranch().then(function (data) {
                vm.dashboardBranches = data.result;
                    
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

       
        function getOfficerList(rankId, branch) {
            $state.goNewTab('branch-officer', { rankId: rankId, branch: branch });

        }

    }
})();
