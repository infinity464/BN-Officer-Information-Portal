

(function () {

	'use strict';

	var controllerId = 'heirTypeAddController';

	angular.module('app').controller(controllerId, heirTypeAddController);
    heirTypeAddController.$inject = ['$stateParams', 'heirTypeService', 'notificationService', '$state'];

    function heirTypeAddController($stateParams, heirTypeService, notificationService, $state) {
		var vm = this;
		vm.heirTypeId = 0;
        vm.title = 'ADD MODE';
		vm.heirType = {};	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.heirTypeForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.heirTypeId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
        function Init() {         
			heirTypeService.getHeirType(vm.heirTypeId).then(function (data) {
				vm.heirType = data.result;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.heirTypeId !== 0) {
                updateHeirType();
			} else {
                insertHeirType();
			}
		}

        function insertHeirType() {
			heirTypeService.saveHeirType(vm.heirType).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updateHeirType() {
			heirTypeService.updateHeirType(vm.heirTypeId, vm.heirType).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('heir-types');
		}

	}
})();
