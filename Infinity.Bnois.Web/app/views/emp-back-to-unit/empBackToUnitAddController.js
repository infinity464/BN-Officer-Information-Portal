
(function () {

    'use strict';

    var controllerId = 'empBackToUnitAddController';

    angular.module('app').controller(controllerId, empBackToUnitAddController);
    empBackToUnitAddController.$inject = ['$stateParams', 'empRunMissingService', 'notificationService', '$state'];

    function empBackToUnitAddController($stateParams, empRunMissingService, notificationService, $state) {
        var vm = this;
        vm.empRunMissingId = 0;
        vm.title = 'ADD MODE';
        vm.empBackToUnit = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.empBackToUnitForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.empRunMissingId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            empRunMissingService.getEmpBackToUnit(vm.empRunMissingId).then(function (data) {
                vm.empBackToUnit = data.result.empBackToUnit;
                vm.empBackToUnit.date = new Date(vm.empBackToUnit.date);
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.empRunMissingId !== 0 && vm.empRunMissingId !== '') {
                updateEmpBackToUnit();
            } else {
                insertEmpBackToUnit();
            }
        }


        function insertEmpBackToUnit() {
            empRunMissingService.saveEmpBackToUnit(vm.empBackToUnit).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateEmpBackToUnit() {
            empRunMissingService.updateEmpBackToUnit(vm.empRunMissingId, vm.empBackToUnit).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('emp-back-to-units');
        }
    }
})();
