(function () {

	'use strict';

	var controllerId = 'leavePolicyiesController';
	angular.module('app').controller(controllerId, leavePolicyiesController);
	leavePolicyiesController.$inject = ['$state', 'leavePolicyService', 'notificationService', '$location'];

	function leavePolicyiesController($state, leavePolicyService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
		vm.leavePolicyies = [];
		vm.addLeavePolicy = addLeavePolicy;
		vm.updateLeavePolicy = updateLeavePolicy;
		vm.deleteLeavePolicy = deleteLeavePolicy;
		vm.pageChanged = pageChanged;
		vm.onSearch = onSearch;
		vm.searchText = "";
		vm.pageSize = 50;
		vm.pageNumber = 1;
		vm.total = 0;

		if (location.search().ps !== undefined && location.search().ps != null && location.search().ps != '') {
			vm.pageSize = location.search().ps;
		}

		if (location.search().pn !== undefined && location.search().pn != null && location.search().pn != '') {
			vm.pageNumber = location.search().pn;
		}
		if (location.search().q !== undefined && location.search().q != null && location.search().q != '') {
			vm.searchText = location.search().q;
		}
		init();
		function init() {
			leavePolicyService.getLeavePolicyies(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
				vm.leavePolicyies = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function addLeavePolicy() {
			$state.go('leavePolicy-create');
		}

		function updateLeavePolicy(leavePolicyies) {

			$state.go('leavePolicy-modify', { id: leavePolicyies.leavePolicyId });
		}

		function deleteLeavePolicy(leavePolicyies) {
			leavePolicyService.deleteLeavePolicy(leavePolicyies.leavePolicyId).then(function (data) {
				$state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
			});
		}

		function pageChanged() {
			$state.go('leavePolicyies', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}


	}

})();
