(function () {

	'use strict';
	var controllerId = 'bloodGroupsController';
	angular.module('app').controller(controllerId, bloodGroupsController);
    bloodGroupsController.$inject = ['$state', 'bloodGroupService', 'notificationService', '$location'];

    function bloodGroupsController($state, bloodGroupService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
		vm.bloodGroups = [];
        vm.addBloodGroup = addBloodGroup;
        vm.updateBloodGroup = updateBloodGroup;
        vm.deleteBloodGroup = deleteBloodGroup;
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
			bloodGroupService.getBloodGroups(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {

                vm.bloodGroups = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function addBloodGroup() {
            $state.go('blood-group-create');			
		}

		function updateBloodGroup(bloodGroup) {
            $state.go('blood-group-modify', { id: bloodGroup.bloodGroupId });
		}

		function deleteBloodGroup(bloodGroup) {	
            bloodGroupService.deleteBloodGroup(bloodGroup.bloodGroupId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
		}

        function pageChanged() {
            $state.go('blood-groups', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });

		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}
	}

})();
