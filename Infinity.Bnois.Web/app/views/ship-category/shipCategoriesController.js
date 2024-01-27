(function () {

	'use strict';
	var controllerId = 'shipCategoriesController';
	angular.module('app').controller(controllerId, shipCategoriesController);
    shipCategoriesController.$inject = ['$state', 'shipCategoryService', 'notificationService', '$location'];

    function shipCategoriesController($state, shipCategoryService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
		vm.shipCategories = [];
        vm.addShipCategory = addShipCategory;
        vm.updateShipCategory = updateShipCategory;
        vm.deleteShipCategory = deleteShipCategory;
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
			shipCategoryService.getShipCategories(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {

                vm.shipCategories = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function addShipCategory() {
            $state.go('ship-category-create');			
		}

        function updateShipCategory(shipCategory) {
            $state.go('ship-category-modify', { id: shipCategory.shipCategoryId });
		}

        function deleteShipCategory(shipCategory) {	
            shipCategoryService.deleteShipCategory(shipCategory.shipCategoryId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
		}

        function pageChanged() {
            $state.go('ship-categories', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });

		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}
	}

})();
