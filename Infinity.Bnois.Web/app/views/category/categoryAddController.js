(function () {

    'use strict';

    var controllerId = 'categoryAddController';

    angular.module('app').controller(controllerId, categoryAddController);
    categoryAddController.$inject = ['$stateParams', 'categoryService', 'notificationService', '$state'];

    function categoryAddController($stateParams, categoryService, notificationService, $state) {
        var vm = this;
        vm.categoryId = 0;
        vm.title = 'ADD MODE';
        vm.category = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.categoryForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.categoryId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
           categoryService.getCategory(vm.categoryId).then(function (data) {
                vm.category = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.categoryId !== 0 && vm.categoryId !== '') {
                updateCategory();
            } else {
                insertCategory();
            }
        }

        function insertCategory() {
            categoryService.saveCategory(vm.category).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateCategory() {
            categoryService.updateCategory(vm.categoryId, vm.category).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('categories');
        }
    }
})();
