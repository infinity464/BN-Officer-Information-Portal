

(function () {

    'use strict';

    var controllerId = 'commendationAppreciationController';

    angular.module('app').controller(controllerId, commendationAppreciationController);
    commendationAppreciationController.$inject = ['$stateParams','employeeService','currentStatusService', 'notificationService', '$state'];

    function commendationAppreciationController($stateParams, employeeService,currentStatusService, notificationService, $state) {
        var vm = this;
      
        vm.commendations = [];
        vm.appreciations = [];
        vm.notableAchievements = [];


        Init();
        function Init() {
            currentStatusService.getCommendationAppreciation(vm.pNo, 1).then(function (data) {
                vm.commendations = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            currentStatusService.getCommendationAppreciation(vm.pNo, 2).then(function (data) {
                vm.appreciations = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
    }
})();
