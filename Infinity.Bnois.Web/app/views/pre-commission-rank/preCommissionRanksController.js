(function () {

	'use strict';
	var controllerId = 'preCommissionRanksController';
	angular.module('app').controller(controllerId, preCommissionRanksController);
    preCommissionRanksController.$inject = ['$state', 'preCommissionRankService', 'notificationService', '$location'];

    function preCommissionRanksController($state, preCommissionRankService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
		vm.preCommissionRanks = [];
        vm.addPreCommissionRank = addPreCommissionRank;
        vm.updatePreCommissionRank = updatePreCommissionRank;
        vm.deletePreCommissionRank = deletePreCommissionRank;
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
			preCommissionRankService.getPreCommissionRanks(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {

                vm.preCommissionRanks = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function addPreCommissionRank() {
            $state.go('pre-commission-rank-create');			
		}

        function updatePreCommissionRank(preCommissionRank) {
            $state.go('pre-commission-rank-modify', { id: preCommissionRank.preCommissionRankId });
		}

        function deletePreCommissionRank(preCommissionRank) {	
            preCommissionRankService.deletePreCommissionRank(preCommissionRank.preCommissionRankId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
		}

        function pageChanged() {
            $state.go('pre-commission-ranks', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });

		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}
	}

})();
