(function () {

	'use strict';
	var controllerId = 'zonesController';
	angular.module('app').controller(controllerId, zonesController);
    zonesController.$inject = ['$state', 'zoneService', 'notificationService', '$location'];

    function zonesController($state, zoneService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
		vm.zones = [];
        vm.addZone = addZone;
        vm.updateZone = updateZone;
        vm.deleteZone = deleteZone;
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
			zoneService.getZones(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {

                vm.zones = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function addZone() {
            $state.go('zone-create');			
		}

		function updateZone(zone) {
            $state.go('zone-modify', { id: zone.zoneId });
		}

		function deleteZone(zone) {	
            zoneService.deleteZone(zone.zoneId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
		}

        function pageChanged() {
            $state.go('zones', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });

		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}
	}

})();
