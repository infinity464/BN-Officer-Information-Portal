/// <reference path="../../services/religionCastService.js" />
(function () {

    'use strict';

    var controllerId = 'religionCastAddController';

    angular.module('app').controller(controllerId, religionCastAddController);
    religionCastAddController.$inject = ['$stateParams', 'religionCastService', 'notificationService', '$state'];

    function religionCastAddController($stateParams, religionCastService, notificationService, $state) {
        var vm = this;
        vm.religionCastId = 0;
        vm.title = 'ADD MODE';
        vm.religionCast = {};
        vm.religions = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.religionCastForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.religionCastId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            religionCastService.getReligionCast(vm.religionCastId).then(function (data) {
                vm.religionCast = data.result.religionCast;
                vm.religions = data.result.religions;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.religionCastId !== 0 && vm.religionCastId !== '') {
                updateReligionCast();
            } else {
                insertReligionCast();
            }
        }

        function insertReligionCast() {
            religionCastService.saveReligionCast(vm.religionCast).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateReligionCast() {
            religionCastService.updateReligionCast(vm.religionCastId, vm.religionCast).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('religion-casts');
        }
    }
})();
