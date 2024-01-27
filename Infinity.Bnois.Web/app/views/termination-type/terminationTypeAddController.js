

(function () {

	'use strict';

	var controllerId = 'terminationTypeAddController';

	angular.module('app').controller(controllerId, terminationTypeAddController);
    terminationTypeAddController.$inject = ['$stateParams', 'terminationTypeService', 'notificationService', '$state'];

    function terminationTypeAddController($stateParams, terminationTypeService, notificationService, $state) {
		var vm = this;
		vm.terminationTypeId = 0;
        vm.title = 'ADD MODE';
		vm.terminationType = {};	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.terminationTypeForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.terminationTypeId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			terminationTypeService.getTerminationType(vm.terminationTypeId).then(function (data) {
				vm.terminationType = data.result;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.terminationTypeId !== 0) {
                updateTerminationType();
			} else {
                insertTerminationType();
			}
		}

        function insertTerminationType() {
			terminationTypeService.saveTerminationType(vm.terminationType).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updateTerminationType() {
			terminationTypeService.updateTerminationType(vm.terminationTypeId, vm.terminationType).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('termination-types');
		}

	}
})();
