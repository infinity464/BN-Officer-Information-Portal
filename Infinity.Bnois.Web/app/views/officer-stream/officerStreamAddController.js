(function () {

    'use strict';

    var controllerId = 'officerStreamAddController';

    angular.module('app').controller(controllerId, officerStreamAddController);
    officerStreamAddController.$inject = ['$stateParams', 'officerStreamService', 'notificationService', '$state'];

    function officerStreamAddController($stateParams, officerStreamService, notificationService, $state) {
        var vm = this;
        vm.officerStreamId = 0;
        vm.title = 'ADD MODE';
        vm.officerStream = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.officerStreamForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.officerStreamId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            officerStreamService.getOfficerStream(vm.officerStreamId).then(function (data) {
                vm.officerStream = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.officerStreamId !== 0 && vm.officerStreamId !== '') {
                updateOfficerStream();
            } else {  
                insertOfficerStream();
            }
        }

        function insertOfficerStream() {
            officerStreamService.saveOfficerStream(vm.officerStream).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateOfficerStream() {
            officerStreamService.updateOfficerStream(vm.officerStreamId, vm.officerStream).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('officer-streams');
        }
    }
})();
