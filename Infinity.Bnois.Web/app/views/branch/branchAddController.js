

(function () {

	'use strict';

	var controllerId = 'branchAddController';

	angular.module('app').controller(controllerId, branchAddController);
    branchAddController.$inject = ['$stateParams', 'branchService', 'notificationService', '$state'];

    function branchAddController($stateParams, branchService, notificationService, $state) {
		var vm = this;
		vm.branchId = 0;
        vm.title = 'ADD MODE';
		vm.branch = {};
		//vm.modules = [];
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.branchForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.branchId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			branchService.getBranch(vm.branchId).then(function (data) {
				vm.branch = data.result;
				//vm.modules = data.result.modules;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {
		
			if (vm.branchId !== 0) {
				updateFeature();
			} else {
				insertFeature();
			}
		}

		function insertFeature() {
			branchService.saveBranch(vm.branch).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function updateFeature() {
			branchService.updateBranch(vm.branchId, vm.branch).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {		
            $state.go('branches');
		}

	}
})();
