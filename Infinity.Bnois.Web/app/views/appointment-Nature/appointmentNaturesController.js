(function () {

    'use strict';
	var controllerId = 'appointmentNaturesController';
	angular.module('app').controller(controllerId, appointmentNaturesController);
	appointmentNaturesController.$inject = ['$state', 'appointmentNatureService', 'notificationService', '$location'];

	function appointmentNaturesController($state, appointmentNatureService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
		vm.appointmentNatures = [];
		vm.addAppointmentNature = addAppointmentNature;
		vm.updateAppointmentNature = updateAppointmentNature;
		vm.deleteAppointmentNature = deleteAppointmentNature;
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
			appointmentNatureService.getAppointmentNatures(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
				vm.appointmentNatures = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

		function addAppointmentNature() {
			$state.go('appointmentNature-create');
        }

		function updateAppointmentNature(appointmentNature) {
			
			$state.go('appointmentNature-modify', { id: appointmentNature.aNatId});
        }

		function deleteAppointmentNature(appointmentNature) {
			appointmentNatureService.deleteAppointmentNature(appointmentNature.aNatId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
			$state.go('appointmentNatures', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
