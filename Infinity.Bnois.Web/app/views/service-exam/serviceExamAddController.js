/// <reference path="../../services/serviceExamService.js" />
(function () {

    'use strict';

    var controllerId = 'serviceExamAddController';

    angular.module('app').controller(controllerId, serviceExamAddController);
    serviceExamAddController.$inject = ['$stateParams', 'serviceExamService', 'notificationService', '$state'];

    function serviceExamAddController($stateParams, serviceExamService, notificationService, $state) {
        var vm = this;
        vm.serviceExamId = 0;
        vm.title = 'ADD MODE';
        vm.serviceExam = {};
        vm.serviceExamCategories = [];
        vm.branches = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.serviceExamForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.serviceExamId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            serviceExamService.getServiceExam(vm.serviceExamId).then(function (data) {
                vm.serviceExam = data.result.serviceExam;
                vm.serviceExamCategories = data.result.serviceExamCategories;
                vm.branches = data.result.branches;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.serviceExamId !== 0 && vm.serviceExamId !== '') {
                updateServiceExam();
            } else {
                insertServiceExam();
            }
        }

        function insertServiceExam() {
            serviceExamService.saveServiceExam(vm.serviceExam).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateServiceExam() {
            serviceExamService.updateServiceExam(vm.serviceExamId, vm.serviceExam).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('service-exams');
        }
    }
})();
