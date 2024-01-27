
(function () {

	'use strict';

	var controllerId = 'patternAddController';

	angular.module('app').controller(controllerId, patternAddController);
	patternAddController.$inject = ['$stateParams', 'patternService', 'notificationService', '$state'];

	function patternAddController($stateParams, patternService, notificationService, $state) {
		var vm = this;
		vm.patternId = 0;
	    vm.title = 'ADD MODE';
		vm.pattern = {};
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.patternForm = {};



		if ($stateParams.id !== undefined && $stateParams.id !== null) {

			vm.patternId = $stateParams.id;
			vm.saveButtonText = 'Update';
		    vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			patternService.getPattern(vm.patternId).then(function (data) {
				vm.pattern = data.result;


			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {
			if (vm.patternId !== 0 && vm.patternId !== '') {
				updatePattern();
			} else {
				insertPattern();
			}
		}

		function insertPattern() {
			patternService.savePattern(vm.pattern).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}
		function updatePattern() {
			patternService.updatePattern(vm.patternId, vm.pattern).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {
			$state.go('patterns');
		}

	}
})();
