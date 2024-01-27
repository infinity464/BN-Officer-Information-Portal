(function () {

    'use strict';

    var controllerId = 'employeeAddController';

    angular.module('app').controller(controllerId, employeeAddController);
    employeeAddController.$inject = ['$stateParams', 'employeeService', 'rankService', 'notificationService', '$state'];

    function employeeAddController($stateParams, employeeService, rankService, notificationService, $state) {
        var vm = this;
        vm.employeeId = 0;
        vm.title = 'ADD MODE';
        vm.employee = {};
        vm.batches = [];
        vm.genders = [];
        vm.ranks = []; 
        vm.rankCategories = [];
        vm.countries = [];
        vm.officerTypes = [];
        vm.employeeStatuses = [];
        vm.executionRemarks = [];        
        vm.getRankByRankCategory = getRankByRankCategory;
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.employeeForm = {};
        vm.countriesName = countriesName;
        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            employeeService.getEmployee(vm.employeeId).then(function (data) {
                vm.employee = data.result.employee; 

                if (vm.employee != null) {
                    countriesName(vm.employee.officerTypeId);
                }
                vm.executionRemarks = data.result.executionRemarks;
                vm.batches = data.result.batches;
                vm.genders = data.result.genders;
                vm.ranks = data.result.ranks;
                vm.rankCategories = data.result.rankCategories;
                vm.countries = data.result.countries;
                vm.officerTypes = data.result.officerTypes;
                vm.employeeStatuses = data.result.employeeStatuses;
                if (vm.employee.bExecutionDate != null) {
                    vm.employee.bExecutionDate = new Date(vm.employee.bExecutionDate);
                }
              
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
           
            if (vm.employeeId !== 0 && vm.employeeId !== '') {
                updateEmployee();
            } else {
                insertEmployee();
            }
        }

        function insertEmployee() {

            employeeService.saveEmployee(vm.employee).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateEmployee() {
            employeeService.updateEmployee(vm.employeeId, vm.employee).then(function (data) {
                $state.go('employee-tabs.employee-modify', { id: vm.employeeId });
                    notificationService.displaySuccess("Updated Successfully.!!!");
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('employees');
        }



        function countriesName(officerType) {
            if (officerType == 3) {
                vm.employee.countryId = null;
                vm.countryDisable = false;
            }
            else {
                vm.employee.countryId = 12;
                vm.countryDisable = true;

            }
        }



        function getRankByRankCategory(rankCategoryId) {
            rankService.getRankByRankCategory(rankCategoryId).then(function (data) {
                vm.ranks = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
    }
})();
