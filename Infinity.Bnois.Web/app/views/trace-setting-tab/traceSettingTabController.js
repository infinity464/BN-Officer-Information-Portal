
'use strict';
var controllerId = 'traceSettingTabController';
angular.module('app').controller(controllerId, traceSettingTabController);
traceSettingTabController.$inject = ['$stateParams', '$state', 'notificationService'];

function traceSettingTabController($stateParams, $state, notificationService) {
    /* jshint validthis:true */
    var vm = this;
    vm.traceSettingId = 0;
    if ($stateParams.id !== undefined && $stateParams.id !== null) {
        vm.traceSettingId = $stateParams.id;
    }
    init();
    function init() {
        $state.go('trace-setting-tabs.trace-setting-modify', { traceSettingId: vm.traceSettingId });
    }

}