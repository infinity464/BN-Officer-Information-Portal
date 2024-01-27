/// <reference path="../../services/upazilaService.js" />
(function () {

    'use strict';

    var controllerId = 'upazilaAddController';

    angular.module('app').controller(controllerId, upazilaAddController);
    upazilaAddController.$inject = ['$stateParams', 'upazilaService', 'notificationService', '$state'];

    function upazilaAddController($stateParams, upazilaService, notificationService, $state) {
        var vm = this;
        vm.upazilaId = 0;
        vm.title = 'ADD MODE';
        vm.upazila = {};
        vm.districts = [];
        vm.divisions = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.upazilaForm = {};
        vm.getDistrictByDivision = getDistrictByDivision;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.upazilaId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            upazilaService.getUpazila(vm.upazilaId).then(function (data) {
                vm.upazila = data.result.upazila;
                vm.districts = data.result.districts;
                 vm.divisions = data.result.divisions;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.upazilaId !== 0 && vm.upazilaId !== '') {
                updateUpazila();
            } else {
                insertUpazila();
            }
        }

        function insertUpazila() {
            upazilaService.saveUpazila(vm.upazila).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateUpazila() {
            upazilaService.updateUpazila(vm.upazilaId, vm.upazila).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('upazilas');
        }



        function getDistrictByDivision(divisionId) {
            upazilaService.getDistrictByDivision(divisionId).then(function (data) {
                vm.districts = data.result.districts;
            });
        }
    }
})();
