

(function () {

	'use strict';

	var controllerId = 'zoneAddController';

	angular.module('app').controller(controllerId, zoneAddController);
    zoneAddController.$inject = ['$stateParams', 'zoneService', 'notificationService', '$state'];

    function zoneAddController($stateParams, zoneService, notificationService, $state) {
		var vm = this;
		vm.zoneId = 0;
        vm.title = 'ADD MODE';
		vm.zone = {};	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.zoneForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.zoneId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
            zoneService.getZone(vm.zoneId).then(function (data) {
				vm.zone = data.result;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.zoneId !== 0) {
                updateZone();
			} else {
                insertZone();
			}
		}

        function insertZone() {
            zoneService.saveZone(vm.zone).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updateZone() {
            zoneService.updateZone(vm.zoneId, vm.zone).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('zones');
		}

	}
})();
