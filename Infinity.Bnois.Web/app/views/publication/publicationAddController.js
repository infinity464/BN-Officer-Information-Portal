/// <reference path="../../services/publicationService.js" />
(function () {

    'use strict';

    var controllerId = 'publicationAddController';

    angular.module('app').controller(controllerId, publicationAddController);
    publicationAddController.$inject = ['$stateParams', 'publicationService', 'notificationService', '$state'];

    function publicationAddController($stateParams, publicationService, notificationService, $state) {
        var vm = this;
        vm.publicationId = 0;
        vm.title = 'ADD MODE';
        vm.publication = {};
        vm.publications = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.publicationForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.publicationId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            publicationService.getPublication(vm.publicationId).then(function (data) {
                vm.publication = data.result.publication;
                vm.publicationCategories = data.result.publicationCategories;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.publicationId !== 0 && vm.publicationId !== '') {
                updatePublication();
            } else {
                insertPublication();
            }
        }

        function insertPublication() {
            publicationService.savePublication(vm.publication).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updatePublication() {
            publicationService.updatePublication(vm.publicationId, vm.publication).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('publications');
        }
    }
})();
