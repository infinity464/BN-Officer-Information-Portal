(function () {

    'use strict';

    var controllerId = 'bulkUserController';

    angular.module('app').controller(controllerId, bulkUserController);
    bulkUserController.$inject = ['$stateParams', 'codeValue', 'bulkUserService','userService', 'notificationService', '$state', '$location'];

    function bulkUserController($stateParams, codeValue, bulkUserService, userService, notificationService, $state, location) {
        var vm = this;
        vm.users = {};
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 5000;
        vm.pageNumber = 1;
        vm.total = 0;
        //vm.selectedUser = {};
        //vm.allSelect = allSelect;
        vm.selected = [];
        vm.exist = exist;
        vm.toggleSelection = toggleSelection;
        vm.selectDeselectAll = selectDeselectAll;
        vm.saveUser = saveUser;

        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
            vm.searchText = location.search().q;
        }
        Init();
        function Init() {
            bulkUserService.UserList(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.users = data.result;
                vm.total = data.total;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function saveUser() {
            userService.saveBulkUser(vm.selected).then(function (data) {
                notificationService.displaySuccess("User created successfully");
                pageChanged();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };


        function exist(item) {
            return vm.selected.findIndex(x => x.pNo == item.pNo) > -1;
        }
        function toggleSelection(item) {
            var idx = vm.selected.findIndex(x => x.pNo == item.pNo);
            if (idx > -1) {
                vm.selected.splice(idx, 1);
            } else {
                vm.selected.push(item);
            }
        };
        function selectDeselectAll() {
            if ($('#checkbox').is(":checked")) {

                for (var i = 0; i < vm.users.length; i++) {
                    var idx = vm.selected.findIndex(x => x.pNo == vm.users[i].pNo);
                    if (idx < 0) {
                        vm.selected.push(vm.users[i]);
                    }
                }
                console.log(vm.selected);
            } else {
                vm.selected = [];
                console.log(vm.selected);

            }
        };

        function pageChanged() {
            $state.go('users-create', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });

        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }




    }
})();