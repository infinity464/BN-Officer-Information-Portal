

(function () {

	'use strict';

	var controllerId = 'punishmentCategoryAddController';

	angular.module('app').controller(controllerId, punishmentCategoryAddController);
    punishmentCategoryAddController.$inject = ['$stateParams', 'punishmentCategoryService', 'notificationService', '$state'];

    function punishmentCategoryAddController($stateParams, punishmentCategoryService, notificationService, $state) {
		var vm = this;
		vm.punishmentCategoryId = 0;
        vm.title = 'ADD MODE';
		vm.punishmentCategory = {};	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.punishmentCategoryForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.punishmentCategoryId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			punishmentCategoryService.getPunishmentCategory(vm.punishmentCategoryId).then(function (data) {
				vm.punishmentCategory = data.result;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.punishmentCategoryId !== 0) {
                updatePunishmentCategory();
			} else {
                insertPunishmentCategory();
			}
		}

        function insertPunishmentCategory() {
			punishmentCategoryService.savePunishmentCategory(vm.punishmentCategory).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updatePunishmentCategory() {
			punishmentCategoryService.updatePunishmentCategory(vm.punishmentCategoryId, vm.punishmentCategory).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('punishment-categories');
		}

	}
})();
