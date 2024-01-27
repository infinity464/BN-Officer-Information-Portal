(function () {

    'use strict';

    var controllerId = 'employeeDollarSignAddController';

    angular.module('app').controller(controllerId, employeeDollarSignAddController);
    employeeDollarSignAddController.$inject = ['$stateParams', 'employeeService', 'notificationService', '$state'];

    function employeeDollarSignAddController($stateParams, employeeService, notificationService, $state) {
        var vm = this;
        vm.employeeId = 0;
        vm.title = 'ADD MODE';
        vm.employee = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.employeeForm = {};

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            employeeService.getEmployeeByDollarSign(vm.employeeId).then(function (data) {
                vm.employee = data.result.employee;
                if (vm.employee.dateOfDollarSign != null) {
                    vm.employee.dateOfDollarSign = new Date(vm.employee.dateOfDollarSign);
                }
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            updateEmployeeDollarSign();

        }


        function updateEmployeeDollarSign() {
            employeeService.updateEmployeeDollarSign(vm.employee).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('employee-dollar-signs');
        }
    }
})();
