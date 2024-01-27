

(function () {

    'use strict';

    var controllerId = 'occupationAddController';

    angular.module('app').controller(controllerId, occupationAddController);
    occupationAddController.$inject = ['$stateParams', 'occupationService', 'notificationService', '$state'];

    function occupationAddController($stateParams, occupationService, notificationService, $state) {
        var vm = this;
        vm.occupationId = 0;
        vm.title = 'ADD MODE';
        vm.occupation = {};
        //vm.modules = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.occupationForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.occupationId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            occupationService.getOccupation(vm.occupationId).then(function (data) {
                vm.occupation = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {

            if (vm.occupationId !== 0) {
                updateOccupation();
            } else {
                insertOccupation();
            }
        }

        function insertOccupation() {
            occupationService.saveOccupation(vm.occupation).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function updateOccupation() {
            occupationService.updateOccupation(vm.occupationId, vm.occupation).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('occupations');
        }

    }
})();
