

(function () {

	'use strict';

	var controllerId = 'publicationCategoryAddController';

	angular.module('app').controller(controllerId, publicationCategoryAddController);
    publicationCategoryAddController.$inject = ['$stateParams', 'publicationCategoryService', 'notificationService', '$state'];

    function publicationCategoryAddController($stateParams, publicationCategoryService, notificationService, $state) {
		var vm = this;
		vm.publicationCategoryId = 0;
        vm.title = 'ADD MODE';
		vm.publicationCategory = {};	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.publicationCategoryForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.publicationCategoryId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			publicationCategoryService.getPublicationCategory(vm.publicationCategoryId).then(function (data) {
				vm.publicationCategory = data.result;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.publicationCategoryId !== 0) {
                updatePublicationCategory();
			} else {
                insertPublicationCategory();
			}
		}

        function insertPublicationCategory() {
			publicationCategoryService.savePublicationCategory(vm.publicationCategory).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updatePublicationCategory() {
			publicationCategoryService.updatePublicationCategory(vm.publicationCategoryId, vm.publicationCategory).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('publication-categories');
		}

	}
})();
