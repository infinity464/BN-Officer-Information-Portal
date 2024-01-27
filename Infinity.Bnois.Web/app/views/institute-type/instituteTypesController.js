(function () {

	'use strict';
    var controllerId = 'instituteTypesController';
    angular.module('app').controller(controllerId, instituteTypesController);
    instituteTypesController.$inject = ['$state', 'instituteTypeService', 'notificationService', '$location'];

    function instituteTypesController($state, instituteTypeService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
        vm.instituteTypes = [];
        vm.addInstituteType = addInstituteType;
        vm.updateInstituteType = updateInstituteType;
        vm.deleteInstituteType = deleteInstituteType;
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
            instituteTypeService.getInstituteTypes(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.instituteTypes = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function addInstituteType() {
            $state.go('institute-type-create');			
		}

        function updateInstituteType(instituteType) {
            $state.go('institute-type-modify', { id: instituteType.instituteTypeId });
		}

        function deleteInstituteType(instituteType) {	
            instituteTypeService.deleteInstituteType(instituteType.instituteTypeId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
		}

        function pageChanged() {
            $state.go('institute-types', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false })
		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}
	}

})();
