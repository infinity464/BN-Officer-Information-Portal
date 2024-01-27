(function () {

    'use strict';
    var controllerId = 'trainingInstitutesController';
    angular.module('app').controller(controllerId, trainingInstitutesController);
    trainingInstitutesController.$inject = ['$state', 'trainingInstituteService', 'notificationService', '$location'];

    function trainingInstitutesController($state, trainingInstituteService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.trainingInstitutes = [];
        vm.addTrainingInstitute = addTrainingInstitute;
        vm.updateTrainingInstitute = updateTrainingInstitute;
        vm.deleteTrainingInstitute = deleteTrainingInstitute;
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
            trainingInstituteService.getTrainingInstitutes(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.trainingInstitutes = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addTrainingInstitute() {
            $state.go('training-institute-create');
        }

        function updateTrainingInstitute(trainingInstitute) {
            $state.go('training-institute-modify', { id: trainingInstitute.instituteId});
        }

        function deleteTrainingInstitute(trainingInstitute) {
            trainingInstituteService.deleteTrainingInstitute(trainingInstitute.instituteId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('training-institutes', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
