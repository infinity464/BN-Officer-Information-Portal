

(function () {

	'use strict';

	var controllerId = 'punishmentNatureAddController';

	angular.module('app').controller(controllerId, punishmentNatureAddController);
    punishmentNatureAddController.$inject = ['$stateParams', 'punishmentNatureService', 'notificationService', '$state'];

    function punishmentNatureAddController($stateParams, punishmentNatureService, notificationService, $state) {
		var vm = this;
		vm.punishmentNatureId = 0;
        vm.title = 'ADD MODE';
		vm.punishmentNature = {};	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.punishmentNatureForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.punishmentNatureId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			punishmentNatureService.getPunishmentNature(vm.punishmentNatureId).then(function (data) {
				vm.punishmentNature = data.result;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.punishmentNatureId !== 0) {
                updatePunishmentNature();
			} else {
                insertPunishmentNature();
			}
		}

        function insertPunishmentNature() {
			punishmentNatureService.savePunishmentNature(vm.punishmentNature).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updatePunishmentNature() {
			punishmentNatureService.updatePunishmentNature(vm.punishmentNatureId, vm.punishmentNature).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('punishment-natures');
		}

	}
})();
