
(function () {

    'use strict';
    var controllerId = 'bonusPtComAppsController';
    angular.module('app').controller(controllerId, bonusPtComAppsController);
    bonusPtComAppsController.$inject = ['$stateParams', '$state', 'traceSettingService', 'notificationService'];

    function bonusPtComAppsController($stateParams, $state, traceSettingService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.traceSettingId = 0;
        vm.bonusPtComAppId = 0;
        vm.bonusPtComApps = [];
        vm.title = 'Bonus Points for Commendation/Appreciations';
        vm.addBonusPtComApp = addBonusPtComApp;
        vm.updateBonusPtComApp = updateBonusPtComApp;
        
        vm.close = close;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
        }

        init();
        function init() {
            traceSettingService.getBonusPtComApps(vm.traceSettingId).then(function (data) {
                vm.bonusPtComApps = data.result;
                console.log(vm.bonusPtComApps);
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function addBonusPtComApp() {
            $state.go('trace-setting-tabs.bonus-point-com-app-create', { traceSettingId: vm.traceSettingId,bonusPtComAppId: vm.bonusPtComAppId });
        }
        
        function updateBonusPtComApp(bonusPtComApp) {
            $state.go('trace-setting-tabs.bonus-point-com-app-modify', { traceSettingId: vm.traceSettingId, bonusPtComAppId: bonusPtComApp.bonusPtComAppId });
        }
      
        function close() {
            $state.go('trace-setting-tabs.bonus-point-com-apps')
        }
    }

})();
