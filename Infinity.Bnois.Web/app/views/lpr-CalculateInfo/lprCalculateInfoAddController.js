
(function () {

	'use strict';

	var controllerId = 'lprCalculateInfoAddController';

	angular.module('app').controller(controllerId, lprCalculateInfoAddController);
	lprCalculateInfoAddController.$inject = ['$stateParams', 'lprCalculateInfoService', 'notificationService', '$state'];

	function lprCalculateInfoAddController($stateParams, lprCalculateInfoService, notificationService, $state) {
		var vm = this;
		vm.lprCalculateInfoId = 0;
	    vm.title = 'ADD MODE';
        vm.lprCalculate = {};  
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
		vm.lprCalculateInfoForm = {};



		if ($stateParams.id !== undefined && $stateParams.id !== null) {

			vm.lprCalculateInfoId = $stateParams.id;
			vm.saveButtonText = 'Update';
		    vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			lprCalculateInfoService.getLprCalculateInfo(vm.lprCalculateInfoId).then(function (data) {
				console.log(data.result.lprCalculate);
				vm.lprCalculate = data.result.lprCalculate;



			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {
			if (vm.lprCalculateInfoId !== 0 && vm.lprCalculateInfoId !== '') {
				updateLprCalculateInfo();
			} else {
				insertLprCalculateInfo();
			}
		}

		function insertLprCalculateInfo() {
			lprCalculateInfoService.saveLprCalculateInfo(vm.lprCalculate).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}
		function updateLprCalculateInfo() {
			lprCalculateInfoService.updateLprCalculateInfo(vm.lprCalculateInfoId, vm.lprCalculate).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {
			$state.go('lprCalculateInfoes');
		}

	}
})();
