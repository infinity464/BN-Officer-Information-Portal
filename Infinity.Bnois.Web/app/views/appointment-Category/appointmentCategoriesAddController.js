
(function () {

	'use strict';

    var controllerId = 'appointmentCategoriesAddController';

    angular.module('app').controller(controllerId, appointmentCategoriesAddController);
    appointmentCategoriesAddController.$inject = ['$stateParams', 'appointmentCategoryService', 'notificationService', '$state'];

    function appointmentCategoriesAddController($stateParams, appointmentCategoryService, notificationService, $state) {
		var vm = this;
		vm.appointmentCategoryId = 0;
        vm.title = 'ADD MODE';
        vm.appointmentCategory = {};
        vm.appointmentNatureList = [];
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.appointmentCategoryForm = {};



		if ($stateParams.id !== undefined && $stateParams.id !== null) {

            vm.appointmentCategoryId = $stateParams.id;
			vm.saveButtonText = 'Update';
		    vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
            appointmentCategoryService.getAppointmentCategory(vm.appointmentCategoryId).then(function (data) {
              
                vm.appointmentCategory = data.result.aptCatModel;
                vm.appointmentNatureList = data.result.appointmentNatureList;


			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {
            if (vm.appointmentCategoryId !== 0 && vm.appointmentCategoryId !== '') {
				updateAppointmentCategory();
			} else {
                insertAppointmentCategory();
			}
		}

        function insertAppointmentCategory() {
            appointmentCategoryService.saveAppointmentCategory(vm.appointmentCategory).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}
        function updateAppointmentCategory() {
            appointmentCategoryService.updateAppointmentCategory(vm.appointmentCategoryId, vm.appointmentCategory).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {
            $state.go('appointmentCategories');
		}

	}
})();
