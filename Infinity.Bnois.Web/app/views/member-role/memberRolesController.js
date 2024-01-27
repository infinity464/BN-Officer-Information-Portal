(function () {

    'use strict';

    var controllerId = 'memberRolesController';
    angular.module('app').controller(controllerId, memberRolesController);
    memberRolesController.$inject = ['$state', 'memberRoleService', 'notificationService', '$location'];

    function memberRolesController($state, memberRoleService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.memberRoles = [];
        vm.addMemberRole = addMemberRole;
        vm.updateMemberRole = updateMemberRole;
        vm.deleteMemberRole = deleteMemberRole;
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
            memberRoleService.getMemberRoles(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.memberRoles = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addMemberRole() {
            $state.go('member-role-create');
        }

        function updateMemberRole(memberRole) {
            $state.go('member-role-modify', { id: memberRole.memberRoleId});
        }

        function deleteMemberRole(memberRole) {
            memberRoleService.deleteMemberRole(memberRole.memberRoleId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });  
            });
        }
            
        function pageChanged() {
            $state.go('member-roles', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
