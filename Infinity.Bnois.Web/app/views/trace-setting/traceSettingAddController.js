(function () {

    'use strict';

    var controllerId = 'traceSettingAddController';

    angular.module('app').controller(controllerId, traceSettingAddController);
    traceSettingAddController.$inject = ['$stateParams', 'traceSettingService', 'notificationService', '$state'];

    function traceSettingAddController($stateParams, traceSettingService, notificationService, $state) {
        var vm = this;
        vm.traceSettingId = 0;
        vm.title = 'ADD MODE';
        vm.traceSetting = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.traceSettingForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            traceSettingService.getTraceSetting(vm.traceSettingId).then(function (data) {
                vm.traceSetting = data.result;
                if (vm.traceSetting.creationDate != null) {
                    vm.traceSetting.creationDate = new Date(vm.traceSetting.creationDate);
                }
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.traceSettingId !== 0 && vm.traceSettingId !== '') {
                updateTraceSetting();
            } else {
                insertTraceSetting();
            }
        }

        function insertTraceSetting() {
            traceSettingService.saveTraceSetting(vm.traceSetting).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateTraceSetting() {
            traceSettingService.updateTraceSetting(vm.traceSettingId, vm.traceSetting).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('trace-settings');
        }
    }
})();
