
(function () {

    'use strict';
    var controllerId = 'bonusPtAwardsController';
    angular.module('app').controller(controllerId, bonusPtAwardsController);
    bonusPtAwardsController.$inject = ['$stateParams', '$state', 'traceSettingService', 'notificationService'];

    function bonusPtAwardsController($stateParams, $state, traceSettingService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.traceSettingId = 0;
        vm.bonusPtAwardId = 0;
        vm.bonusPtAwards = [];
        vm.title = 'Course Points';
        vm.addBonusPtAward = addBonusPtAward;
        vm.updateBonusPtAward = updateBonusPtAward;
        
        vm.close = close;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
        }

        init();
        function init() {
            traceSettingService.getBonusPtAwards(vm.traceSettingId).then(function (data) {
                vm.bonusPtAwards = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function addBonusPtAward() {
            $state.go('trace-setting-tabs.bonus-point-award-create', { traceSettingId: vm.traceSettingId,bonusPtAwardId: vm.bonusPtAwardId });
        }
        
        function updateBonusPtAward(bonusPtAward) {
            $state.go('trace-setting-tabs.bonus-point-award-modify', { traceSettingId: vm.traceSettingId, bonusPtAwardId: bonusPtAward.bonusPtAwardId });
        }
      
        function close() {
            $state.go('trace-setting-tabs.bonus-point-awards')
        }
    }

})();
