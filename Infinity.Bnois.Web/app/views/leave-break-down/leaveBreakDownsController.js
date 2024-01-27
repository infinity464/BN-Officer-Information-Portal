(function () {

    'use strict';
    var controllerId = 'leaveBreakDownsController';
    angular.module('app').controller(controllerId, leaveBreakDownsController);
	leaveBreakDownsController.$inject = ['$state', 'leaveBreakDownService', 'notificationService', '$location','$filter','codeValue'];

	function leaveBreakDownsController($state, leaveBreakDownService, notificationService, location, $filter, codeValue) {

        /* jshint validthis:true */
		var vm = this;
		vm.plCode = codeValue.Pl_Code;
		vm.monthDuration = codeValue.Month_Duration;
		vm.financialBenefit = codeValue.Financial_Benefit;
		vm.furloughCode = codeValue.Furlough_Leave;


        vm.leaveBreakDowns = [];
        vm.employeeId = "";
        vm.resultLeaveBreakDowns = [];
        vm.resultLeaveBreakDown = {};
		vm.employee = {};
		vm.lprCalculateInfo = {};
		vm.employeeGeneral = {};
        vm.getLeaveBreakDown = getLeaveBreakDown;
		vm.getPLTotal = getPLTotal;
		vm.getAvailTotal = getAvailTotal;
		vm.humanise = humanise;
		vm.getCommisionDays = getCommisionDays;
		vm.getMonthDays = getMonthDays;
		vm.getFurloughLeave = getFurloughLeave;
		vm.getLprFurlough = getLprFurlough;
		vm.getFinancialBenifit = getFinancialBenifit;

		function getLeaveBreakDown(employeeId) {
			
			leaveBreakDownService.getEmployeeLeaveBreakDown(employeeId).then(function (data) {
                vm.balance = 0;
				vm.leaveBreakDowns = data.result.leaveBreakDowns;
				//vm.filterleaveBreakDowns = data.result.leaveBreakDowns;
				vm.filterleaveBreakDowns = $filter('filter')(data.result.leaveBreakDowns, function (obj) {
					if (obj.leaveTypeId == vm.plCode)
						return obj;
				});
				vm.employee = data.result.employee;
				vm.lprCalculateInfo = data.result.lprCalculateInfo;
				vm.employeeGeneral = data.result.employeeGeneral;
		
                vm.index = 0;
                vm.leaveDuration = 0;
                vm.matchCount = 0;
                vm.totalDuration = 0;
				for (var i = 0; i < vm.filterleaveBreakDowns.length; i++) {
                    vm.resultLeaveBreakDown = new Object();
					vm.resultLeaveBreakDown.yearText = vm.filterleaveBreakDowns[i].yearText;
					vm.balance = vm.filterleaveBreakDowns[i].leaveDuration - vm.filterleaveBreakDowns[i].duration;
                    if (i > 0) {
						if (vm.filterleaveBreakDowns[i].yearText == vm.filterleaveBreakDowns[i - 1].yearText) {
                            vm.matchCount = vm.matchCount + 1;
                            if (vm.matchCount === 1) {
                                vm.index = i - 1;
								vm.leaveDuration = vm.filterleaveBreakDowns[i].leaveDuration;
								vm.totalDuration = vm.filterleaveBreakDowns[i - 1].duration;
                            }
							vm.totalDuration = vm.totalDuration + vm.filterleaveBreakDowns[i].duration;
                            vm.resultLeaveBreakDowns[vm.index].balance = vm.leaveDuration - vm.totalDuration;

                            vm.resultLeaveBreakDown.leaveDuration = null;
                            vm.balance = null;
                        }

                        else {
							vm.resultLeaveBreakDown.leaveDuration = vm.filterleaveBreakDowns[i].leaveDuration;
                        }
                    }
                    else {
						vm.resultLeaveBreakDown.leaveDuration = vm.filterleaveBreakDowns[i].leaveDuration;
                    }

					vm.resultLeaveBreakDown.duration = vm.filterleaveBreakDowns[i].duration;
					vm.resultLeaveBreakDown.slot = vm.filterleaveBreakDowns[i].slot;
					vm.resultLeaveBreakDown.formDate = vm.filterleaveBreakDowns[i].formDate;
					vm.resultLeaveBreakDown.toDate = vm.filterleaveBreakDowns[i].toDate;
                    vm.resultLeaveBreakDown.balance = vm.balance;
					vm.resultLeaveBreakDown.leaveTypeId = vm.filterleaveBreakDowns[i].leaveTypeId;                  
                    vm.resultLeaveBreakDowns.push(vm.resultLeaveBreakDown);

                }
                // console.log("Result:" + vm.resultLeaveBreakDowns);

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
		}

		function getCommisionDays() {		
			var commissionDate = new Date(vm.employeeGeneral.commissionDate);
			var currentDate = new Date();
			var dayDif = (currentDate - commissionDate) / 1000 / 60 / 60 / 24;			
			return dayDif;
		}
		function getPLTotal() {			
			var sum = 0;
		
			for (var i = 0; i < vm.resultLeaveBreakDowns.length ; i++) {
				if (vm.resultLeaveBreakDowns[i].leaveTypeId == vm.plCode) sum += vm.resultLeaveBreakDowns[i].leaveDuration;				
			}
			return sum;			
		}
		function getAvailTotal() {
			var sum = 0;
			for (var i = 0; i < vm.resultLeaveBreakDowns.length; i++) {
				if (vm.resultLeaveBreakDowns[i].leaveTypeId == vm.plCode) sum += vm.resultLeaveBreakDowns[i].duration;
			}
			return sum;
		}

		function humanise(diff) {		
			var str = '';		
			var values = [[' year', 365], [' month', 30], [' day', 1]];		
			for (var i = 0; i < values.length; i++) {
				var amount = Math.floor(diff / values[i][1]);			
				if (amount >= 1) {				
					str += amount + values[i][0] + (amount > 1 ? 's' : '') + ' ';				
					diff -= amount * values[i][1];
				}
			}

			return str;
		}
		function getFurloughLeave() {
			var sum = 0;
			var avail = 0;
			for (var i = 0; i < vm.resultLeaveBreakDowns.length; i++) {
				if (vm.resultLeaveBreakDowns[i].leaveTypeId == vm.furloughCode) {
					sum += vm.resultLeaveBreakDowns[i].leaveDuration;
					avail += vm.resultLeaveBreakDowns[i].duration;

				}
			}
			var total = sum - avail;
			return total;
		}
		function getMonthDays(diff) {
			var str = '';
			var values = [[' month', 30], [' day', 1]];
			for (var i = 0; i < values.length; i++) {
				var amount = Math.floor(diff / values[i][1]);
				if (amount >= 1) {
					str += amount + values[i][0] + (amount > 1 ? 's' : '') + ' ';
					diff -= amount * values[i][1];
				}
			}

			return str;
		}
		function getLprFurlough() {
		
			var total = vm.lprCalculateInfo.flLeave * vm.monthDuration;
			return total;
		}
		function getFinancialBenifit(diff) {
			if (diff >= vm.financialBenefit) return vm.financialBenefit;
			else return diff;
		}

    }

})();
