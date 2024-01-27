(function () {

	'use strict';
	var controllerId = 'patternsController';
	angular.module('app').controller(controllerId, patternsController);
	patternsController.$inject = ['$state', 'patternService', 'notificationService', '$location'];

	function patternsController($state, patternService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
		vm.patterns = [];
		vm.addPattern = addPattern;
		vm.updatePattern = updatePattern;
		vm.deletePattern = deletePattern;
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
			patternService.getPatterns(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
				vm.patterns = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function addPattern() {
			$state.go('pattern-create');
		}

		function updatePattern(pattern) {
			
			$state.go('pattern-modify', { id: pattern.patternId });
		}

		function deletePattern(pattern) {
			patternService.deletePattern(pattern.patternId).then(function (data) {
				$state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
			});
		}

		function pageChanged() {
			$state.go('patterns', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}
	}

})();
