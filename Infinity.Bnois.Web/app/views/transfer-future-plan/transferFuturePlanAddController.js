(function () {

    'use strict';

    var controllerId = 'transferFuturePlanAddController';

    angular.module('app').controller(controllerId, transferFuturePlanAddController);
    transferFuturePlanAddController.$inject = ['$stateParams','employeeService', 'transferFuturePlanService','officeAppointmentService',  'notificationService', '$state'];

    function transferFuturePlanAddController($stateParams, employeeService, transferFuturePlanService, officeAppointmentService, notificationService, $state) {
        var vm = this;
        vm.transferFuturePlanId = 0;
        vm.title = 'ADD MODE';
        vm.transferFuturePlan = {};
        
        vm.aptNats = [];
        vm.aptCats = [];
        vm.offices = [];
        vm.patterns = [];
        vm.countries = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.transferFuturePlanForm = {};

        vm.getCategoryByNature = getCategoryByNature;
       
        if ($stateParams.pno !== undefined && $stateParams.pno !== null) {
            vm.pNo = $stateParams.pno;
        }


        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.transferFuturePlanId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            transferFuturePlanService.getTransferFuturePlan(vm.transferFuturePlanId).then(function (data) {
                vm.transferFuturePlan = data.result.transferFuturePlan;
                    employeeService.getEmployeeByPno(vm.pNo).then(function (data) {
                        vm.transferFuturePlan.employeeId = data.result.employeeId;
                        },

                        function (errorMessage) {
                            notificationService.displayError(errorMessage.message);
                        });


                    vm.aptNats = data.result.aptNats;
                    vm.aptCats = data.result.aptCats;  
                    vm.offices = data.result.offices; 
                    vm.countries = data.result.countries; 
                    vm.patterns = data.result.patterns; 

                if (vm.transferFuturePlan.planDate !== null) {
                        vm.transferFuturePlan.planDate = new Date(data.result.transferFuturePlan.planDate);

                    } 
                    if (vm.transferFuturePlan.endDate !== null) {
                        vm.transferFuturePlan.endDate = new Date(data.result.transferFuturePlan.endDate);

                    } 
           

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
           
            if (vm.transferFuturePlanId !== 0 && vm.transferFuturePlanId !== '') {
                updateTransferFuturePlan();
            } else {
                insertTransferFuturePlan();
            }
        }

        function insertTransferFuturePlan() {
            transferFuturePlanService.saveTransferFuturePlan(vm.transferFuturePlan).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateTransferFuturePlan() {
            transferFuturePlanService.updateTransferFuturePlan(vm.transferFuturePlanId, vm.transferFuturePlan).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('current-status-tab.transfer-future-plans');
        }

        function getCategoryByNature(appNatId) {
            officeAppointmentService.getCategoryByNature(appNatId).then(function (data) {
                vm.aptCats = data.result.aptCats;
            });
        }
        
    }

  

})();
