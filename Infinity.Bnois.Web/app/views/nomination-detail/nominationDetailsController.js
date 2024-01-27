

(function () {

    'use strict';
    var controllerId = 'nominationDetailsController';
    angular.module('app').controller(controllerId, nominationDetailsController);
    nominationDetailsController.$inject = ['$state', '$stateParams', 'nominationDetailService', 'backLogService','notificationService', '$location'];

    function nominationDetailsController($state, $stateParams, nominationDetailService, backLogService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.nominationDetails = [];

        vm.nominationDetailId = 0;
        vm.nominationDetail = {};
        vm.nominationDetail.nominationId = 0;
        vm.nominationDetailForm = {};

        vm.save = save;
        vm.close = close;
        vm.back = back;
        vm.saveButtonText = "ADD";

        vm.deleteNominationDetail = deleteNominationDetail;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.nominationDetail.nominationId = $stateParams.id;
        }

        if ($stateParams.title !== undefined && $stateParams.title !== null) {
            vm.title = $stateParams.title;

        }

        vm.type = 0;
        vm.typeName = null;

        if ($stateParams.type !== undefined && $stateParams.type !== null) {
            vm.type = $stateParams.type;
            if (vm.type == 1) {
                vm.typeName = "Course";
            } else if (vm.type == 2) {
                vm.typeName = "Mission";
            }
            else if (vm.type == 3) {
                vm.typeName = "Foreign Visit";
            }
            else {

                vm.typeName = "Other";
            }
        }


        init();
        function init() {
            nominationDetailService.getNominationDetails(vm.nominationDetail.nominationId).then(function (data) {
                vm.nominationDetails = data.result;
                    vm.permission = data.permission;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
            
        }



        function deleteNominationDetail(nominationDetail) {
            nominationDetailService.deleteNominationDetail(nominationDetail.nominationDetailId).then(function (data) {
                init();
                close();
            });
        }



        function save() {
         
            if (vm.nominationDetail.employee.employeeId > 0) {
                vm.nominationDetail.employeeId = vm.nominationDetail.employee.employeeId;
  
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }

            insertNominationDetail(vm.type);

        }

        function insertNominationDetail(type) {

            nominationDetailService.saveNominationDetail(vm.nominationDetail, type).then(function (data) {
                close();


                init();

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            vm.nominationDetail.employee = null;
            vm.nominationDetailId = 0;
  
        }

        function back() {
            $state.go("nominations", { type: vm.type });
        }

        

    }

})();

