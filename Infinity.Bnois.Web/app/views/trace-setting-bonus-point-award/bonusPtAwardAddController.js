/// <reference path="../../services/punishmentsubcategoryservice.js" />


(function () {

    'use strict';
    var controllerId = 'bonusPtAwardAddController';
    angular.module('app').controller(controllerId, bonusPtAwardAddController);
    bonusPtAwardAddController.$inject = ['$stateParams', '$state', 'traceSettingService', 'notificationService'];

    function bonusPtAwardAddController($stateParams, $state, traceSettingService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.bonusPtAwardId = 0;
        vm.traceSettingId = 0;
        vm.bonusPtAward = {};
        vm.awards = [];
        vm.title = 'ADD MODE';
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.bonusPtAwardForm = {};
       

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
        }
        if ($stateParams.bonusPtAwardId > 0) {
            vm.bonusPtAwardId = $stateParams.bonusPtAwardId;
            vm.title = 'UPDATE MODE';
        }
        init();
        function init() {
            traceSettingService.getBonusPtAward(vm.traceSettingId,vm.bonusPtAwardId).then(function (data) {
                vm.bonusPtAward = data.result.bonusPtAward;
                console.log(vm.bonusPtAward );
                vm.awards = data.result.awards;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function save() {
            if (vm.bonusPtAwardId > 0 && vm.bonusPtAwardId !== '') {
                updateBonusPtAward();
            } else {  
                insertBonusPtAward();
            }
        }
        function insertBonusPtAward() {
            traceSettingService.saveBonusPtAward(vm.traceSettingId, vm.bonusPtAward).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateBonusPtAward() {
            traceSettingService.updateBonusPtAward(vm.bonusPtAwardId, vm.bonusPtAward).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        
        function close() {
            $state.go('trace-setting-tabs.bonus-point-awards');
        }
    }

})();
