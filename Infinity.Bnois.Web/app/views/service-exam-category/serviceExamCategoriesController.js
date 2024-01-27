(function () {

	'use strict';
	var controllerId = 'serviceExamCategoriesController';
	angular.module('app').controller(controllerId, serviceExamCategoriesController);
    serviceExamCategoriesController.$inject = ['$state', 'serviceExamCategoryService', 'notificationService', '$location'];

    function serviceExamCategoriesController($state, serviceExamCategoryService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
		vm.serviceExamCategories = [];
        vm.addServiceExamCategory = addServiceExamCategory;
        vm.updateServiceExamCategory = updateServiceExamCategory;
        vm.deleteServiceExamCategory = deleteServiceExamCategory;
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
			serviceExamCategoryService.getServiceExamCategories(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {

                vm.serviceExamCategories = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function addServiceExamCategory() {
            $state.go('service-exam-category-create');			
		}

        function updateServiceExamCategory(serviceExamCategory) {
            $state.go('service-exam-category-modify', { id: serviceExamCategory.serviceExamCategoryId });
		}

        function deleteServiceExamCategory(serviceExamCategory) {	
            serviceExamCategoryService.deleteServiceExamCategory(serviceExamCategory.serviceExamCategoryId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
		}

        function pageChanged() {
            $state.go('service-exam-categories', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });

		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}
	}

})();
