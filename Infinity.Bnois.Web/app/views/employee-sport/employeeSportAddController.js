(function () {

    'use strict';
    var controllerId = 'employeeSportAddController';
    angular.module('app').controller(controllerId, employeeSportAddController);
    employeeSportAddController.$inject = ['$stateParams', '$state', 'employeeSportService', 'notificationService'];

    function employeeSportAddController($stateParams, $state, employeeSportService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.employeeSportId = 0;
        vm.employeeSport = {};
        vm.title = 'ADD MODE';
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.employeeSportForm = {};


        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
        }
        if ($stateParams.employeeSportId > 0) {
            vm.employeeSportId = $stateParams.employeeSportId;
            vm.title = 'UPDATE MODE';
            vm.saveButtonText = 'Update';
            
        }
        init();
        function init() {
            employeeSportService.getEmployeeSport(vm.employeeId, vm.employeeSportId).then(function (data) {
                vm.employeeSport = data.result.employeeSport;
                if (vm.employeeSport.dateOfParticipation != null) {
                    vm.employeeSport.dateOfParticipation = new Date(vm.employeeSport.dateOfParticipation);

                }
                vm.sports = data.result.sports;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        
        function save() {
            if (vm.employeeSportId > 0 && vm.employeeSportId !== '') {
                updateEmployeeSport();
            } else {
                insertEmployeeSport();
            }
        }
        function insertEmployeeSport() {
            employeeSportService.saveEmployeeSport(vm.employeeId, vm.employeeSport).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateEmployeeSport() {
            employeeSportService.updateEmployeeSport(vm.employeeSportId, vm.employeeSport).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function close() {
            $state.go('employee-tabs.employee-sports');
        }
    }

})();
