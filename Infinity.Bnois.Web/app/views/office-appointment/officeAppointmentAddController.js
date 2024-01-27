(function () {

    'use strict';

    var controllerId = 'officeAppointmentAddController';

    angular.module('app').controller(controllerId, officeAppointmentAddController);
    officeAppointmentAddController.$inject = ['$stateParams', '$rootScope','officeAppointmentService', 'notificationService', '$state'];

    function officeAppointmentAddController($stateParams, $rootScope, officeAppointmentService, notificationService, $state) {
        var vm = this;
        vm.officeId = 0;
        vm.officeAppointmentId = 0;
        vm.title = 'Office Appointment Add';
        vm.officeAppointment = {};
        vm.aptNats = [];
        vm.aptCats = [];
        vm.parentAppointments = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.officeAppointmentForm = {};
        vm.getCategoryByNature = getCategoryByNature;


        if ($stateParams.appointmentId > 0 && $stateParams.appointmentId !== null) {
            vm.officeAppointmentId = $stateParams.appointmentId;
            vm.saveButtonText = 'Update';
            vm.title = 'Office Appointment Update';
        }

        if ($stateParams.id > 0 && $stateParams.id !== null) {
            vm.officeId = $stateParams.id;
        }
        Init();
        function Init() {
            officeAppointmentService.getOfficeAppointment(vm.officeAppointmentId, vm.officeId).then(function (data) {
                vm.officeAppointment = data.result.officeAppointment;
               // vm.officeAppointment.rankIds = [1, 2, 3, 5, 6];
                vm.aptNats = data.result.aptNats;
                vm.aptCats = data.result.aptCats;
                vm.parentAppointments = data.result.parentAppointments;
               
                vm.rankList = data.result.rankList;
                vm.branchList = data.result.branchList;
               
               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.officeAppointmentId !== 0 && vm.officeAppointmentId !== '') {

                updateOfficeAppointment();
            } else {
                insertOfficeAppointment();
            }
        }

        function insertOfficeAppointment() {
            vm.officeAppointment.officeId = $rootScope.officeId;
            officeAppointmentService.saveOfficeAppointment(vm.officeAppointment).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateOfficeAppointment() {
            officeAppointmentService.updateOfficeAppointment(vm.officeAppointmentId, vm.officeAppointment).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('office-tabs.office-appointments', { id: $rootScope.officeId});
        }



        function getCategoryByNature(appNatId) {
            officeAppointmentService.getCategoryByNature(appNatId).then(function (data) {
                vm.aptCats = data.result.aptCats;
            });
        }


    }
})();
