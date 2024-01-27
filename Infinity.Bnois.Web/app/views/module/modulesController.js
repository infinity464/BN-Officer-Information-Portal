(function () {

    'use strict';

    var controllerId = 'modulesController';
    angular.module('app').controller(controllerId, modulesController);
    modulesController.$inject = ['$state', 'downloadService', 'moduleService', 'notificationService', '$location'];

    function modulesController($state, downloadService, moduleService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.modules = [];
        vm.addModule = addModule;
        vm.updateModule = updateModule;
        vm.deleteModule = deleteModule;
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
            moduleService.getModules(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.modules = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addModule() {
           
            $state.go("module-create")
        }

        function updateModule(module) {

            $state.go("module-modify", { moduleId: module.moduleId})
        }

        function deleteModule(module) {
            moduleService.deleteModule(module.moduleId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('modules', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }


       

    }

})();
