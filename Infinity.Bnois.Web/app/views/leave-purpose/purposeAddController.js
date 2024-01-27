(function () {

    'use strict';

    var controllerId = 'purposeAddController';

	angular.module('app').controller(controllerId, purposeAddController);
	purposeAddController.$inject = ['$stateParams', 'purposeService', 'notificationService', '$state'];

	function purposeAddController($stateParams, purposeService, notificationService, $state) {
        var vm = this;
        vm.purposeId = 0;
        vm.title = 'ADD MODE';
        vm.purpose = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.purposeForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
			vm.purposeId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        init();
        function init() {
			purposeService.getPurpose(vm.purposeId).then(function (data) {
				vm.purpose = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
			if (vm.purposeId !== 0 && vm.purposeId !== '') {
                updatePurpose();
            } else {
                insertPurpose();
            }
        }

		function insertPurpose() {
			purposeService.savePurpose(vm.purpose).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
		function updatePurpose() {
			purposeService.updatePurpose(vm.purposeId, vm.purpose).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('purposes');
        }
    }
})();
