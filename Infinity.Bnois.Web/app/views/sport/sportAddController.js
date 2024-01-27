(function () {

    'use strict';

    var controllerId = 'sportAddController';

    angular.module('app').controller(controllerId, sportAddController);
    sportAddController.$inject = ['$stateParams', 'sportService', 'notificationService', '$state'];

    function sportAddController($stateParams, sportService, notificationService, $state) {
        var vm = this;
        vm.sportsId = 0;
        vm.title = 'ADD MODE';
        vm.sport = {};
        vm.saveButtonText = 'Save';
        vm.save = save
        vm.close = close;
        vm.sportForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.sportsId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            sportService.getSport(vm.sportsId).then(function (data) {
                vm.sport = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.sportsId !== 0 && vm.sportsId !== '') {
                updateSport();
            } else {
                insertSport();
            }
        }

        function insertSport() {
            sportService.saveSport(vm.sport).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateSport() {
            sportService.updateSport(vm.sportsId, vm.sport).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('sports');
        }
    }
})();
