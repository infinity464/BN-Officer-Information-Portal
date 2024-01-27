
(function () {

    'use strict';
    var controllerId = 'bonusPtPublicsController';
    angular.module('app').controller(controllerId, bonusPtPublicsController);
    bonusPtPublicsController.$inject = ['$stateParams', '$state', 'traceSettingService', 'notificationService'];

    function bonusPtPublicsController($stateParams, $state, traceSettingService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.traceSettingId = 0;
        vm.bonusPtPublicId = 0;
        vm.bonusPtPublics = [];
        vm.title = 'Bonus Points for Publications';
        vm.addBonusPtPublic = addBonusPtPublic;
        vm.updateBonusPtPublic = updateBonusPtPublic;
        
        vm.close = close;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
        }

        init();
        function init() {
            traceSettingService.getBonusPtPublics(vm.traceSettingId).then(function (data) {
                vm.bonusPtPublics = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function addBonusPtPublic() {
            $state.go('trace-setting-tabs.bonus-point-publication-create', { traceSettingId: vm.traceSettingId,bonusPtPublicId: vm.bonusPtPublicId });
        }
        
        function updateBonusPtPublic(bonusPtPublic) {
            $state.go('trace-setting-tabs.bonus-point-publication-modify', { traceSettingId: vm.traceSettingId, bonusPtPublicId: bonusPtPublic.bonusPtPublicId });
        }
      
        function close() {
            $state.go('trace-setting-tabs.bonus-point-publications')
        }
    }

})();
