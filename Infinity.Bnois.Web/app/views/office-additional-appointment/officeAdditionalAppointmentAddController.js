(function () {

    'use strict';

    var controllerId = 'officeAdditionalAppointmentAddController';

    angular.module('app').controller(controllerId, officeAdditionalAppointmentAddController);
    officeAdditionalAppointmentAddController.$inject = ['$stateParams', '$rootScope','officeAppointmentService', 'notificationService', '$state'];

    function officeAdditionalAppointmentAddController($stateParams, $rootScope, officeAppointmentService, notificationService, $state) {
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
        vm.setAppointmentName = setAppointmentName;
   


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
                vm.aptNats = data.result.aptNats;
                vm.aptCats = data.result.aptCats;

                    setAppointmentName(vm.officeAppointment.isAdditional);

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.officeAppointmentId !== 0 && vm.officeAppointmentId !== '') {

                updateOfficeAdditionalAppointment();
            } else {
                saveOfficeAdditionalAppointment();
            }
        }

        function saveOfficeAdditionalAppointment() {
            vm.officeAppointment.officeId = $rootScope.officeId;
            officeAppointmentService.saveOfficeAdditionalAppointment(vm.officeAppointment).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateOfficeAdditionalAppointment() {
            officeAppointmentService.updateOfficeAdditionalAppointment(vm.officeAppointmentId, vm.officeAppointment).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('office-tabs.office-additional-appointments', { id: $rootScope.officeId});
        }



        function getCategoryByNature(appNatId) {
            officeAppointmentService.getCategoryByNature(appNatId).then(function (data) {
                vm.aptCats = data.result.aptCats;
            });
        }
        function setAppointmentName(result) {
            
            if (result) {
                vm.officeAppointment.name = "Additional For Retirement";
                vm.officeAppointment.shortName = "Addl(R)";
            } else {
                vm.officeAppointment.name = "Additional";
                vm.officeAppointment.shortName = "Addl";
            }
        }


    }
})();
