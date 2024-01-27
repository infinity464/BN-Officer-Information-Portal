

(function () {

	'use strict';

    var controllerId = 'instituteTypeAddController';

    angular.module('app').controller(controllerId, instituteTypeAddController);
    instituteTypeAddController.$inject = ['$stateParams', 'instituteTypeService', 'notificationService', '$state'];

    function instituteTypeAddController($stateParams, instituteTypeService, notificationService, $state) {
		var vm = this;
        vm.instituteTypeId = 0;
        vm.title = 'ADD MODE';
        vm.instituteType = {};	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
        vm.instituteTypeForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.instituteTypeId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
            instituteTypeService.getInstituteType(vm.instituteTypeId).then(function (data) {
                vm.instituteType = data.result;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

            if (vm.instituteTypeId !== 0) {
                updateInstituteType();
			} else {
                insertInstituteType();
			}
		}

        function insertInstituteType() {
            instituteTypeService.saveInstituteType(vm.instituteType).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updateInstituteType() {
            instituteTypeService.updateInstituteType(vm.instituteTypeId, vm.instituteType).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('institute-types');
		}

	}
})();
