(function () {

    'use strict';
    var controllerId = 'resultAddController';
    angular.module('app').controller(controllerId, resultAddController);
    resultAddController.$inject = ['$stateParams', 'resultService', 'notificationService', '$state'];

    function resultAddController($stateParams, resultService, notificationService, $state) {
        var vm = this;
        vm.resultId = 0;
        vm.title = 'ADD MODE';
        vm.result = {};
        vm.examCategories = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.resultForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.resultId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            resultService.getResult(vm.resultId).then(function (data) {
                vm.result = data.result.result;
                vm.examCategories = data.result.examCategories;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.resultId !== 0) {
                updateResult();
            } else {
                insertResult();
            }
        }

        function insertResult() {
            resultService.saveResult(vm.result).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateResult() {
            resultService.updateResult(vm.resultId, vm.result).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('results');
        }

    }
})();
