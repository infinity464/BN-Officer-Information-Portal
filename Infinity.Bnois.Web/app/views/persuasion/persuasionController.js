

(function () {

    'use strict';

    var controllerId = 'persuasionController';

    angular.module('app').controller(controllerId, persuasionController);
    persuasionController.$inject = ['$stateParams', 'notificationService','currentStatusService', '$state'];

    function persuasionController($stateParams, notificationService, currentStatusService, $state) {
        var vm = this;
        vm.pNo = 0;
        vm.persuasions = [];

        if ($stateParams.pno !== undefined && $stateParams.pno !== null) {
            vm.pNo = $stateParams.pno;
        }

        Init();
        function Init() {
            currentStatusService.getPersuasion(vm.pNo).then(function (data) {
                vm.persuasions = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

     
       
    }
})();
