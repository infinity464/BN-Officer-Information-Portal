(function () {

    'use strict';

    var controllerId = 'awardAddController';

    angular.module('app').controller(controllerId, awardAddController);
    awardAddController.$inject = ['$stateParams', 'awardService', 'notificationService', '$state'];

    function awardAddController($stateParams, awardService, notificationService, $state) {
        var vm = this;
        vm.awardId = 0;
        vm.title = 'ADD MODE';
        vm.award = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.awardForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.awardId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            awardService.getAward(vm.awardId).then(function (data) {
                vm.award = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.awardId !== 0 && vm.awardId !== '') {
                updateAward();
            } else {
                insertAward();
            }
        }

        function insertAward() {
            awardService.saveAward(vm.award).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateAward() {
            awardService.updateAward(vm.awardId, vm.award).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('awards');
        }
    }
})();
