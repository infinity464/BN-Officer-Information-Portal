/// <reference path="../../services/employeeSecurityClearanceService.js" />
(function () {

    'use strict';

    var controllerId = 'employeeSecurityClearanceAddController';

    angular.module('app').controller(controllerId, employeeSecurityClearanceAddController);
    employeeSecurityClearanceAddController.$inject = ['$stateParams', 'employeeSecurityClearanceService', 'backLogService','notificationService', '$state'];

    function employeeSecurityClearanceAddController($stateParams, employeeSecurityClearanceService, backLogService, notificationService, $state) {
        var vm = this;
        vm.employeeSecurityClearanceId = 0;
        vm.title = 'ADD MODE';
        vm.employeeSecurityClearance = {};
        vm.securityClearanceReasons = [];
        vm.ranks = [];
        vm.transfers = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.employeeSecurityClearanceForm = {};
        vm.isBackLogChecked = isBackLogChecked;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeSecurityClearanceId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            employeeSecurityClearanceService.getEmployeeSecurityClearance(vm.employeeSecurityClearanceId).then(function (data) {
                vm.employeeSecurityClearance = data.result.employeeSecurityClearance;
               
                if (vm.employeeSecurityClearanceId !== 0 && vm.employeeSecurityClearanceId !== '') {
                    isBackLogChecked(vm.employeeSecurityClearance.isBackLog);
                    vm.employeeSecurityClearance.clearanceDate = new Date(data.result.employeeSecurityClearance.clearanceDate);
                    if (vm.employeeSecurityClearance.expirydate != null) {
                        vm.employeeSecurityClearance.expirydate = new Date(data.result.employeeSecurityClearance.expirydate);
                    }
                }
                vm.securityClearanceReasons = data.result.securityClearanceReasons;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.employeeSecurityClearance.employee.employeeId > 0) {
                vm.employeeSecurityClearance.employeeId = vm.employeeSecurityClearance.employee.employeeId;
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }

            if (vm.employeeSecurityClearanceId !== 0 && vm.employeeSecurityClearanceId !== '') {
                updateEmployeeSecurityClearance();
            } else {
                insertEmployeeSecurityClearance();
            }
        }

        function insertEmployeeSecurityClearance() {
            employeeSecurityClearanceService.saveEmployeeSecurityClearance(vm.employeeSecurityClearance).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateEmployeeSecurityClearance() {
            employeeSecurityClearanceService.updateEmployeeSecurityClearance(vm.employeeSecurityClearanceId, vm.employeeSecurityClearance).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('employee-security-clearances');
        }

        function isBackLogChecked(isBackLog) {
            if (isBackLog) {
                if (vm.employeeSecurityClearance.employee.employeeId > 0) {
                    backLogService.getBackLogSelectModels(vm.employeeSecurityClearance.employee.employeeId).then(function (data) {
                        vm.ranks = data.result.ranks;
                        vm.transfers = data.result.transfers;
                    });
                }
            }
        }
    }
})();
