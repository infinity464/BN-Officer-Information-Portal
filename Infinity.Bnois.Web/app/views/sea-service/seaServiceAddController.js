(function () {

    'use strict';

    var controllerId = 'seaServiceAddController';

    angular.module('app').controller(controllerId, seaServiceAddController);
    seaServiceAddController.$inject = ['$stateParams', 'seaServiceService', 'notificationService', '$state'];

    function seaServiceAddController($stateParams, seaServiceService, notificationService, $state) {
        var vm = this;
        vm.seaServiceId = 0;
        vm.title = 'ADD MODE';
        vm.seaService = {};
        vm.countries = [];
        vm.shipTypes = [];
       
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.seaServiceForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.seaServiceId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            seaServiceService.getSeaService(vm.seaServiceId).then(function (data) {
                vm.seaService = data.result.seaService;

                vm.countries = data.result.countries;
                vm.shipTypes = data.result.shipTypes;
                   
                    if (vm.seaServiceId !== 0 && vm.seaServiceId !== '') {

                        if (vm.seaService.fromDate != null) {
                            vm.seaService.fromDate = new Date(data.result.seaService.fromDate);

                        }
                        if (vm.seaService.toDate != null) {
                            vm.seaService.toDate = new Date(data.result.seaService.toDate);

                        }


                    }
           

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.seaService.employee.employeeId > 0) {
                vm.seaService.employeeId = vm.seaService.employee.employeeId;
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }

            if (vm.seaServiceId !== 0 && vm.seaServiceId !== '') {
                updateSeaService();
            } else {
                insertSeaService();
            }
        }

        function insertSeaService() {
            seaServiceService.saveSeaService(vm.seaService).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateSeaService() {
            seaServiceService.updateSeaService(vm.seaServiceId, vm.seaService).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('sea-services');
        }

    }

  

})();
