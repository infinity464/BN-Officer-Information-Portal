/// <reference path="../../services/punishmentsubcategoryservice.js" />


(function () {

    'use strict';
    var controllerId = 'bonusPtPublicAddController';
    angular.module('app').controller(controllerId, bonusPtPublicAddController);
    bonusPtPublicAddController.$inject = ['$stateParams', '$state', 'traceSettingService', 'notificationService'];

    function bonusPtPublicAddController($stateParams, $state, traceSettingService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.bonusPtPublicId = 0;
        vm.traceSettingId = 0;
        vm.bonusPtPublic = {};
        vm.publicationCategories = [];
        vm.title = 'ADD MODE';
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.bonusPtPublicForm = {};
       

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
        }
        if ($stateParams.bonusPtPublicId > 0) {
            vm.bonusPtPublicId = $stateParams.bonusPtPublicId;
            vm.title = 'UPDATE MODE';
        }
        init();
        function init() {
            traceSettingService.getBonusPtPublic(vm.traceSettingId,vm.bonusPtPublicId).then(function (data) {
                vm.bonusPtPublic = data.result.bonusPtPublic;
                vm.publicationCategories = data.result.publicationCategories;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function save() {
            if (vm.bonusPtPublicId > 0 && vm.bonusPtPublicId !== '') {
                updateBonusPtPublic();
            } else {  
                insertBonusPtPublic();
            }
        }
        function insertBonusPtPublic() {
            traceSettingService.saveBonusPtPublic(vm.traceSettingId, vm.bonusPtPublic).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateBonusPtPublic() {
            traceSettingService.updateBonusPtPublic(vm.bonusPtPublicId, vm.bonusPtPublic).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        
        function close() {
            $state.go('trace-setting-tabs.bonus-point-publications');
        }
    }

})();
