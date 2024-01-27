(function () {

    'use strict';
    var controllerId = 'siblingAddController';
    angular.module('app').controller(controllerId, siblingAddController);
    siblingAddController.$inject = ['$stateParams', '$state', 'siblingService', 'notificationService'];

    function siblingAddController($stateParams, $state, siblingService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.siblingId = 0;
        vm.sibling = {};
        vm.siblingTypes = [];
        vm.occupations = [];
        vm.title = 'ADD MODE';

        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.siblingForm = {};


        

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
        }
        if ($stateParams.siblingId >0) {
            vm.siblingId = $stateParams.siblingId;
            vm.title = 'UPDATE MODE';
            vm.saveButtonText = 'Update';
        }
        init();
        function init() {
            siblingService.getSibling(vm.employeeId, vm.siblingId).then(function (data) {
                vm.sibling = data.result.sibling;
                vm.siblingTypes = data.result.siblingTypes;
                vm.occupations = data.result.occupations;
                if (vm.siblingId > 0) {
                    vm.sibling.dateOfBirth = new Date(data.result.sibling.dateOfBirth);

                } else {
                    vm.sibling.siblingType = 1;
                }

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }



        function save() {
            if (vm.siblingId > 0 && vm.siblingId !== '') {
                updateSibling();
            } else {
                insertSibling();
            }
        }
        function insertSibling() {
            siblingService.saveSibling(vm.employeeId, vm.sibling).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateSibling() {
            siblingService.updateSibling(vm.siblingId, vm.sibling).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('employee-tabs.employee-siblings');
        }


      
    }

})();
