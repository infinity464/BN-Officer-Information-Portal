(function () {

    'use strict';
    var controllerId = 'spouseAddController';
    angular.module('app').controller(controllerId, spouseAddController);
    spouseAddController.$inject = ['$stateParams', '$state', 'spouseService', 'notificationService'];

    function spouseAddController($stateParams, $state, spouseService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.spouseId = 0;
        vm.spouse = {};
        vm.relationTypes = [];
        vm.occupations = [];
        vm.currentStatus = [];
       
        vm.rankCategories = [];
        vm.title = 'ADD MODE';
        vm.deadDisabled = true;

        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.spouseForm = {};
        vm.deadInformation = deadInformation;


        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
        }
        if ($stateParams.spouseId > 0) {
            vm.spouseId = $stateParams.spouseId;
            vm.title = 'UPDATE MODE';
            vm.saveButtonText = 'Update';
        }
        init();

        function init() {
            spouseService.getSpouse(vm.employeeId, vm.spouseId).then(function (data) {
                vm.spouse = data.result.spouse;
                vm.currentStatus = data.result.currentStatus;
                vm.relationTypes = data.result.relationTypes;
                vm.occupations = data.result.occupations;
                vm.rankCategories = data.result.rankCategories;
                if (vm.spouseId > 0) {

                    
                   

                    if (vm.spouse.deadDate != null) {
                        vm.spouse.deadDate = new Date(data.result.spouse.deadDate);
                    }

                    if (vm.spouse.dateofBirth != null) {
                        vm.spouse.dateofBirth = new Date(data.result.spouse.dateofBirth);
                    }
                    if (vm.spouse.marriageDate != null) {
                        vm.spouse.marriageDate = new Date(data.result.spouse.marriageDate);
                    }
                    

                    if (vm.spouse.currentStatus === 2) {
                        vm.deadDisabled = false;

                    }
                } else {
                    vm.spouse.currentStatus = 1;
                }

                

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function save() {
            if (vm.spouseId > 0 && vm.spouseId !== '') {
                updateSpouse();
            } else {
                insertSpouse();
            }
        }

        function insertSpouse() {
            spouseService.saveSpouse(vm.employeeId, vm.spouse).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function updateSpouse() {
            spouseService.updateSpouse(vm.spouseId, vm.spouse).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('employee-tabs.employee-spouses');
        }


        function deadInformation(currentStatus) {

            if (currentStatus == 2) {

                vm.deadDisabled = false;
            } else {
                vm.deadDisabled = true;
            }
        }
    }

})();
