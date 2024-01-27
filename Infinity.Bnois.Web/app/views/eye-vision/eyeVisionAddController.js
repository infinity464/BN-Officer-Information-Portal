

(function () {

	'use strict';

	var controllerId = 'eyeVisionAddController';

	angular.module('app').controller(controllerId, eyeVisionAddController);
    eyeVisionAddController.$inject = ['$stateParams', 'eyeVisionService', 'notificationService', '$state'];

    function eyeVisionAddController($stateParams, eyeVisionService, notificationService, $state) {
		var vm = this;
		vm.eyeVisionId = 0;
        vm.title = 'ADD MODE';
		vm.eyeVision = {};	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.eyeVisionForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.eyeVisionId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			eyeVisionService.getEyeVision(vm.eyeVisionId).then(function (data) {
				vm.eyeVision = data.result;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.eyeVisionId !== 0) {
                updateEyeVision();
			} else {
                insertEyeVision();
			}
		}

        function insertEyeVision() {
			eyeVisionService.saveEyeVision(vm.eyeVision).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updateEyeVision() {
			eyeVisionService.updateEyeVision(vm.eyeVisionId, vm.eyeVision).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('eye-visions');
		}

	}
})();
