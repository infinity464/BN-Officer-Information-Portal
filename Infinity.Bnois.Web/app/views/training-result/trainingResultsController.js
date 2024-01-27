

(function () {

    'use strict';
    var controllerId = 'trainingResultsController';
    angular.module('app').controller(controllerId, trainingResultsController);
    trainingResultsController.$inject = ['$state', 'downloadService','trainingResultService', 'notificationService', '$location'];

    function trainingResultsController($state, downloadService,trainingResultService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.trainingResults = [];
        vm.addTrainingResult = addTrainingResult;
        vm.updateTrainingResult = updateTrainingResult;
        vm.deleteTrainingResult = deleteTrainingResult;
        vm.trainingResultUpload = trainingResultUpload;
        vm.trainingResultDownload = trainingResultDownload;
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
            trainingResultService.getTrainingResults(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.trainingResults = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addTrainingResult() {
            $state.go('training-result-create');
        }

        function updateTrainingResult(trainingResult) {
            $state.go('training-result-modify', { id: trainingResult.trainingResultId });
        }

        function deleteTrainingResult(trainingResult) {
            trainingResultService.deleteTrainingResult(trainingResult.trainingResultId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }


        function trainingResultUpload(trainingResult) {
            $state.go('training-result-upload', { id: trainingResult.trainingResultId});
        }

        function trainingResultDownload(trainingResult) {
            var url = trainingResultService.trainingResultDownload(trainingResult.trainingResultId);
            downloadService.downloadFile(url);
        }

        function pageChanged() {
            $state.go('training-results', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }



        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }


    }

})();

