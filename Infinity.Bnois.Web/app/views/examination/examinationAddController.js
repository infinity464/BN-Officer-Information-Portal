(function () {
    'use strict';
    var controllerId = 'examinationAddController';
    angular.module('app').controller(controllerId, examinationAddController);
    examinationAddController.$inject = ['$stateParams', 'examinationService', 'notificationService', '$state'];

    function examinationAddController(stateParams, examinationService, notificationService, $state) {
        var vm = this;
        vm.examinationId = 0;
        vm.title = 'ADD MODE';
        vm.examination = {};
        vm.examCategories = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.examinationForm = {};

        if (stateParams.id !== undefined && stateParams.id !== '') {
            vm.examinationId = stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            examinationService.getExamination(vm.examinationId).then(function (data) {
                vm.examination = data.result.examination;
                vm.examCategories = data.result.examCategories;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.examinationId !== 0) {
                updateExamination();
            } else {
                insertExamination();
            }
        }

        function insertExamination() {
            examinationService.saveExamination(vm.examination).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateExamination() {
            examinationService.updateExamination(vm.examinationId, vm.examination).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
           $state.go('examinations');
        }

    }
})();
