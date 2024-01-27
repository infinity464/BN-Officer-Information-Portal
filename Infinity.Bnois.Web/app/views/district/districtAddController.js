/// <reference path="../../services/districtService.js" />
(function () {

    'use strict';

    var controllerId = 'districtAddController';

    angular.module('app').controller(controllerId, districtAddController);
    districtAddController.$inject = ['$stateParams', 'districtService', 'notificationService', '$state'];

    function districtAddController($stateParams, districtService, notificationService, $state) {
        var vm = this;
        vm.districtId = 0;
        vm.title = 'ADD MODE';
        vm.district = {};
        vm.districts = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.districtForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.districtId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            districtService.getDistrict(vm.districtId).then(function (data) {
                vm.district = data.result.district;
                vm.districts = data.result.districts;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.districtId !== 0 && vm.districtId !== '') {
                updateDistrict();
            } else {
                insertDistrict();
            }
        }

        function insertDistrict() {
            districtService.saveDistrict(vm.district).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateDistrict() {
            districtService.updateDistrict(vm.districtId, vm.district).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('districts');
        }
    }
})();
