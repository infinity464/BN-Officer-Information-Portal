

(function () {

    'use strict';
    var controllerId = 'foreignProjectsController';
    angular.module('app').controller(controllerId, foreignProjectsController);
    foreignProjectsController.$inject = ['$state', 'foreignProjectService', 'notificationService', '$location'];

    function foreignProjectsController($state, foreignProjectService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.foreignProjects = [];
        vm.addForeignProject = addForeignProject;
        vm.updateForeignProject = updateForeignProject;
        vm.deleteForeignProject = deleteForeignProject;
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
            foreignProjectService.getForeignProjects(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.foreignProjects = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addForeignProject() {
            $state.go('foreign-project-create');
        }

        function updateForeignProject(foreignProject) {
            $state.go('foreign-project-modify', { id: foreignProject.foreignProjectId });
        }

        function deleteForeignProject(foreignProject) {
            foreignProjectService.deleteForeignProject(foreignProject.foreignProjectId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('foreign-projects', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();

