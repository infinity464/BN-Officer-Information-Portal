

(function () {

	'use strict';

	var controllerId = 'bloodGroupAddController';

	angular.module('app').controller(controllerId, bloodGroupAddController);
    bloodGroupAddController.$inject = ['$stateParams', 'bloodGroupService', 'notificationService', '$state'];

    function bloodGroupAddController($stateParams, bloodGroupService, notificationService, $state) {
		var vm = this;
		vm.bloodGroupId = 0;
        vm.title = 'ADD MODE';
		vm.bloodGroup = {};	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.bloodGroupForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.bloodGroupId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			bloodGroupService.getBloodGroup(vm.bloodGroupId).then(function (data) {
				vm.bloodGroup = data.result;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.bloodGroupId !== 0) {
                updateBloodGroup();
			} else {
                insertBloodGroup();
			}
		}

        function insertBloodGroup() {
			bloodGroupService.saveBloodGroup(vm.bloodGroup).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updateBloodGroup() {
			bloodGroupService.updateBloodGroup(vm.bloodGroupId, vm.bloodGroup).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('blood-groups');
		}

	}
})();
