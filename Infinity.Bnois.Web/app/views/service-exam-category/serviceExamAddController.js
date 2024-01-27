

(function () {

	'use strict';

	var controllerId = 'serviceExamCategoryAddController';

	angular.module('app').controller(controllerId, serviceExamCategoryAddController);
    serviceExamCategoryAddController.$inject = ['$stateParams', 'serviceExamCategoryService', 'notificationService', '$state'];

    function serviceExamCategoryAddController($stateParams, serviceExamCategoryService, notificationService, $state) {
		var vm = this;
		vm.serviceExamCategoryId = 0;
        vm.title = 'ADD MODE';
		vm.serviceExamCategory = {};	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.serviceExamCategoryForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.serviceExamCategoryId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			serviceExamCategoryService.getServiceExamCategory(vm.serviceExamCategoryId).then(function (data) {
				vm.serviceExamCategory = data.result;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.serviceExamCategoryId !== 0) {
                updateServiceExamCategory();
			} else {
                insertServiceExamCategory();
			}
		}

        function insertServiceExamCategory() {
			serviceExamCategoryService.saveServiceExamCategory(vm.serviceExamCategory).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updateServiceExamCategory() {
			serviceExamCategoryService.updateServiceExamCategory(vm.serviceExamCategoryId, vm.serviceExamCategory).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('service-exam-categories');
		}

	}
})();
