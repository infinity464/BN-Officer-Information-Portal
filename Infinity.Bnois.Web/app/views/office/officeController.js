(function () {

    'use strict';

    var controllerId = 'officeController';

    angular.module('app').controller(controllerId, officeController);
    officeController.$inject = ['$state', '$stateParams','$rootScope','officeService', 'notificationService'];

    function officeController($state, $stateParams,$rootScope,officeService, notificationService) {
        var vm = this;
        vm.officeId = 0;
        vm.title = 'Office';
        $rootScope.office = {};
        vm.countries = [];
        vm.zones = [];
        vm.shipTypes = [];
        vm.shipCategories = [];
        vm.patterns = [];
        vm.adminAuthorities = [];
        vm.objectives = [];
        vm.paretOffices = [];
        vm.bornOffices = [];
        $rootScope.saveButtonText = 'Save';
        vm.save = save;
        vm.cancel = cancel;
        vm.officeForm = {};
        vm.offices = [];



        Init();
        function Init() {
            if ($rootScope.officeId !== 0 && $rootScope.officeId !== '') {
                $rootScope.saveButtonText = 'Update';
            }
            officeService.getOffice($rootScope.officeId).then(function (data) {
                $rootScope.office = data.result.office;
                if ($rootScope.office != null) {
                    if ($rootScope.office.commDate !=null) {
                        $rootScope.office.commDate = new Date(data.result.office.commDate);
                    }
                    if ($rootScope.office.deComDate != null) {
                        $rootScope.office.deComDate = new Date(data.result.office.deComDate);
                    }
                    if ($rootScope.office.seServStartDate != null) {
                        $rootScope.office.seServStartDate = new Date(data.result.office.seServStartDate);
                    }
                    if ($rootScope.office.seServEndDate != null) {
                        $rootScope.office.seServEndDate = new Date(data.result.office.seServEndDate);
                    }
                    if ($rootScope.office.commandServStartDate != null) {
                        $rootScope.office.commandServStartDate = new Date(data.result.office.commandServStartDate);
                    }
                    if ($rootScope.office.commandServEndDate != null) {
                        $rootScope.office.commandServEndDate = new Date(data.result.office.commandServEndDate);
                    }
                }

                vm.adminAuthorities = data.result.adminAuthorities;
                vm.patterns = data.result.patterns;
                vm.shipCategories = data.result.shipCategories;
                vm.shipTypes = data.result.shipTypes;
                vm.zones = data.result.zones;
                vm.countries = data.result.countries;
                vm.objectives = data.result.objectives;
                vm.bornOffices = data.result.bornOffices;
                vm.parentOffices = data.result.parentOffices;
                    //vm.office.parentId = vm.officeId;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if ($rootScope.officeId !== 0 && $rootScope.officeId !== '') {
                
                updateOffice();
            } else {
                insertOffice();
            }
        }


        function insertOffice() {
            officeService.saveOffice($rootScope.office).then(function (data) {
                    notificationService.displaySuccess("Office added Successfully!!");
                   
                

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateOffice() {
            officeService.updateOffice($rootScope.officeId, $rootScope.office).then(function (data) {
                    notificationService.displaySuccess("Office updated Successfully!!");
                
                

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function cancel() {
            $rootScope.officeId = 0;
            $rootScope.office = null;
            $state.go('office-tabs.office-general', { id: $rootScope.officeId });
           


        }



    }
})();
