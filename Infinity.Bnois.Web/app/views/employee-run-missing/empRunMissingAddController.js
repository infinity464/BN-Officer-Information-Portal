
(function () {

    'use strict';

    var controllerId = 'empRunMissingAddController';

    angular.module('app').controller(controllerId, empRunMissingAddController);
    empRunMissingAddController.$inject = ['$stateParams', 'empRunMissingService', 'notificationService', '$state'];

    function empRunMissingAddController($stateParams, empRunMissingService, notificationService, $state) {
        var vm = this;
        vm.empRunMissingId = 0;
        vm.title = 'ADD MODE';
        vm.empRunMissing = {};
        vm.statusTypes = [{ value: 3, text: "Run" }, { value: 4, text: "Missing" }, { value: 9, text: "Return from Run/Missing" }];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.empRunMissingForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.empRunMissingId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            empRunMissingService.getEmpRunMissing(vm.empRunMissingId).then(function (data) {
                vm.empRunMissing = data.result.empRunMissing;
                vm.empRunMissing.date = new Date(data.result.empRunMissing.date);
                //vm.statusTypes = data.result.statusTypes;
                if (vm.empRunMissingId === 0) {
                    vm.empRunMissing.type = "Run";
                }
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.empRunMissing.employee.employeeId > 0) {
                vm.empRunMissing.employeeId = vm.empRunMissing.employee.employeeId;
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }
            if (vm.empRunMissingId !== 0 && vm.empRunMissingId !== '') {
                updateEmpRunMissing();
            } else {
                insertEmpRunMissing();
            }
        }

        function insertEmpRunMissing() {
            empRunMissingService.saveEmpRunMissing(vm.empRunMissing).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateEmpRunMissing() {
            empRunMissingService.updateEmpRunMissing(vm.empRunMissingId, vm.empRunMissing).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('employee-run-missings');
        }
    }
})();
