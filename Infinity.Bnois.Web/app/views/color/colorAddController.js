(function () {

    'use strict';

    var controllerId = 'colorAddController';

    angular.module('app').controller(controllerId, colorAddController);
    colorAddController.$inject = ['$stateParams', 'colorService', 'notificationService', '$state'];

    function colorAddController($stateParams, colorService, notificationService, $state) {
        var vm = this;
        vm.colorId = 0;
        vm.title = 'ADD MODE';
        vm.color = {};
        vm.colorTypes = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.colorForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.colorId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            colorService.getColor(vm.colorId).then(function (data) {
                vm.color = data.result.color;
                vm.colorTypes = data.result.colorTypes;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.colorId !== 0 && vm.colorId !== '') {
                updateColor();
            } else {
                insertColor();
            }
        }

        function insertColor() {
            colorService.saveColor(vm.color).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateColor() {
            colorService.updateColor(vm.colorId, vm.color).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('colors');
        }
    }
})();
