(function () {

    'use strict';

	var controllerId = 'examinationsController';
	angular.module('app').controller(controllerId, examinationsController);
	examinationsController.$inject = ['$state', 'examinationService', 'notificationService', '$location'];

    function examinationsController($state, examinationService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
		vm.examinations = [];
		vm.addExamination = addExamination;
		vm.updateExamination = updateExamination;
        vm.deleteExamination = deleteExamination;

        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;

        vm.total = 0;

        if (location.search().ps !== undefined && location.search().ps != null && location.search().ps != '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn != null && location.search().pn != '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q != null && location.search().q != '') {
            vm.searchText = location.search().q;
        }
        init();
        function init() {
            examinationService.getExaminations(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
				vm.examinations = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

		function addExamination() {
		$state.go('examination-create');
        }

		function updateExamination(examination) {
            $state.go('examination-modify', { id: examination.examinationId });
        }

		function deleteExamination(examination) {
			examinationService.deleteExamination(examination.examinationId).then(function (data) {
                init();
            });
        }

        function pageChanged() {
			var url = location.url('/examinations');
            location.path(url.$$url).search('pn', vm.pageNumber).search('ps', vm.pageSize).search('q', vm.searchText);
        }
        
        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }

        function pageChanged() {
            $state.go('examinations', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

    }

})();
