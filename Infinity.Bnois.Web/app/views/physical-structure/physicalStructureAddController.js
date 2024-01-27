

(function () {

	'use strict';

	var controllerId = 'physicalStructureAddController';

	angular.module('app').controller(controllerId, physicalStructureAddController);
    physicalStructureAddController.$inject = ['$stateParams', 'physicalStructureService', 'notificationService', '$state'];

    function physicalStructureAddController($stateParams, physicalStructureService, notificationService, $state) {
		var vm = this;
		vm.physicalStructureId = 0;
        vm.title = 'ADD MODE';
		vm.physicalStructure = {};	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.physicalStructureForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.physicalStructureId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
            vm.saveButtonText = 'Update';
		}

		Init();
		function Init() {
			physicalStructureService.getPhysicalStructure(vm.physicalStructureId).then(function (data) {
				vm.physicalStructure = data.result;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.physicalStructureId !== 0) {
			    updatePhysicalStructure();
			} else {
			    insertPhysicalStructure();
			}
		}

        function insertPhysicalStructure() {
			physicalStructureService.savePhysicalStructure(vm.physicalStructure).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updatePhysicalStructure() {
			physicalStructureService.updatePhysicalStructure(vm.physicalStructureId, vm.physicalStructure).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('physical-structures');
		}

	}
})();
