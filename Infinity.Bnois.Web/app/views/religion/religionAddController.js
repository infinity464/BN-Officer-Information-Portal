

(function () {

    'use strict';

    var controllerId = 'religionAddController';

    angular.module('app').controller(controllerId, religionAddController);
    religionAddController.$inject = ['$stateParams', 'religionService', 'notificationService', '$state'];

    function religionAddController($stateParams, religionService, notificationService, $state) {
        var vm = this;
        vm.religionId = 0;
        vm.title = 'ADD MODE';
        vm.religion = {};
        //vm.modules = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.religionForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.religionId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            religionService.getReligion(vm.religionId).then(function (data) {
                vm.religion = data.result;
                //vm.modules = data.result.modules;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {

            if (vm.religionId !== 0) {
                updateFeature();
            } else {
                insertFeature();
            }
        }

        function insertFeature() {
            religionService.saveReligion(vm.religion).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function updateFeature() {
            religionService.updateReligion(vm.religionId, vm.religion).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('religions');
        }

    }
})();
