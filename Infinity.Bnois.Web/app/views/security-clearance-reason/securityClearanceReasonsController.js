/// <reference path="../../services/securityClearanceReasonService.js" />

(function () {

    'use strict';
    var controllerId = 'securityClearanceReasonsController';
    angular.module('app').controller(controllerId, securityClearanceReasonsController);
    securityClearanceReasonsController.$inject = ['$state', 'securityClearanceReasonService', 'notificationService', '$location'];

    function securityClearanceReasonsController($state, securityClearanceReasonService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.securityClearanceReasons = [];
        vm.addSecurityClearanceReason = addSecurityClearanceReason;
        vm.updateSecurityClearanceReason = updateSecurityClearanceReason;
        vm.deleteSecurityClearanceReason = deleteSecurityClearanceReason;
        vm.back = back;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;

        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
            vm.searchText = location.search().q;
        }
        init();
        function init() {
            securityClearanceReasonService.getSecurityClearanceReasons(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.securityClearanceReasons = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addSecurityClearanceReason() {
            $state.go('security-clearance-reason-create');
        }

        function updateSecurityClearanceReason(securityClearanceReason) {
            $state.go('security-clearance-reason-modify', { id: securityClearanceReason.securityClearanceReasonId });
        }

        function deleteSecurityClearanceReason(securityClearanceReason) {
            securityClearanceReasonService.deleteSecurityClearanceReason(securityClearanceReason.securityClearanceReasonId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('security-clearance-reasons', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }

        function back() {
            $state.go('employee-security-clearances');
        }
    }

})();
