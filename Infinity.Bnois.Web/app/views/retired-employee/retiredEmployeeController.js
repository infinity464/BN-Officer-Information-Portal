(function () {

    'use strict';
    var controllerId = 'retiredEmployeeController';
    angular.module('app').controller(controllerId, retiredEmployeeController);
    retiredEmployeeController.$inject = ['$stateParams', '$state', 'retiredEmployeeService','countryService','certificateService', 'notificationService'];

    function retiredEmployeeController($stateParams, $state, retiredEmployeeService, countryService, certificateService,notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.retiredEmployee = {};
        vm.countryNames = [];
        vm.certificateNames = [];
        vm.retiredEmployee = {};
        vm.title = 'Retired Officer  Information';
        vm.updateRetiredEmployee = updateRetiredEmployee;
        vm.backToRetiredEmployees = backToRetiredEmployees;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
        }

        init();
        function init() {
            retiredEmployeeService.getRetiredEmployee(vm.employeeId).then(function (data) {
                vm.retiredEmployee = data.result.retiredEmployee;

                if (vm.retiredEmployee.countryIds !=null) {
                   
                    for (var i = 0; i < vm.retiredEmployee.countryIds.length; i++) {
                        countryService.getCountry(vm.retiredEmployee.countryIds[i]).then(function (data) {
                            vm.countryNames.push(data.result.fullName);
                            
                        })
                    }
                }
              
                

                if (vm.retiredEmployee.certificateIds !=null) {
                        for (var i = 0; i < vm.retiredEmployee.certificateIds.length; i++) {
                            certificateService.getCertificate(vm.retiredEmployee.certificateIds[i]).then(function (data) {
                                vm.certificateNames.push(data.result.fullName);
                                
                            })
                    }
              
                }    

                  
                if (vm.retiredEmployee.issueDate != null) {
                    vm.retiredEmployee.issueDate = new Date(vm.retiredEmployee.issueDate);
                }
                
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function updateRetiredEmployee() {
            $state.go('retired-employee-modify', { id: vm.employeeId});
        }

        function backToRetiredEmployees() {
            $state.go('retired-employees');
        }
    }

})();
