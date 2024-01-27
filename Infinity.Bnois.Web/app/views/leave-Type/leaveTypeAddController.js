

(function () {

	'use strict';

	var controllerId = 'leaveTypeAddController';

	angular.module('app').controller(controllerId, leaveTypeAddController);
	leaveTypeAddController.$inject = ['$stateParams', 'leaveTypeService', 'notificationService', '$state'];

	function leaveTypeAddController($stateParams, leaveTypeService, notificationService, $state) {
		var vm = this;
		vm.leaveTypeId = 0;
	    vm.title = 'ADD MODE';
		vm.leaveType = {};
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.leaveTypeForm = {};

		if ($stateParams.id !== undefined && $stateParams.id !== '') {
			vm.leaveTypeId = $stateParams.id;
			vm.saveButtonText = 'Update';
		    vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			leaveTypeService.getLeaveType(vm.leaveTypeId).then(function (data) {
				vm.leaveType = data.result;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.leaveTypeId !== 0) {
				updateLeaveType();
			} else {
				insertLeaveType();
			}
		}

		function insertLeaveType() {
			leaveTypeService.saveLeaveType(vm.leaveType).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function updateLeaveType() {
			leaveTypeService.updateLeaveType(vm.leaveTypeId, vm.leaveType).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {
			$state.go('leave-type');
		}

	}
})();
