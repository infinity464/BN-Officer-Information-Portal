

(function () {

    'use strict';

    var controllerId = 'countryAddController';

    angular.module('app').controller(controllerId, countryAddController);
    countryAddController.$inject = ['$stateParams', 'countryService', 'notificationService', '$state'];

    function countryAddController($stateParams, countryService, notificationService, $state) {
        var vm = this;
        vm.countryId = 0;
        vm.title = 'ADD MODE';
        vm.country = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.countryForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.countryId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            countryService.getCountry(vm.countryId).then(function (data) {
                vm.country = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.countryId !== 0) {
                updateCountry();
            } else {
                insertCountry();
            }
        }

        function insertCountry() {
            countryService.saveCountry(vm.country).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateCountry() {
            countryService.updateCountry(vm.countryId, vm.country).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('countries');
        }

    }
})();
