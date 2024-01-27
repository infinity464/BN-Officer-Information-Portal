(function () {

    'use strict';

    var controllerId = 'observationIntelligentAddController';

    angular.module('app').controller(controllerId, observationIntelligentAddController);
    observationIntelligentAddController.$inject = ['$stateParams', 'backLogService','employeeService', 'observationIntelligentService', 'notificationService', '$state'];

    function observationIntelligentAddController($stateParams, backLogService, employeeService, observationIntelligentService, notificationService, $state) {
        var vm = this;
        vm.observationIntelligentId = 0;
        vm.title = 'ADD MODE';
        vm.observationIntelligent = {};
        vm.observationIntelligentTypes = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.observationIntelligentForm = {};

        vm.ranks = [];
        vm.transfers = [];
        vm.givenTransfers = [];
        vm.isBackLogChecked = isBackLogChecked;

        vm.givenTransfer = givenTransfer;
        vm.searchEmployee = searchEmployee;


        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.observationIntelligentId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            observationIntelligentService.getObservationIntelligent(vm.observationIntelligentId).then(function (data) {
                vm.observationIntelligent = data.result.observationIntelligent;
                vm.observationIntelligentTypes = data.result.observationIntelligentTypes;


                  

                if (vm.observationIntelligentId !== 0 && vm.observationIntelligentId !== '') {
                    if (vm.observationIntelligent.date != null) {
                        vm.observationIntelligent.date = new Date(data.result.observationIntelligent.date);

                    }                      
                    isBackLogChecked(vm.observationIntelligent.isBackLog);
                    givenTransfer(vm.observationIntelligent.employee1.employeeId);
                  } else {
                        vm.observationIntelligent.type = 1;
 
                    }
           

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.observationIntelligent.employee.employeeId > 0 ) {
                vm.observationIntelligent.employeeId = vm.observationIntelligent.employee.employeeId;
                if (vm.observationIntelligent.employee1 !== null) {
                    vm.observationIntelligent.givenEmployeeId = vm.observationIntelligent.employee1.employeeId;
                }              
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }


            if (vm.observationIntelligentId !== 0 && vm.observationIntelligentId !== '') {
                updateObservationIntelligent();
            } else {
                insertObservationIntelligent();

            }
        }

        function insertObservationIntelligent() {

            observationIntelligentService.saveObservationIntelligent(vm.observationIntelligent).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateObservationIntelligent() {
            observationIntelligentService.updateObservationIntelligent(vm.observationIntelligentId, vm.observationIntelligent).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('observation-intelligent-reports');
        }


        function isBackLogChecked(isBackLog) {
            if (isBackLog) {
                if (vm.observationIntelligent.employee.employeeId > 0) {
                    backLogService.getBackLogSelectModels(vm.observationIntelligent.employee.employeeId).then(function (data) {
                        vm.ranks = data.result.ranks;
                        vm.transfers = data.result.transfers;
                    });
                }

            }
        }



        function searchEmployee(pno) {
            employeeService.getEmployeeByPno(pno).then(function (data) {
                vm.observationIntelligent.employee1 = data.result;
                givenTransfer(vm.observationIntelligent.employee1.employeeId);
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function givenTransfer(employeeId) {
            backLogService.getBackLogSelectModels(employeeId).then(function (data) {
                vm.givenTransfers = data.result.transfers;
            });

        }

       

    }

  

})();
