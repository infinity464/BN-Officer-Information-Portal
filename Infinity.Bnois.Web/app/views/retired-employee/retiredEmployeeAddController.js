(function () {

    'use strict';
    var controllerId = 'retiredEmployeeAddController';
    angular.module('app').controller(controllerId, retiredEmployeeAddController);
    retiredEmployeeAddController.$inject = ['$stateParams', '$state', 'retiredEmployeeService', 'notificationService'];

    function retiredEmployeeAddController($stateParams, $state, retiredEmployeeService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.retiredEmployeeId = 0;
        vm.isMarried = false;
        vm.retiredEmployee = {};
        vm.countries = [];
        vm.certificates = [];
        vm.employeeId = 0;
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
            retiredEmployeeService.getRetiredEmployee(vm.employeeId).then(function (data) {
                vm.retiredEmployee = data.result.retiredEmployee;
                vm.countries = data.result.countries;
                vm.certificates = data.result.certificates;


                if (vm.retiredEmployee.issueDate != null) {
                    vm.retiredEmployee.issueDate = new Date(vm.retiredEmployee.issueDate);
                }
                
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function save() {
            updateRetiredEmployee();
        }

        function updateRetiredEmployee() {
            retiredEmployeeService.updateRetiredEmployee(vm.employeeId, vm.retiredEmployee).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function close() {
            $state.go('retired-employee', { id: vm.employeeId });
        }


        vm.countryOptions = {
            placeholder: "Select Country...",
            dataTextField: "text",
            dataValueField: "value",
            valuePrimitive: true,
            autoBind: false,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(vm.countries);
                    }
                }
            }
        };


        vm.certificateOptions = {
            placeholder: "Select Certificate...",
            dataTextField: "text",
            dataValueField: "value",
            valuePrimitive: true,
            autoBind: false,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(vm.certificates);
                    }
                }
            }
        };

    }

})();
