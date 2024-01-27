

(function () {

	'use strict';

	var controllerId = 'commendationAddController';

	angular.module('app').controller(controllerId, commendationAddController);
    commendationAddController.$inject = ['$stateParams', 'commendationService', 'notificationService', '$state'];

    function commendationAddController($stateParams, commendationService, notificationService, $state) {
		var vm = this;
		vm.commendationId = 0;
        vm.title = 'ADD MODE';
		vm.commendation = {};	
		vm.commendationTypes = [];	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.commendationForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.commendationId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			commendationService.getCommendation(vm.commendationId).then(function (data) {
				vm.commendation = data.result.commendation;			
				vm.commendationTypes = data.result.commendationTypes;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.commendationId !== 0) {
                updateCommendation();
			} else {
                insertCommendation();
			}
		}

        function insertCommendation() {
			commendationService.saveCommendation(vm.commendation).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updateCommendation() {
			commendationService.updateCommendation(vm.commendationId, vm.commendation).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('commendations');
		}

	}
})();
