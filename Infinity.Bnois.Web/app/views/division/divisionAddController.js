(function () {

    'use strict';

    var controllerId = 'divisionAddController';

    angular.module('app').controller(controllerId, divisionAddController);
    divisionAddController.$inject = ['$stateParams', 'divisionService', 'notificationService', '$state'];

    function divisionAddController($stateParams, divisionService, notificationService, $state) {
        var vm = this;
        vm.divisionId = 0;
        vm.title = 'ADD MODE';
        vm.division = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.divisionForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.divisionId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            divisionService.getDivision(vm.divisionId).then(function (data) {
                vm.division = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.divisionId !== 0 && vm.divisionId !== '') {
                updateDivision();
            } else {
                insertDivision();
            }
        }

        function insertDivision() {
            divisionService.saveDivision(vm.division).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateDivision() {
            divisionService.updateDivision(vm.divisionId, vm.division).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('divisions');
        }
    }
})();
