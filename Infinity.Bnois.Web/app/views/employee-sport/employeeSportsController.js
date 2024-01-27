(function () {

    'use strict';
    var controllerId = 'employeeSportsController';
    angular.module('app').controller(controllerId, employeeSportsController);
    employeeSportsController.$inject = ['$stateParams', '$state', 'employeeSportService', 'notificationService'];

    function employeeSportsController($stateParams, $state, employeeSportService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.employeeId = 0;
        vm.employeeSportId = 0;
        vm.employeeSports = [];
        vm.title = 'Officer Sports';
        vm.addEmployeeSport = addEmployeeSport;
        vm.updateEmployeeSport = updateEmployeeSport;
        vm.deleteEmployeeSport = deleteEmployeeSport;

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        init();
        function init() {
            employeeSportService.getEmployeeSports(vm.employeeId).then(function (data) {
                vm.employeeSports = data.result;  
                    vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        
        function addEmployeeSport() {
            $state.go('employee-tabs.employee-sport-create', { id: vm.employeeId, employeeSportId: vm.employeeSportId });
        }
        
        function updateEmployeeSport(employeeSport) {
            $state.go('employee-tabs.employee-sport-modify', { id: vm.employeeId, employeeSportId: employeeSport.employeeSportId });
        }


        function deleteEmployeeSport(employeeSport) {
            employeeSportService.deleteEmployeeSport(employeeSport.employeeSportId).then(function (data) {
                employeeSportService.getEmployeeSports(vm.employeeId).then(function (data) {
                    vm.employeeSports = data.result;
                });
                $state.go('employee-tabs.employee-sports');
            });
        }
    }

})();
