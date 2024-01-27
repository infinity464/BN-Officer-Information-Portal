

(function () {

    'use strict';

    var controllerId = 'extracurricularTypeAddController';

    angular.module('app').controller(controllerId, extracurricularTypeAddController);
    extracurricularTypeAddController.$inject = ['$stateParams', 'extracurricularTypeService', 'notificationService', '$state'];

    function extracurricularTypeAddController($stateParams, extracurricularTypeService, notificationService, $state) {
        var vm = this;
        vm.extracurricularTypeId = 0;
        vm.title = 'ADD MODE';
        vm.extracurricularType = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.extracurricularTypeTypeForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.extracurricularTypeId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            extracurricularTypeService.getExtracurricularType(vm.extracurricularTypeId).then(function (data) {
                vm.extracurricularType = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.extracurricularTypeId !== 0 && vm.extracurricularTypeId !== '') {
                updateExtracurricularType();
            } else {
                insertExtracurricularType();
            }
        }

        function insertExtracurricularType() {
            extracurricularTypeService.saveExtracurricularType(vm.extracurricularType).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateExtracurricularType() {
            extracurricularTypeService.updateExtracurricularType(vm.extracurricularTypeId, vm.extracurricularType).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('extracurricular-types');
        }

    }
})();
