(function () {

	'use strict';
	var controllerId = 'visitCategoriesController';
	angular.module('app').controller(controllerId, visitCategoriesController);
    visitCategoriesController.$inject = ['$state', 'visitCategoryService', 'notificationService', '$location'];

    function visitCategoriesController($state, visitCategoryService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
		vm.visitCategories = [];
        vm.addVisitCategory = addVisitCategory;
        vm.updateVisitCategory = updateVisitCategory;
        vm.deleteVisitCategory = deleteVisitCategory;
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
			visitCategoryService.getVisitCategories(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {

                vm.visitCategories = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function addVisitCategory() {
            $state.go('visit-category-create');			
		}

        function updateVisitCategory(visitCategory) {
            $state.go('visit-category-modify', { id: visitCategory.visitCategoryId });
		}

        function deleteVisitCategory(visitCategory) {	
            visitCategoryService.deleteVisitCategory(visitCategory.visitCategoryId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
		}

        function pageChanged() {
            $state.go('visit-categories', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });

		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}
	}

})();
