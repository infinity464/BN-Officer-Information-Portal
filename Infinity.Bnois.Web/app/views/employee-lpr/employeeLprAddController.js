
(function () {

    'use strict';

    var controllerId = 'employeeLprAddController';

    angular.module('app').controller(controllerId, employeeLprAddController);
    employeeLprAddController.$inject = ['$stateParams', 'employeeLprService', 'employeeService', 'notificationService', '$state'];

    function employeeLprAddController($stateParams, employeeLprService, employeeService, notificationService, $state) {  
        const MONTH = "1";
        const DAY = "2";

        var vm = this;

        vm.Retired = 2
        vm.LPR = 6;
        vm.Termination = 7;
       

        vm.empLprId = 0;
        vm.title = 'ADD MODE';
        vm.employeeLpr = {};
        vm.terminationType = [];
        vm.retirementType = [];
        vm.currentStatusType = [{ value: 6, text: "LPR" }, { value: 2, text: "Retired" }, { value: 7, text: "Terminated" }];
        vm.durationType = [];
        vm.getRetiredDate = getRetiredDate;
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.searchEmployee = searchEmployee;
        vm.employeeLPRForm = {};
       

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.empLprId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            employeeLprService.getEmployeeLpr(vm.empLprId).then(function (data) {
                vm.employeeLpr = data.result.employeeLpr;
                if (vm.employeeLpr.lprDate!=null) {
                    vm.employeeLpr.lprDate = new Date(vm.employeeLpr.lprDate);
                }
                if (vm.employeeLpr.retireDate != null) {
                    vm.employeeLpr.retireDate = new Date(vm.employeeLpr.retireDate);
                }
                if (vm.employeeLpr.terminationDate != null) {
                    vm.employeeLpr.terminationDate = new Date(vm.employeeLpr.terminationDate);
                }
                vm.terminationType = data.result.terminationType;
                vm.retirementType = data.result.retirementType;
                vm.durationType = data.result.durationType;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.empLprId !== 0 && vm.empLprId !== '') {
                updateEmployeeLPR();
            } else {
                insertEmployeeLPR();
            }
        }

        function insertEmployeeLPR() {
            employeeLprService.saveEmployeeLpr(vm.employeeLpr).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateEmployeeLPR() {
            employeeLprService.updateEmployeeLpr(vm.empLprId, vm.employeeLpr).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('employeelpr');
        }



        function searchEmployee() {
            employeeService.getEmployeeByPno(vm.employeeLpr.employee.pNo).then(function (data) {
                vm.employeeLpr.employee = data.result;
                vm.employeeLpr.lprDate = new Date(vm.employeeLpr.employee.employeeGeneral[0].lprDate);
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function getRetiredDate(durationday, durationmonth) {
            var lprDate = new Date(vm.employeeLpr.lprDate);
            if (durationday != '') {
                vm.employeeLpr.retireDate = new Date(lprDate.setDate(lprDate.getDate() + parseInt(durationday)));
            }
            if (durationmonth != '') {
                vm.employeeLpr.retireDate = new Date(lprDate.setMonth(lprDate.getMonth() + parseInt(durationmonth)));          
            }
            else {
                vm.employeeLpr.retireDate = lprDate;
            }
        }

        //function clearDuration() {
        //    vm.employeeLpr.duration="";
        //    vm.employeeLpr.duration="";
        //}
    }
})();
