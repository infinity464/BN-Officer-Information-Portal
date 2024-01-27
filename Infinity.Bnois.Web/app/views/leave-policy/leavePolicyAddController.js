

(function () {

	'use strict';

	var controllerId = 'leavePolicyAddController';

	angular.module('app').controller(controllerId, leavePolicyAddController);
	leavePolicyAddController.$inject = ['$stateParams', 'leavePolicyService', 'notificationService', '$state','codeValue'];

	function leavePolicyAddController($stateParams, leavePolicyService, notificationService, $state, codeValue) {
		var vm = this;
		// Additionnal Parameter Code Set
		vm.plCode = codeValue.Pl_Code;
		vm.AfterYear = codeValue.After_Year;
		vm.WholeCarrer = codeValue.Whole_Carrer;
		vm.Month = codeValue.Month;
		vm.Day = codeValue.Day;
		vm.MonthDuration = codeValue.Month_Duration;

		vm.leavePolicyId = 0;
	    vm.title = 'ADD MODE';
		vm.leavePolicy = {};
		vm.commissionType = [];
		vm.leaveType = [];
		vm.effectType = [];
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
        vm.durationType = [];
        vm.effectTypeLabel = "";      
		vm.leavePolicyForm = {};
		vm.getEffectType = getEffectType;
		vm.checkForeignDate = checkForeignDate;
		if ($stateParams.id !== undefined && $stateParams.id !== '') {
			vm.leavePolicyId = $stateParams.id;
			vm.saveButtonText = 'Update';
		    vm.title = 'UPDATE MODE';
		}

		Init();
		function Init() {
			leavePolicyService.getLeavePolicy(vm.leavePolicyId).then(function (data) {
				
				vm.leavePolicy = data.result.leavePolicy;
				vm.commissionType = data.result.commissionType;
				vm.leaveType = data.result.leaveType;
				vm.effectType = data.result.effectType;
                vm.durationType = data.result.durationType;
				
				//vm.modules = data.result.modules;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

			if (vm.leavePolicyId !== 0) {
				updateLeavePolicy();
			} else {
				insertLeavePolicy();
			}
		}

		function insertLeavePolicy() {
			leavePolicyService.saveLeavePolicy(vm.leavePolicy).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function updateLeavePolicy() {
			leavePolicyService.updateLeavePolicy(vm.leavePolicyId, vm.leavePolicy).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
        }
        function getEffectType(effectID) {
			if (effectID == vm.WholeCarrer) {
                vm.effectTypeLabel = "Per Year";

            }
			else if (effectID == vm.AfterYear) {
				vm.effectTypeLabel = "Year";
            }
            else {
				vm.effectTypeLabel = "";
            }
        }
		function checkForeignDate() {
			var leaveDuration = vm.leavePolicy.leaveDurationType == vm.Month ? (vm.leavePolicy.leaveDuration * vm.MonthDuration): vm.leavePolicy.leaveDuration;
			var ForeignDuration = vm.leavePolicy.foreignDurationType == vm.Month ? (vm.leavePolicy.foreignDuration * vm.MonthDuration): vm.leavePolicy.foreignDuration;
			if (leaveDuration < ForeignDuration) {
				notificationService.displayError("Foreign Duration Must Be Less Than LeaveDuration");
				vm.leavePolicy.foreignDuration = '';
				vm.leavePolicy.foreignDurationType = '';
			}

		}
		function close() {
			$state.go('leavePolicyies');
		}
        
	}
})();
