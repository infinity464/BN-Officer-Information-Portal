(function () {

    'use strict';

    var controllerId = 'employeePftAddController';

    angular.module('app').controller(controllerId, employeePftAddController);
    employeePftAddController.$inject = ['$stateParams', 'employeePftService','serviceExamService', 'notificationService', '$state'];

    function employeePftAddController($stateParams, employeePftService, serviceExamService ,notificationService, $state) {
        var vm = this;
        vm.employeePftId = 0;
        vm.title = 'ADD MODE';
        vm.employeePft = {};
        vm.pftTypes = [];
        vm.pftResults = [];
  
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.employeePftForm = {};
      

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeePftId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            employeePftService.getEmployeePft(vm.employeePftId).then(function (data) {
                vm.employeePft = data.result.employeePft;

                vm.pftTypes = data.result.pftTypes;
                vm.pftResults = data.result.pftResults;  
                    if (vm.employeePftId !== 0 && vm.employeePftId !== '') {
                       
                        if (vm.employeePft.pftDate != null) {
                            vm.employeePft.pftDate = new Date(data.result.employeePft.pftDate);
                        }
                    }

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.employeePft.employee.employeeId > 0) {
                vm.employeePft.employeeId = vm.employeePft.employee.employeeId;
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }

            if (vm.employeePftId !== 0 && vm.employeePftId !== '') {
                updateEmployeePft();
            } else {
                insertEmployeePft();
            }
        }

        function insertEmployeePft() {
            employeePftService.saveEmployeePft(vm.employeePft).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateEmployeePft() {
            employeePftService.updateEmployeePft(vm.employeePftId, vm.employeePft).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('employee-pfts');
        }


      
    }

  

})();
