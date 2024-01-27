

(function () {

    'use strict';
    var controllerId = 'oprAppointmentsController';
    angular.module('app').controller(controllerId, oprAppointmentsController);
    oprAppointmentsController.$inject = ['$state','$stateParams','oprAppointmentService', 'notificationService', '$location'];

    function oprAppointmentsController($state, $stateParams, oprAppointmentService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.oprAppointments = [];
        vm.specialAppointmentTypes = [];
        vm.suitabilities = [];

        vm.oprAppointmentId = 0;
        vm.oprAppointment = {};
        vm.oprAppointment.employeeOprId = 0;
        vm.oprAppointmentForm = {};

        vm.save = save;
        vm.close = close;
        vm.back = back;
        vm.saveButtonText = "ADD";


        vm.deleteoprAppointment = deleteoprAppointment;
       
        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.oprAppointment.employeeOprId = $stateParams.id; 
            
        }


        init();
        function init() {

              
            oprAppointmentService.getoprAppointments(vm.oprAppointment.employeeOprId).then(function (data) {
                vm.oprAppointments = data.result;
                console.log(vm.oprAppointments);
                    vm.permission = data.permission;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            oprAppointmentService.getoprAppointment(0).then(function (data) {  
                    vm.specialAppointmentTypes = data.result.specialAppointmentTypes;
                    vm.suitabilities = data.result.suitabilities;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
            
        }



        function deleteoprAppointment(oprAppointment) {
            oprAppointmentService.deleteoprAppointment(oprAppointment.id).then(function (data) {
                init();
                close();
            });
        }
        
        function save() {

              insertoprAppointment();

        }

        function insertoprAppointment() {
           
            oprAppointmentService.saveoprAppointment(vm.oprAppointment).then(function (data) {
                

                init();

                    close();

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            vm.oprAppointment.suitabilityId=0;
            vm.oprAppointment.specialAptTypeId=0;
            vm.oprAppointment.note=null;
           
  
        }

        function back() {
            $state.go("opr-entries");
        }

    }

})();

