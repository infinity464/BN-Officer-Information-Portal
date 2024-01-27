(function () {

	'use strict';
    var controllerId = 'institutesController';
    angular.module('app').controller(controllerId, institutesController);
    institutesController.$inject = ['$state', 'instituteService', 'notificationService', '$location'];

    function institutesController($state, instituteService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
        vm.institutes = [];
        vm.addInstitute = addInstitute;
        vm.updateInstitute = updateInstitute;
        vm.deleteInstitute = deleteInstitute;
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
            instituteService.getInstitutes(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.institutes = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function addInstitute() {
            $state.go('institute-create');			
		}

        function updateInstitute(institute) {
            $state.go('institute-modify', { id: institute.instituteId });
		}

        function deleteInstitute(institute) {	
            instituteService.deleteInstitute(institute.instituteId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
		}

        function pageChanged() {
            $state.go('institutes', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false })
		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}
	}

})();
