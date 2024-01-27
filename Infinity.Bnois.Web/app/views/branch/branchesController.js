(function () {

	'use strict';
	var controllerId = 'branchesController';
	angular.module('app').controller(controllerId, branchesController);
    branchesController.$inject = ['$state', 'branchService', 'notificationService', '$location'];

    function branchesController($state, branchService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
        vm.branches = [];
        vm.permission = {};
		vm.addBranch = addBranch;
		vm.updateBranch = updateBranch;
		vm.deleteBranch = deleteBranch;
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
			branchService.getBranches(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {

				vm.branches = data.result;			
                vm.total = data.total; vm.permission = data.permission;
                vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function addBranch() {
            $state.go('branch-create');		
		}

		function updateBranch(branch) {
            $state.go('branch-modify', { id: branch.branchId });		
		}

		function deleteBranch(branch) {	

            branchService.deleteBranch(branch.branchId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
		}

		function pageChanged() {		
            $state.go('branches', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}
	}

})();
