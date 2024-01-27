

(function () {

	'use strict';

	var controllerId = 'preCommissionRankAddController';

	angular.module('app').controller(controllerId, preCommissionRankAddController);
    preCommissionRankAddController.$inject = ['$stateParams', 'preCommissionRankService', 'notificationService', '$state'];

    function preCommissionRankAddController($stateParams, preCommissionRankService, notificationService, $state) {
		var vm = this;
		vm.preCommissionRankId = 0;
        vm.title = 'ADD MODE';
		vm.preCommissionRank = {};	
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.preCommissionRankForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.preCommissionRankId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			preCommissionRankService.getPreCommissionRank(vm.preCommissionRankId).then(function (data) {
				vm.preCommissionRank = data.result;			
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.preCommissionRankId !== 0) {
                updatePreCommissionRank();
			} else {
                insertPreCommissionRank();
			}
		}

        function insertPreCommissionRank() {
			preCommissionRankService.savePreCommissionRank(vm.preCommissionRank).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updatePreCommissionRank() {
			preCommissionRankService.updatePreCommissionRank(vm.preCommissionRankId, vm.preCommissionRank).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('pre-commission-ranks');
		}

	}
})();
