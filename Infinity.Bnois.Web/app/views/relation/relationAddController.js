

(function () {

	'use strict';

	var controllerId = 'relationAddController';

	angular.module('app').controller(controllerId, relationAddController);
    relationAddController.$inject = ['$stateParams', 'relationService', 'notificationService', '$state'];

    function relationAddController($stateParams, relationService, notificationService, $state) {
		var vm = this;
		vm.relationId = 0;
        vm.title = 'ADD MODE';
		vm.relation = {};	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.relationForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.relationId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			relationService.getRelation(vm.relationId).then(function (data) {
				vm.relation = data.result;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.relationId !== 0) {
                updateRelation();
			} else {
                insertRelation();
			}
		}

        function insertRelation() {
			relationService.saveRelation(vm.relation).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updateRelation() {
			relationService.updateRelation(vm.relationId, vm.relation).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('relations');
		}

	}
})();
