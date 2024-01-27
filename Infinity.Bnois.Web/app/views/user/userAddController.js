(function () {

    'use strict';

    var controllerId = 'UserAddController';

    angular.module('app').controller(controllerId, userAddController);

    userAddController.$inject = ['$stateParams', 'userService', '$state', 'notificationService'];

    function userAddController($stateParams, userService, $state, notificationService) {

        var vm = this;
        vm.title = 'ADD MODE';
        vm.user = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.userForm = {};
        vm.userId = '';
        vm.languages = [];

        if ($stateParams.userId !== undefined && $stateParams.userId !==null) {
            vm.userId = $stateParams.userId;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        init();

        function init() {
            userService.getUser(vm.userId).then(function (data) {
                vm.user = data.result.user;
                    vm.languages = data.result.languages;
                },
            function (errorMessage) {
                notificationService.displayError(errorMessage.message);
            });
        };

        function save() {
            if (vm.userId !== '' && vm.userId !== null) {
                updateUser();
            } else {
                insertUser();
            }
        }

        function insertUser() {
            userService.saveUser(vm.user).then(function (data) {
                notificationService.displaySuccess("User added successfully");
                    close();
            },
              function (errorMessage) {
                  notificationService.displayError(errorMessage.message);
              });
        }

        function updateUser() {
            userService.updateUser(vm.userId, vm.user).then(function (data) {
                notificationService.displaySuccess("User update successfully");
                    close();
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('users');
        }
    }
})();
