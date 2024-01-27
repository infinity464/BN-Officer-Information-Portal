

(function () {

    'use strict';

    var controllerId = 'maritalTypeAddController';

    angular.module('app').controller(controllerId, maritalTypeAddController);
    maritalTypeAddController.$inject = ['$stateParams', 'maritalTypeService', 'notificationService', '$state'];

    function maritalTypeAddController($stateParams, maritalTypeService, notificationService, $state) {
        var vm = this;
        vm.maritalTypeId = 0;
        vm.title = 'ADD MODE';
        vm.maritalType = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.maritalTypeTypeForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.maritalTypeId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            maritalTypeService.getMaritalType(vm.maritalTypeId).then(function (data) {
                vm.maritalType = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.maritalTypeId !== 0 && vm.maritalTypeId !== '') {
                updateMaritalType();
            } else {
                insertMaritalType();
            }
        }

        function insertMaritalType() {
            maritalTypeService.saveMaritalType(vm.maritalType).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateMaritalType() {
            maritalTypeService.updateMaritalType(vm.maritalTypeId, vm.maritalType).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('marital-types');
        }

    }
})();
