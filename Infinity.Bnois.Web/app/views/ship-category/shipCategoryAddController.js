

(function () {

	'use strict';

	var controllerId = 'shipCategoryAddController';

	angular.module('app').controller(controllerId, shipCategoryAddController);
    shipCategoryAddController.$inject = ['$stateParams', 'shipCategoryService', 'notificationService', '$state'];

    function shipCategoryAddController($stateParams, shipCategoryService, notificationService, $state) {
		var vm = this;
		vm.shipCategoryId = 0;
        vm.title = 'ADD MODE';
		vm.shipCategory = {};	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.shipCategoryForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.shipCategoryId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
            shipCategoryService.getShipCategory(vm.shipCategoryId).then(function (data) {
				vm.shipCategory = data.result;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.shipCategoryId !== 0) {
                updateShipCategory();
			} else {
                insertShipCategory();
			}
		}

        function insertShipCategory() {
            shipCategoryService.saveShipCategory(vm.shipCategory).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updateShipCategory() {
            shipCategoryService.updateShipCategory(vm.shipCategoryId, vm.shipCategory).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('ship-categories');
		}

	}
})();
