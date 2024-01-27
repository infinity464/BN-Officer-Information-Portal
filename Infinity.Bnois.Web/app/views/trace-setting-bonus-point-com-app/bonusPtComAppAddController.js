/// <reference path="../../services/punishmentsubcategoryservice.js" />


(function () {

    'use strict';
    var controllerId = 'bonusPtComAppAddController';
    angular.module('app').controller(controllerId, bonusPtComAppAddController);
    bonusPtComAppAddController.$inject = ['$stateParams', '$state', 'traceSettingService', 'notificationService'];

    function bonusPtComAppAddController($stateParams, $state, traceSettingService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.bonusPtComAppId = 0;
        vm.traceSettingId = 0;
        vm.bonusPtComApp = {};
        vm.commendations = [];
        vm.title = 'ADD MODE';
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.bonusPtComAppForm = {};
       

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
        }
        if ($stateParams.bonusPtComAppId > 0) {
            vm.bonusPtComAppId = $stateParams.bonusPtComAppId;
            vm.title = 'UPDATE MODE';
        }
        init();
        function init() {
            traceSettingService.getBonusPtComApp(vm.traceSettingId,vm.bonusPtComAppId).then(function (data) {
                vm.bonusPtComApp = data.result.bonusPtComApp;
                vm.commendations = data.result.commendations;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function save() {
            if (vm.bonusPtComAppId > 0 && vm.bonusPtComAppId !== '') {
                updateBonusPtComApp();
            } else {  
                insertBonusPtComApp();
            }
        }
        function insertBonusPtComApp() {
            traceSettingService.saveBonusPtComApp(vm.traceSettingId, vm.bonusPtComApp).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateBonusPtComApp() {
            traceSettingService.updateBonusPtComApp(vm.bonusPtComAppId, vm.bonusPtComApp).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        
        function close() {
            $state.go('trace-setting-tabs.bonus-point-com-apps');
        }
    }

})();
