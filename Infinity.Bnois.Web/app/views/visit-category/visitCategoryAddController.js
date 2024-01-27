

(function () {

	'use strict';

	var controllerId = 'visitCategoryAddController';

	angular.module('app').controller(controllerId, visitCategoryAddController);
    visitCategoryAddController.$inject = ['$stateParams', 'visitCategoryService', 'notificationService', '$state'];

    function visitCategoryAddController($stateParams, visitCategoryService, notificationService, $state) {
		var vm = this;
		vm.visitCategoryId = 0;
        vm.title = 'ADD MODE';
		vm.visitCategory = {};	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.visitCategoryForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.visitCategoryId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			visitCategoryService.getVisitCategory(vm.visitCategoryId).then(function (data) {
				vm.visitCategory = data.result;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.visitCategoryId !== 0) {
                updateVisitCategory();
			} else {
                insertVisitCategory();
			}
		}

        function insertVisitCategory() {
			visitCategoryService.saveVisitCategory(vm.visitCategory).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updateVisitCategory() {
			visitCategoryService.updateVisitCategory(vm.visitCategoryId, vm.visitCategory).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('visit-categories');
		}

	}
})();
