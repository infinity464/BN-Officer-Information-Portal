

(function () {

    'use strict';

    var controllerId = 'medalAwardPublicationController';

    angular.module('app').controller(controllerId, medalAwardPublicationController);
    medalAwardPublicationController.$inject = ['$stateParams','employeeService','currentStatusService', 'notificationService', '$state'];

    function medalAwardPublicationController($stateParams, employeeService, currentStatusService, notificationService, $state) {
        var vm = this;
      
        vm.awards = [];
        vm.medals = [];
        vm.publications = [];
        vm.cleanServices = [];
        vm.intialHide;
        if ($stateParams.pno !== undefined && $stateParams.pno !== null) {
            vm.pNo = $stateParams.pno;
        }

        Init();
        function Init() {
            currentStatusService.getPublication().then(function (data) {
                vm.publications = data.result;
                if (vm.publications.length > 0) {
                    vm.intialHide = false;
                }
                else {
                    vm.intialHide = true;
                }
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
            //employeeService.getEmployeeByPno().then(function (data) {
            //    vm.officerName = data.result.fullNameEng;
            //    vm.pNo = data.result.pNo;
            //    if (vm.pNo != null) {
            //        //currentStatusService.getAward(vm.pNo).then(function (data) {
            //        //    vm.awards = data.result;

            //        //},
            //        //    function (errorMessage) {
            //        //        notificationService.displayError(errorMessage.message);
            //        //    });

            //        //currentStatusService.getMedal(vm.pNo).then(function (data) {
            //        //    vm.medals = data.result;

            //        //},
            //        //    function (errorMessage) {
            //        //        notificationService.displayError(errorMessage.message);
            //        //    });

                    


            //        //currentStatusService.getCleanService(vm.pNo).then(function (data) {
            //        //    vm.cleanServices = data.result;

            //        //},
            //        //    function (errorMessage) {
            //        //        notificationService.displayError(errorMessage.message);
            //        //    });
            //    }

            //    },
            //    function (errorMessage) {
            //        notificationService.displayError(errorMessage.message);
            //    });
        };
    }
})();
