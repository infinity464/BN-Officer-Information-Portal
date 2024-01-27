(function () {

    'use strict';

    var controllerId = 'featuresController';
    angular.module('app').controller(controllerId, featuresController);
    featuresController.$inject = ['$state', 'downloadService', 'featureService', 'notificationService', '$location'];

    function featuresController($state, downloadService, featureService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.features = [];
        vm.addFeature = addFeature;
        vm.updateFeature = updateFeature;
        vm.deleteFeature = deleteFeature;
        vm.pageChanged = pageChanged;
        vm.onSearch = searchFeature;
        vm.searchText = "";
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;
       
        if (location.search().ps !== undefined && location.search().ps != null && location.search().ps!= '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn != null && location.search().pn!= '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q != null && location.search().q!= '') {
            vm.searchText = location.search().q;
        }
        init();
        function init() {
            featureService.getFeatures(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.features = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addFeature() {
            $state.go('feature-create');
        }

        function updateFeature(feature) {
           
            $state.go('feature-modify', { featureId: feature.featureId});
        }

        function deleteFeature(feature) {
            featureService.deleteFeature(feature.featureId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('features', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function searchFeature() {
            vm.pageNumber = 1;
            pageChanged();
        }

     
    }

})();
