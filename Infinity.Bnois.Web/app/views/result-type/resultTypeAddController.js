(function () {

    'use strict';

    var controllerId = 'resultTypeAddController';

    angular.module('app').controller(controllerId, resultTypeAddController);
    resultTypeAddController.$inject = ['$stateParams', 'resultTypeService', 'notificationService', '$state'];

    function resultTypeAddController($stateParams, resultTypeService, notificationService, $state) {
        var vm = this;
        vm.resultTypeId = 0;
        vm.title = 'ADD MODE';
        vm.resultType = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.resultTypeForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.resultTypeId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            resultTypeService.getResultType(vm.resultTypeId).then(function (data) {
                vm.resultType = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.resultTypeId !== 0 && vm.resultTypeId !== '') {
                updateResultType();
            } else {
                insertResultType();
            }
        }

        function insertResultType() {
            resultTypeService.saveResultType(vm.resultType).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateResultType() {
            resultTypeService.updateResultType(vm.resultTypeId, vm.resultType).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('result-types');
        }
    }
})();
