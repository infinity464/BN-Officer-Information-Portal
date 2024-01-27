(function () {

    'use strict';
    var controllerId = 'employeeOtherAddController';
    angular.module('app').controller(controllerId, employeeOtherAddController);
    employeeOtherAddController.$inject = ['$stateParams', '$state', 'employeeOtherService', 'notificationService'];

    function employeeOtherAddController($stateParams, $state, employeeOtherService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.employeeOther = {};
        vm.employeeOtherForm = {};
        vm.save = save;
        vm.close = close;
        vm.saveButtonText = 'Save';
        vm.title = 'ADD MODE';

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
            vm.title = 'UPDATE MODE';
            vm.saveButtonText = 'Update';
        }
        init();
        function init() {
            employeeOtherService.getEmployeeOther(vm.employeeId).then(function (data) {
                vm.employeeOther = data.result.employeeOther;
                    if (vm.employeeOther != null) {
                        if (vm.employeeOther.idIssueDate != null) {
                        vm.employeeOther.idIssueDate = new Date(data.result.employeeOther.idIssueDate);
                        }
                        if (vm.employeeOther.passIssueDate != null) {
                            vm.employeeOther.passIssueDate = new Date(data.result.employeeOther.passIssueDate);

                        }
                        if (vm.employeeOther.serIssueDate != null) {
                            vm.employeeOther.serIssueDate = new Date(data.result.employeeOther.serIssueDate);

                        }
                        if (vm.employeeOther.expiryDate != null) {
                            vm.employeeOther.expiryDate = new Date(data.result.employeeOther.expiryDate);

                        }
                        if (vm.employeeOther.dlIssueDate != null) {
                            vm.employeeOther.dlIssueDate = new Date(data.result.employeeOther.dlIssueDate);

                        }

                        if (vm.employeeOther.dlExpiryDate != null) {
                            vm.employeeOther.dlExpiryDate = new Date(data.result.employeeOther.dlExpiryDate);

                        }
                     
                    }

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function save() {
            updateEmployeeOther();
        }

        function updateEmployeeOther() {
            employeeOtherService.updateEmployeeOther(vm.employeeId, vm.employeeOther).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function close() {
            $state.go('employee-tabs.employee-others');
        }
    }

})();
