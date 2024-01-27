(function () {

    'use strict';

    var controllerId = 'maritalTypesController';
    angular.module('app').controller(controllerId, maritalTypesController);
    maritalTypesController.$inject = ['$state', 'maritalTypeService', 'notificationService', '$location'];

    function maritalTypesController($state, maritalTypeService, notificationService, location) {
     
        /* jshint validthis:true */
        var vm = this;
        vm.maritalTypes = [];
        vm.addMaritalType = addMaritalType;
        vm.updateMaritalType = updateMaritalType;
        vm.deleteMaritalType = deleteMaritalType;
        vm.pageChanged = pageChanged;
        vm.onSearch = onSearch;
        vm.searchText = "";
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
            maritalTypeService.getMaritalTypes(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.maritalTypes = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addMaritalType() {
            $state.go('marital-type-create');
        }

        function updateMaritalType(maritalType) {
         
            $state.go('marital-type-modify', { id: maritalType.maritalTypeId});
        }

        function deleteMaritalType(maritalType) {
            maritalTypeService.deleteMaritalType(maritalType.maritalTypeId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });  
            });
        }

        function pageChanged() {
            $state.go('marital-types', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
