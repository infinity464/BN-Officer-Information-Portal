

(function () {

	'use strict';

	var controllerId = 'subjectAddController';

	angular.module('app').controller(controllerId, subjectAddController);
    subjectAddController.$inject = ['$stateParams', 'subjectService', 'notificationService', '$state'];

    function subjectAddController($stateParams, subjectService, notificationService, $state) {
		var vm = this;
		vm.subjectId = 0;
        vm.title = 'ADD MODE';
		vm.subject = {};	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.subjectForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.subjectId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			subjectService.getSubject(vm.subjectId).then(function (data) {
				vm.subject = data.result;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.subjectId !== 0) {
				updateFeature();
			} else {
				insertFeature();
			}
		}

		function insertFeature() {
			subjectService.saveSubject(vm.subject).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function updateFeature() {
			subjectService.updateSubject(vm.subjectId, vm.subject).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('subjects');
		}

	}
})();
