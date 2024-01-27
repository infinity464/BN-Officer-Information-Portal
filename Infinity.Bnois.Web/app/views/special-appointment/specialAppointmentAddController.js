(function () {

    'use strict';

    var controllerId = 'specialAppointmentAddController';

    angular.module('app').controller(controllerId, specialAppointmentAddController);
    specialAppointmentAddController.$inject = ['$stateParams', 'specialAppointmentService', 'notificationService', '$state'];

    function specialAppointmentAddController($stateParams, specialAppointmentService, notificationService, $state) {
        var vm = this;
        vm.id = 0;
        vm.title = 'ADD MODE';
        vm.specialAppointment = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.specialAppointmentForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.id = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            specialAppointmentService.getSpecialAppointment(vm.id).then(function (data) {
                vm.specialAppointment = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.id !== 0 && vm.id !== '') {
                updateSpecialAppointment();
            } else {  
                insertSpecialAppointment();
            }
        }

        function insertSpecialAppointment() {
            specialAppointmentService.saveSpecialAppointment(vm.specialAppointment).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateSpecialAppointment() {
            specialAppointmentService.updateSpecialAppointment(vm.id, vm.specialAppointment).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('special-appointments');
        }
    }
})();
