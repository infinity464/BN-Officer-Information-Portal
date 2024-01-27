(function () {

    'use strict';

    var controllerId = 'rankAddController';

    angular.module('app').controller(controllerId, rankAddController);
    rankAddController.$inject = ['$stateParams', 'rankService', 'notificationService', '$state'];

    function rankAddController($stateParams, rankService, notificationService, $state) {
        var vm = this;
        vm.rankId = 0;
        vm.title = 'ADD MODE';
        vm.rank = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.rankForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.rankId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            rankService.getRank(vm.rankId).then(function (data) {
                vm.rank = data.result.rank;
                if (vm.rank.rankId<=0) {
                    vm.rank.isConfirm = true;
                }
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.rankId !== 0 && vm.rankId !== '') {
                updateRank();
            } else {
                insertRank();
            }
        }

        function insertRank() {
            rankService.saveRank(vm.rank).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateRank() {
            rankService.updateRank(vm.rankId, vm.rank).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('ranks');
        }
    }
})();
