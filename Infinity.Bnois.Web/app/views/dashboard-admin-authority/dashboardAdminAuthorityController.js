

(function () {

    'use strict';

    var controllerId = 'dashboardAdminAuthorityController';

    angular.module('app').controller(controllerId, dashboardAdminAuthorityController);
    dashboardAdminAuthorityController.$inject = ['$stateParams','dashboardService' ,'officeService','notificationService', '$state'];

    function dashboardAdminAuthorityController($stateParams, dashboardService, officeService,notificationService, $state) {
        var vm = this;
        
        vm.dashboardAdminAuthorities = [];
        vm.dashboardInsideNavyOrganizations = [];
        vm.dashboardBCGOrganizations = [];
        vm.getOfficerList = getOfficerList;
        vm.getInsideNavyOfficerList = getInsideNavyOfficerList;
        vm.getBCGOfficerList = getBCGOfficerList;
    

        Init();
        function Init() {

       
            dashboardService.getDashboardAdminAuthority().then(function (data) {
                vm.dashboardAdminAuthorities = data.result;


                  

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            dashboardService.getDashboardInsideNavyOrganization().then(function (data) {
                vm.dashboardInsideNavyOrganizations = data.result;



            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            dashboardService.getDashboardBCGOrganization().then(function (data) {
                vm.dashboardBCGOrganizations = data.result;

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
           
        }

        function getOfficerList(officeId, rankLevel) {
            $state.goNewTab('admin-authority-officer', { officeId: officeId, rankLevel: rankLevel,type:1 });

        }

        function getInsideNavyOfficerList(officeId, rankLevel) {
            $state.goNewTab('admin-authority-officer', { officeId: officeId, rankLevel: rankLevel, type: 2 });

        }

        function getBCGOfficerList(officeId, rankLevel) {
            $state.goNewTab('admin-authority-officer', { officeId: officeId, rankLevel: rankLevel, type: 3 });

        }

       
    }
})();
