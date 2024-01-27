

(function () {

    'use strict';

    var controllerId = 'futurePlanController';

    angular.module('app').controller(controllerId, futurePlanController);
    futurePlanController.$inject = ['$stateParams', 'notificationService','currentStatusService', '$state'];

    function futurePlanController($stateParams, notificationService, currentStatusService, $state) {
        var vm = this;
        vm.pNo = 0;
        vm.courseFuturePlans = [];
        vm.transferFuturePlans = [];

        if ($stateParams.pno !== undefined && $stateParams.pno !== null) {
            vm.pNo = $stateParams.pno;
        }

        Init();
        function Init() {
            currentStatusService.getCourseFuturePlan(vm.pNo).then(function (data) {
                vm.courseFuturePlans = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            currentStatusService.getTransferFuturePlan(vm.pNo).then(function (data) {
                vm.transferFuturePlans = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

        }

     
       
    }
})();
