(function () {

	'use strict';
	var controllerId = 'eyeVisionsController';
	angular.module('app').controller(controllerId, eyeVisionsController);
    eyeVisionsController.$inject = ['$state', 'eyeVisionService', 'notificationService', '$location'];

    function eyeVisionsController($state, eyeVisionService, notificationService, location) {

		/* jshint validthis:true */
		var vm = this;
		vm.eyeVisions = [];
        vm.addEyeVision = addEyeVision;
        vm.updateEyeVision = updateEyeVision;
        vm.deleteEyeVision = deleteEyeVision;
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
			eyeVisionService.getEyeVisions(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {

                vm.eyeVisions = data.result;
				vm.total = data.total; vm.permission = data.permission;
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function addEyeVision() {
            $state.go('eye-vision-create');			
		}

        function updateEyeVision(eyeVision) {
            $state.go('eye-vision-modify', { id: eyeVision.eyeVisionId });
		}

        function deleteEyeVision(eyeVision) {	
            eyeVisionService.deleteEyeVision(eyeVision.eyeVisionId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
		}

        function pageChanged() {
            $state.go('eye-visions', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });

		}

		function onSearch() {
			vm.pageNumber = 1;
			pageChanged();
		}
	}

})();
