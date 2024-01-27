(function () {

    'use strict';

    var controllerId = 'employeeServiceExtensionAddController';

    angular.module('app').controller(controllerId, employeeServiceExtensionAddController);
    employeeServiceExtensionAddController.$inject = ['$stateParams', 'employeeServiceExtensionService', 'employeeService', 'notificationService', '$state'];

    function employeeServiceExtensionAddController($stateParams, employeeServiceExtensionService, employeeService, notificationService, $state) {
        var vm = this;
        vm.empSvrExtId = 0;
        vm.title = 'ADD MODE';
        vm.labelName = 'Retirement Date';
        vm.employeeServiceExtension = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.employeeServiceExtensionForm = {};
        vm.searchEmployee = searchEmployee;
        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.empSvrExtId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            employeeServiceExtensionService.getEmployeeServiceExtension(vm.empSvrExtId).then(function (data) {
                vm.employeeServiceExtension = data.result;
                if (vm.employeeServiceExtension.employee.employeeGeneral[0].isContract) {
                    vm.labelName = 'Contract End Date';
                }
                vm.employeeServiceExtension.retirementDate = new Date(vm.employeeServiceExtension.retirementDate);
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.employeeServiceExtension.employee.employeeId > 0) {
                vm.employeeServiceExtension.employeeId = vm.employeeServiceExtension.employee.employeeId;
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }

            if (vm.employeeServiceExtension.employee.employeeStatusId == 2 || vm.employeeServiceExtension.employee.employeeStatusId == 5) {
                notificationService.displayError(" Service Extension Not possible.");
            }
            else {
                if (vm.empSvrExtId !== 0 && vm.empSvrExtId !== '') {
                    updateEmployeeServiceExtension();
                } else {
                    insertEmployeeServiceExtension();
                }
            }

           

        }

        function insertEmployeeServiceExtension() {
            employeeServiceExtensionService.saveEmployeeServiceExtension(vm.employeeServiceExtension).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateEmployeeServiceExtension() {
            employeeServiceExtensionService.updateEmployeeServiceExtension(vm.empSvrExtId, vm.employeeServiceExtension).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function searchEmployee() {
            
            employeeService.getEmployeeByPno(vm.employeeServiceExtension.employee.pNo).then(function (data) {
                vm.employeeServiceExtension.employee = data.result;
                console.log(vm.employeeServiceExtension.employee);
                if (vm.employeeServiceExtension.employee.employeeGeneral[0].isContract) {
                    vm.labelName = 'Contract End Date';
                    vm.employeeServiceExtension.retirementDate = new Date(vm.employeeServiceExtension.employee.employeeGeneral[0].contractEndDate);
                }
                else {
                    vm.employeeServiceExtension.retirementDate = new Date(vm.employeeServiceExtension.employee.employeeGeneral[0].lprDate);
                }
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function close() {
            $state.go('employee-service-extensions');
        }

    }
})();
