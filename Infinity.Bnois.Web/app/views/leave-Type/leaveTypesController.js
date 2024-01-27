(function () {

	'use strict';

	var controllerId = 'leaveTypesController';
	angular.module('app').controller(controllerId, leaveTypesController);
	leaveTypesController.$inject = ['$state', 'leaveTypeService', 'notificationService', '$location'];

	function leaveTypesController($state, leaveTypeService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
		vm.leaveTypes = [];
		vm.addLeaveType = addLeaveType;
		vm.updateLeaveType = updateLeaveType;
		vm.deleteLeaveType = deleteLeaveType;
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
			leaveTypeService.getLeaveTypes(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
				vm.leaveTypes = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function addLeaveType() {
			$state.go('leave-type-create');
		}

		function updateLeaveType(leaveType) {

			$state.go('leave-type-modify', { id: leaveType.leaveTypeId });
		}

		function deleteLeaveType(leave) {
            leaveTypeService.deleteLeaveType(leave.leaveTypeId).then(function (data) {
				$state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
			});
		}

		function pageChanged() {
			$state.go('leave-type', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}


	}

})();
