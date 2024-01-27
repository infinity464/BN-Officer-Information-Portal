(function () {

    'use strict';

    var controllerId = 'extracurricularTypesController';
    angular.module('app').controller(controllerId, extracurricularTypesController);
    extracurricularTypesController.$inject = ['$state', 'extracurricularTypeService', 'notificationService', '$location'];

    function extracurricularTypesController($state, extracurricularTypeService, notificationService, location) {
     
        /* jshint validthis:true */
        var vm = this;
        vm.extracurricularTypes = [];
        vm.addExtracurricularType = addExtracurricularType;
        vm.updateExtracurricularType = updateExtracurricularType;
        vm.deleteExtracurricularType = deleteExtracurricularType;
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
            extracurricularTypeService.getExtracurricularTypes(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.extracurricularTypes = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addExtracurricularType() {
            $state.go('extracurricular-type-create');
        }

        function updateExtracurricularType(extracurricularType) {
         
            $state.go('extracurricular-type-modify', { id: extracurricularType.extracurricularTypeId});
        }

        function deleteExtracurricularType(extracurricularType) {
            extracurricularTypeService.deleteExtracurricularType(extracurricularType.extracurricularTypeId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });  
            });
        }

        function pageChanged() {
            $state.go('extracurricular-types', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
