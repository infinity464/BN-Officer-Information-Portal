/// <reference path="../../services/punishmentsubcategoryservice.js" />
(function () {

    'use strict';
    var controllerId = 'ptDeductPunishmentsController';
    angular.module('app').controller(controllerId, ptDeductPunishmentsController);
    ptDeductPunishmentsController.$inject = ['$stateParams', '$state', 'traceSettingService','notificationService'];

    function ptDeductPunishmentsController($stateParams, $state, traceSettingService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.traceSettingId = 0;
        vm.ptDeductPunishmentId = 0;
        vm.ptDeductPunishments = [];
        vm.title = 'Course Points';
        vm.addPtDeductPunishment = addPtDeductPunishment;
        vm.updatePtDeductPunishment = updatePtDeductPunishment;
       
        
        vm.close = close;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
        }

        init();
        function init() {
            traceSettingService.getPtDeductPunishments(vm.traceSettingId).then(function (data) {
                vm.ptDeductPunishments = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function addPtDeductPunishment() {
            $state.go('trace-setting-tabs.point-deduction-punishment-create', { traceSettingId: vm.traceSettingId,ptDeductPunishmentId: vm.ptDeductPunishmentId });
        }
        
        function updatePtDeductPunishment(ptDeductPunishment) {
            $state.go('trace-setting-tabs.point-deduction-punishment-modify', { traceSettingId: vm.traceSettingId, ptDeductPunishmentId: ptDeductPunishment.ptDeductPunishmentId });
        }
      
        function close() {
            $state.go('trace-setting-tabs.point-deduction-punishments')
        }

      
    }

})();
