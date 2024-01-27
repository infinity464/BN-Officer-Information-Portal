(function () {

    'use strict';

    var controllerId = 'RolesController';

    angular.module('app').controller(controllerId, rolesController);

    rolesController.$inject = ['$state', 'roleService', '$location', 'notificationService'];

    function rolesController($state, roleService, location, notificationService) {

        /* jshint validthis:true */
        var vm = this;

        vm.roles = {};
        vm.addRole = addRole;
        vm.deleteRole = deleteRole;
        vm.updateRole = updateRole;
        vm.addRoleFeatures = addRoleFeatures;
        vm.searchText = '';
        vm.searchRole = searchRole;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;
        vm.pageChanged = pageChanged;

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
            roleService.getRoles(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.roles = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage);
                });
        }

        function addRole() {
            $state.go('role-create');
        }
        function addRoleFeatures(role) {
            $state.go('role-features', { roleId: role.id});
        }

        function updateRole(role) {
            $state.go('role-modify', { roleId: role.id });
        }

        function deleteRole(role) {
            roleService.deleteRole(role.id).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('roles', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function searchRole() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }
})();