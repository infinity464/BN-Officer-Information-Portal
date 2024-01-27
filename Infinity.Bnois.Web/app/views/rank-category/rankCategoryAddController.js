(function () {

    'use strict';

    var controllerId = 'rankCategoryAddController';

    angular.module('app').controller(controllerId, rankCategoryAddController);
    rankCategoryAddController.$inject = ['$stateParams', 'rankCategoryService', 'notificationService', '$state'];

    function rankCategoryAddController($stateParams, rankCategoryService, notificationService, $state) {
        var vm = this;
        vm.rankCategoryId = 0;
        vm.title = 'ADD MODE';
        vm.rankCategory = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.rankCategoryForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.rankCategoryId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            rankCategoryService.getRankCategory(vm.rankCategoryId).then(function (data) {
                vm.rankCategory = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.rankCategoryId !== 0 && vm.rankCategoryId !== '') {
                updateRankCategory();
            } else {
                insertRankCategory();
            }
        }

        function insertRankCategory() {
            rankCategoryService.saveRankCategory(vm.rankCategory).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateRankCategory() {
            rankCategoryService.updateRankCategory(vm.rankCategoryId, vm.rankCategory).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('rank-categories');
        }
    }
})();
