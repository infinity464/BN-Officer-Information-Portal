(function () {

    'use strict';

    var controllerId = 'securityClearanceReasonAddController';

    angular.module('app').controller(controllerId, securityClearanceReasonAddController);
    securityClearanceReasonAddController.$inject = ['$stateParams', 'securityClearanceReasonService', 'notificationService', '$state'];

    function securityClearanceReasonAddController($stateParams, securityClearanceReasonService, notificationService, $state) {
        var vm = this;
        vm.securityClearanceReasonId = 0;
        vm.title = 'ADD MODE';
        vm.securityClearanceReason = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.securityClearanceReasonForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.securityClearanceReasonId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            securityClearanceReasonService.getSecurityClearanceReason(vm.securityClearanceReasonId).then(function (data) {
                vm.securityClearanceReason = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.securityClearanceReasonId !== 0 && vm.securityClearanceReasonId !== '') {
                updateSecurityClearanceReason();
            } else {
                insertSecurityClearanceReason();
            }
        }

        function insertSecurityClearanceReason() {
            securityClearanceReasonService.saveSecurityClearanceReason(vm.securityClearanceReason).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateSecurityClearanceReason() {
            securityClearanceReasonService.updateSecurityClearanceReason(vm.securityClearanceReasonId, vm.securityClearanceReason).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('security-clearance-reasons');
        }
    }
})();
