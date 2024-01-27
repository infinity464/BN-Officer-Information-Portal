(function () {

    'use strict';

    var controllerId = 'certificateAddController';

    angular.module('app').controller(controllerId, certificateAddController);
    certificateAddController.$inject = ['$stateParams', 'certificateService', 'notificationService', '$state'];

    function certificateAddController($stateParams, certificateService, notificationService, $state) {
        var vm = this;
        vm.certificateId = 0;
        vm.title = 'ADD MODE';
        vm.certificate = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.certificateForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.certificateId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            certificateService.getCertificate(vm.certificateId).then(function (data) {
                vm.certificate = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.certificateId !== 0 && vm.certificateId !== '') {
                updateCertificate();
            } else {  
                insertCertificate();
            }
        }

        function insertCertificate() {
            certificateService.saveCertificate(vm.certificate).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateCertificate() {
            certificateService.updateCertificate(vm.certificateId, vm.certificate).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('certificates');
        }
    }
})();
