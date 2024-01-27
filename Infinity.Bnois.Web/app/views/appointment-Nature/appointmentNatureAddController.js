
(function () {

	'use strict';

	var controllerId = 'appointmentNatureAddController';

	angular.module('app').controller(controllerId, appointmentNatureAddController);
	appointmentNatureAddController.$inject = ['$stateParams', 'appointmentNatureService', 'notificationService', '$state'];

	function appointmentNatureAddController($stateParams, appointmentNatureService, notificationService, $state) {
		var vm = this;
		vm.appointmentNatureId = 0;
	    vm.title = 'ADD MODE';
		vm.appointmentNature = {};
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.appointmentNatureForm = {};
	


		if ($stateParams.id !== undefined && $stateParams.id !== null) {
		
			vm.appointmentNatureId = $stateParams.id;
			vm.saveButtonText = 'Update';
		    vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			appointmentNatureService.getAppointmentNature(vm.appointmentNatureId).then(function (data) {
				vm.appointmentNature = data.result;		


			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {
			if (vm.appointmentNatureId !== 0 && vm.appointmentNatureId !== '') {
				updateAppointmentNature();
			} else {
				insertAppointmentNature();
			}
		}

		function insertAppointmentNature() {
			appointmentNatureService.saveAppointmentNature(vm.appointmentNature).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}
		function updateAppointmentNature() {
			appointmentNatureService.updateAppointmentNature(vm.appointmentNatureId, vm.appointmentNature).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {
			$state.go('appointmentNatures');
		}

	}
})();
