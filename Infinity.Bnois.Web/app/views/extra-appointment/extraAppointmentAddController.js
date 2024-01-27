(function () {

    'use strict';

    var controllerId = 'extraAppointmentAddController';

    angular.module('app').controller(controllerId, extraAppointmentAddController);
    extraAppointmentAddController.$inject = ['$stateParams','$scope', 'extraAppointmentService', 'officeAppointmentService' ,'notificationService', '$state'];

    function extraAppointmentAddController($stateParams, $scope, extraAppointmentService, officeAppointmentService,notificationService, $state) {
        var vm = this;
        vm.extraAppointmentId = 0;
        vm.title = 'ADD MODE';
        vm.extraAppointment = {};
        vm.offices = [];
        vm.appointments = [];

       
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.extraAppointmentForm = {};
        vm.getExtraAppointmentByOffice = getExtraAppointmentByOffice;
        vm.localSearch = localSearch;
        vm.selected = selected;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.extraAppointmentId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            extraAppointmentService.getExtraAppointment(vm.extraAppointmentId).then(function (data) {
                vm.extraAppointment = data.result.extraAppointment;
                    vm.offices = data.result.offices;
                vm.appointments = data.result.appointments;
                   
 
                if (vm.extraAppointmentId !== 0 && vm.extraAppointmentId !== '') {
                    if (vm.extraAppointment.assignDate != null) {
                        vm.extraAppointment.assignDate = new Date(data.result.extraAppointment.assignDate);

                    }

                    if (vm.extraAppointment.endDate != null) {
                        vm.extraAppointment.endDate = new Date(data.result.extraAppointment.endDate);

                    }

                    changeInputValue();
                } 
           

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.extraAppointment.employee.employeeId > 0) {
                vm.extraAppointment.employeeId = vm.extraAppointment.employee.employeeId;
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }

            if (vm.extraAppointmentId !== 0 && vm.extraAppointmentId !== '') {
                updateExtraAppointment();
            } else {
                insertExtraAppointment();
            }
        }

        function insertExtraAppointment() {
            extraAppointmentService.saveExtraAppointment(vm.extraAppointment).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateExtraAppointment() {
            extraAppointmentService.updateExtraAppointment(vm.extraAppointmentId, vm.extraAppointment).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('extra-appointments');
        }



        function localSearch(str) {
            var matches = [];
            vm.offices.forEach(function (transfer) {

                if ((transfer.text.toLowerCase().indexOf(str.toString().toLowerCase()) >= 0)) {
                    matches.push(transfer);

                }
            });
            return matches;
        }


        function selected(object) {
            
            vm.extraAppointment.officeId = object.originalObject.value;
            getExtraAppointmentByOffice(vm.extraAppointment.officeId);
        }


        function changeInputValue() {
            if (vm.extraAppointmentId > 0) {
               
                var obj = { value: vm.extraAppointment.office.officeId, text: vm.extraAppointment.office.shortName };
                $scope.$broadcast('angucomplete-alt:changeInput', 'officeId', obj);
            }
        }


        function getExtraAppointmentByOffice(officeId) {
            officeAppointmentService.getOfficeAppointmentByOffice(officeId).then(function (data) {
                vm.appointments = data.result;
            });
        }

     
      
    }

  

})();
