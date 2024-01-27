(function () {

	'use strict';
    var controllerId = 'appointmentCategoriesController';
    angular.module('app').controller(controllerId, appointmentCategoriesController);
    appointmentCategoriesController.$inject = ['$state', 'appointmentCategoryService', 'notificationService', '$location'];

    function appointmentCategoriesController($state, appointmentCategoryService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
		vm.appointmentCategories = [];
        vm.addAppointmentCategory = addAppointmentCategory;
        vm.updateAppointmentCategory = updateAppointmentCategory;
        vm.deleteAppointmentCategory = deleteAppointmentCategory;
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
            appointmentCategoryService.getAppointmentCategories(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.appointmentCategories = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function addAppointmentCategory() {
            $state.go('appointmentCategory-create');
		}

        function updateAppointmentCategory(appointmentCategory) {

            $state.go('appointmentCategory-modify', { id: appointmentCategory.acatId });
		}

        function deleteAppointmentCategory(appointmentCategory) {
            appointmentCategoryService.deleteAppointmentCategory(appointmentCategory.acatId).then(function (data) {
				$state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
			});
		}

		function pageChanged() {
            $state.go('appointmentCategories', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}
	}

})();
