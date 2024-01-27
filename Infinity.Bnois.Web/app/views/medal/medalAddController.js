(function () {

    'use strict';

    var controllerId = 'medalAddController';

    angular.module('app').controller(controllerId, medalAddController);
    medalAddController.$inject = ['$stateParams', 'medalService', 'notificationService', '$state'];

    function medalAddController($stateParams, medalService, notificationService, $state) {
        var vm = this;
        vm.medalId = 0;
        vm.title = 'ADD MODE';
        vm.medal = {};
        vm.medalTypes = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.medalForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.medalId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            medalService.getMedal(vm.medalId).then(function (data) {
                vm.medal = data.result.medal;
                vm.medalTypes = data.result.medalTypes;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.medalId !== 0 && vm.medalId !== '') {
                updateMedal();
            } else {
                insertMedal();
            }
        }

        function insertMedal() {
            medalService.saveMedal(vm.medal).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateMedal() {
            medalService.updateMedal(vm.medalId, vm.medal).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('medals');
        }
    }
})();
