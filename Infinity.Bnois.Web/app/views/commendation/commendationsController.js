(function () {

	'use strict';
	var controllerId = 'commendationsController';
	angular.module('app').controller(controllerId, commendationsController);
    commendationsController.$inject = ['$state', 'commendationService', 'notificationService', '$location'];

    function commendationsController($state, commendationService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
		vm.commendations = [];
        vm.addCommendation = addCommendation;
        vm.updateCommendation = updateCommendation;
        vm.deleteCommendation = deleteCommendation;
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
			commendationService.getCommendations(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {

                vm.commendations = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function addCommendation() {
            $state.go('commendation-create');			
		}

        function updateCommendation(commendation) {
            $state.go('commendation-modify', { id: commendation.commendationId });
		}

        function deleteCommendation(commendation) {	
            commendationService.deleteCommendation(commendation.commendationId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
		}

        function pageChanged() {
            $state.go('commendations', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });

		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}
	}

})();
