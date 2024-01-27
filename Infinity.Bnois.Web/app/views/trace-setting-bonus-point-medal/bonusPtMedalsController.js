
(function () {

    'use strict';
    var controllerId = 'bonusPtMedalsController';
    angular.module('app').controller(controllerId, bonusPtMedalsController);
    bonusPtMedalsController.$inject = ['$stateParams', '$state', 'traceSettingService', 'notificationService'];

    function bonusPtMedalsController($stateParams, $state, traceSettingService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.traceSettingId = 0;
        vm.bonusPtMedalId = 0;
        vm.bonusPtMedals = [];
        vm.title = 'Course Points';
        vm.addBonusPtMedal = addBonusPtMedal;
        vm.updateBonusPtMedal = updateBonusPtMedal;
        
        vm.close = close;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
        }

        init();
        function init() {
            traceSettingService.getBonusPtMedals(vm.traceSettingId).then(function (data) {
                vm.bonusPtMedals = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function addBonusPtMedal() {
            $state.go('trace-setting-tabs.bonus-point-medal-create', { traceSettingId: vm.traceSettingId,bonusPtMedalId: vm.bonusPtMedalId });
        }
        
        function updateBonusPtMedal(bonusPtMedal) {
            $state.go('trace-setting-tabs.bonus-point-medal-modify', { traceSettingId: vm.traceSettingId, bonusPtMedalId: bonusPtMedal.bonusPtMedalId });
        }
      
        function close() {
            $state.go('trace-setting-tabs.bonus-point-medals')
        }
    }

})();
