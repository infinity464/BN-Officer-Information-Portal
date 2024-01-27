

(function () {

    'use strict';

    var controllerId = 'dashboardGenderController';

    angular.module('app').controller(controllerId, dashboardGenderController);
    dashboardGenderController.$inject = ['$stateParams', 'dashboardService','genderService','notificationService', '$state'];

    function dashboardGenderController($stateParams, dashboardService, genderService,notificationService, $state) {
        var vm = this;
        
        vm.dashboardGenders = [];
        vm.genders = [];
        vm.getEmployeeListByOffice = getEmployeeListByOffice;
        vm.getOfficerList = getOfficerList;
        vm.genderId = -1;

        Init();
        function Init() {

            genderService.getOfficerGenderSelectModels().then(function(data) {
                vm.genders = data.result;
                getEmployeeListByOffice(-1);

            });

        }

        function getEmployeeListByOffice(genderId) {
            if (genderId == null) {
                genderId = -1;
            }
               
                dashboardService.getDashboardGender(genderId).then(function (data) {
                        vm.dashboardGenders = data.result;

                    },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
          
           
        }

        function getOfficerList(rankId, branch) {
            $state.goNewTab('gender-officer', { rankId: rankId, branch: branch, genderId:vm.genderId });

        }

    }
})();
