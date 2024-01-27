/// <reference path="../../services/colorService.js" />

(function () {

    'use strict';
	var controllerId = 'purposeController';
	angular.module('app').controller(controllerId, purposeController);
	purposeController.$inject = ['$state', 'purposeService', 'notificationService', '$location'];

	function purposeController($state, purposeService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
		vm.purposes = [];
		vm.addPurpose = addPurpose;
		vm.updatePurpose = updatePurpose;
		vm.deletePurpose = deletePurpose;
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
			purposeService.getPurposes(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
				vm.purposes = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

		function addPurpose() {
			$state.go('purpose-create');
        }

		function updatePurpose(purpose) {
			$state.go('purpose-modify', { id: purpose.purposeId });
        }

		function deletePurpose(purpose) {
			purposeService.deletePurpose(purpose.purposeId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
			$state.go('purposes', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
