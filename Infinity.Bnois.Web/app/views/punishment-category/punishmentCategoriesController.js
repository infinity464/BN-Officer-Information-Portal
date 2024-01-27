(function () {

	'use strict';
	var controllerId = 'punishmentCategoriesController';
	angular.module('app').controller(controllerId, punishmentCategoriesController);
    punishmentCategoriesController.$inject = ['$state', 'punishmentCategoryService', 'notificationService', '$location'];

    function punishmentCategoriesController($state, punishmentCategoryService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
		vm.punishmentCategories = [];
        vm.addPunishmentCategory = addPunishmentCategory;
        vm.updatePunishmentCategory = updatePunishmentCategory;
        vm.deletePunishmentCategory = deletePunishmentCategory;
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
			punishmentCategoryService.getPunishmentCategories(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {

                vm.punishmentCategories = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function addPunishmentCategory() {
            $state.go('punishment-category-create');			
		}

        function updatePunishmentCategory(punishmentCategory) {
            $state.go('punishment-category-modify', { id: punishmentCategory.punishmentCategoryId });
		}

        function deletePunishmentCategory(punishmentCategory) {	
            punishmentCategoryService.deletePunishmentCategory(punishmentCategory.punishmentCategoryId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
		}

        function pageChanged() {
            $state.go('punishment-categories', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });

		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}
	}

})();
