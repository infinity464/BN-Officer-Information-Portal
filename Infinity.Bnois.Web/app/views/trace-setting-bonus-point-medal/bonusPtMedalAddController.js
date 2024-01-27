/// <reference path="../../services/punishmentsubcategoryservice.js" />


(function () {

    'use strict';
    var controllerId = 'bonusPtMedalAddController';
    angular.module('app').controller(controllerId, bonusPtMedalAddController);
    bonusPtMedalAddController.$inject = ['$stateParams', '$state', 'traceSettingService', 'notificationService'];

    function bonusPtMedalAddController($stateParams, $state, traceSettingService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.bonusPtMedalId = 0;
        vm.traceSettingId = 0;
        vm.bonusPtMedal = {};
        vm.medals = [];
        vm.title = 'ADD MODE';
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.bonusPtMedalForm = {};
       

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
        }
        if ($stateParams.bonusPtMedalId > 0) {
            vm.bonusPtMedalId = $stateParams.bonusPtMedalId;
            vm.title = 'UPDATE MODE';
        }
        init();
        function init() {
            traceSettingService.getBonusPtMedal(vm.traceSettingId,vm.bonusPtMedalId).then(function (data) {
                vm.bonusPtMedal = data.result.bonusPtMedal;
                vm.medals = data.result.medals;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function save() {
            if (vm.bonusPtMedalId > 0 && vm.bonusPtMedalId !== '') {
                updateBonusPtMedal();
            } else {  
                insertBonusPtMedal();
            }
        }
        function insertBonusPtMedal() {
            traceSettingService.saveBonusPtMedal(vm.traceSettingId, vm.bonusPtMedal).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateBonusPtMedal() {
            traceSettingService.updateBonusPtMedal(vm.bonusPtMedalId, vm.bonusPtMedal).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        
        function close() {
            $state.go('trace-setting-tabs.bonus-point-medals');
        }
    }

})();
