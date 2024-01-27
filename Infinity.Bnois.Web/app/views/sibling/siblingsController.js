(function () {

    'use strict';
    var controllerId = 'siblingsController';
    angular.module('app').controller(controllerId, siblingsController);
    siblingsController.$inject = ['$stateParams', '$state', 'siblingService', 'notificationService'];

    function siblingsController($stateParams, $state, siblingService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.siblingId = 0;
        vm.siblings = [];
        vm.title = 'Sibling';
        vm.addSibling = addSibling;
        vm.updateSibling = updateSibling;
        vm.deleteSibling = deleteSibling;
        vm.uploadSiblingImage = uploadSiblingImage;
        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        init();
        function init() {
            siblingService.getSiblings(vm.employeeId).then(function (data) {
                vm.siblings = data.result;
                    vm.permission = data.permission;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function addSibling() {
            $state.go('employee-tabs.employee-sibling-create', { id: vm.employeeId, siblingId: vm.siblingId });
        }

  

        function updateSibling(sibling) {
            $state.go('employee-tabs.employee-sibling-modify', { id: vm.employeeId, siblingId: sibling.siblingId });
        }

        function uploadSiblingImage(sibling) {
            $state.go('employee-tabs.employee-sibling-image', {siblingId: sibling.siblingId });
        }

        function deleteSibling(sibling) {
            siblingService.deleteSibling(sibling.siblingId).then(function (data) {
                siblingService.getSiblings(vm.employeeId).then(function (data) {
                    vm.siblings = data.result;
                });
                $state.go('employee-tabs.employee-siblings');
            });
        }
    }

})();
