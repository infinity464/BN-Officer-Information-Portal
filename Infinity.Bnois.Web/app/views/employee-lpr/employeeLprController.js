(function () {

	'use strict';
	var controllerId = 'employeeLprController';
	angular.module('app').controller(controllerId, employeeLprController);
	employeeLprController.$inject = ['$state', 'employeeLprService', 'notificationService', '$location'];

	function employeeLprController($state, employeeLprService, notificationService, location) {

		/* jshint validthis:true */
        var vm = this;

        vm.Retired = 2
        vm.LPR = 6;
        vm.Termination = 7;

		vm.employeeLprs = [];
		vm.addEmployeeLpr = addEmployeeLpr;
		vm.updateEmployeeLpr = updateEmployeeLpr;
		vm.deleteEmployeeLpr = deleteEmployeeLpr;
		vm.pageChanged = pageChanged;
		vm.searchText = "";
		vm.onSearch = onSearch;
		vm.pageSize = 50;
		vm.pageNumber = 1;
		vm.total = 0;

		if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
			vm.pageSize = location.search().ps;
		}

		if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
			vm.pageNumber = location.search().pn;
		}
		if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
			vm.searchText = location.search().q;
		}
		init();
		function init() {
			employeeLprService.getEmployeeLprs(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
				vm.employeeLprs = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function addEmployeeLpr() {
			$state.go('employee-lpr-create');
		}

		function updateEmployeeLpr(employeeLpr) {
			$state.go('employee-lpr-modify', { id: employeeLpr.empLprId });
		}

		function deleteEmployeeLpr(employeeLpr) {
			employeeLprService.deleteEmployeeLpr(employeeLpr.empLprId).then(function (data) {
				$state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
			});
		}

		function pageChanged() {
			$state.go('employeelpr', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}
	}

})();
