(function () {

	'use strict';
	var controllerId = 'publicationCategoriesController';
	angular.module('app').controller(controllerId, publicationCategoriesController);
    publicationCategoriesController.$inject = ['$state', 'publicationCategoryService', 'notificationService', '$location'];

    function publicationCategoriesController($state, publicationCategoryService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
		vm.publicationCategoriess = [];
        vm.addPublicationCategory = addPublicationCategory;
        vm.updatePublicationCategory = updatePublicationCategory;
        vm.deletePublicationCategory = deletePublicationCategory;
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
			publicationCategoryService.getPublicationCategories(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {

                vm.publicationCategories = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function addPublicationCategory() {
            $state.go('publication-category-create');			
		}

        function updatePublicationCategory(publicationCategory) {
            $state.go('publication-category-modify', { id: publicationCategory.publicationCategoryId });
		}

        function deletePublicationCategory(publicationCategory) {	
            publicationCategoryService.deletePublicationCategory(publicationCategory.publicationCategoryId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
		}

        function pageChanged() {
            $state.go('publication-categories', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });

		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}
	}

})();
