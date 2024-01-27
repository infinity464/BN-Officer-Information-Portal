(function () {

	'use strict';

	var controllerId = 'examCategoryAddController';

	angular.module('app').controller(controllerId, examCategoryAddController);
    examCategoryAddController.$inject = ['$stateParams', 'examCategoryService', 'notificationService', '$state'];

    function examCategoryAddController($stateParams, examCategoryService, notificationService, $state) {
		var vm = this;
		vm.examCategoryId = 0;
        vm.title = 'ADD MODE';
        vm.examCategory = {};
        vm.boardTypes = [{ text: "Board", value: 1 }, { text: "University", value: 2 }]
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.examCategoryForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.examCategoryId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			examCategoryService.getExamCategory(vm.examCategoryId).then(function (data) {
				vm.examCategory = data.result;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.examCategoryId !== 0 && vm.examCategoryId !== '') {
				updateExamCategory();
			} else {
				insertExamCategory();
			}
		}

		function insertExamCategory() {
			examCategoryService.saveExamCategory(vm.examCategory).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}
		function updateExamCategory() {
			examCategoryService.updateExamCategory(vm.examCategoryId, vm.examCategory).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {
            $state.go('exam-categories');
		}
	}
})();
