(function () {

    'use strict';

    var controllerId = 'resultGradeAddController';

    angular.module('app').controller(controllerId, resultGradeAddController);
    resultGradeAddController.$inject = ['$stateParams', 'resultGradeService', 'notificationService', '$state'];

    function resultGradeAddController($stateParams, resultGradeService, notificationService, $state) {
        var vm = this;
        vm.resultGradeId = 0;
        vm.title = 'ADD MODE';
        vm.resultGrade = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.resultGradeForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.resultGradeId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            resultGradeService.getResultGrade(vm.resultGradeId).then(function (data) {
                vm.resultGrade = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.resultGradeId !== 0 && vm.resultGradeId !== '') {
                updateResultGrade();
            } else {
                insertResultGrade();
            }
        }

        function insertResultGrade() {
            resultGradeService.saveResultGrade(vm.resultGrade).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateResultGrade() {
            resultGradeService.updateResultGrade(vm.resultGradeId, vm.resultGrade).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('result-grades');
        }
    }
})();
