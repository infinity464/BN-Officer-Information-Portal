(function () {

    'use strict';
    var controllerId = 'officeAppointmentStructuresController';
    angular.module('app').controller(controllerId, officeAppointmentStructuresController);
    officeAppointmentStructuresController.$inject = ['$stateParams', '$state','codeValue','$scope','$rootScope','officeService','notificationService'];

    function officeAppointmentStructuresController($stateParams, $state, codeValue, $scope, $rootScope, officeService, notificationService) {
        /* jshint validthis:true */
        var vm = this;

        vm.url = codeValue.API_OFFICER_PICTURE_URL;
        vm.office = {};
        vm.appointedOfficers = [];
        vm.vacantAppointments = [];
        vm.back = back;
        vm.officeCurrentStatus = officeCurrentStatus;

        if ($stateParams.officeName !== null) {
            vm.officeName = $stateParams.officeName;
           
        }

        if ($stateParams.officeId > 0 && $stateParams.officeId !== null) {
            vm.officeId = $stateParams.officeId;

        }


        init();

        function init() {
            officeService.getOfficeAppointmentDetails(vm.officeId).then(function (data) {
                vm.office = data.result.office;
                vm.appointedOfficers = data.result.appointedOfficers;
               
                vm.vacantAppointments = data.result.vacantAppointments;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });


        }

        function back() {

            $state.go('office-structures');
        }

        function officeCurrentStatus(pNo) {

            $state.goNewTab('current-status-tab', { pno: pNo });
        }
    }
})();
