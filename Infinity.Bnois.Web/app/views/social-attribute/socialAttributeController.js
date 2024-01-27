
(function () {

    'use strict';
    var controllerId = 'socialAttributeController';
    angular.module('app').controller(controllerId, socialAttributeController);
    socialAttributeController.$inject = ['$stateParams', '$state', 'socialAttributeService', 'notificationService'];

    function socialAttributeController($stateParams, $state, socialAttributeService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = "Social Attribute";
        vm.socialAttribute = {};
        vm.employeeId = 0;
        vm.socialAttributeForm = {};
        vm.save = save;
        vm.updateSocialAttribute = updateSocialAttribute;
        vm.close = close;
        vm.saveButtonText = 'Save';
        vm.title = 'ADD MODE';

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
            vm.title = 'UPDATE MODE';
            vm.saveButtonText = 'Update';
        }
        init();
        function init() {
            socialAttributeService.getSocialAttribute(vm.employeeId).then(function (data) {
                vm.socialAttribute = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function save() {
            update();
        }

        function updateSocialAttribute() {
            $state.go('employee-tabs.employee-social-attribute-modify');
        }

        function update() {
            socialAttributeService.updateSocialAttribute(vm.employeeId, vm.socialAttribute).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function close() {
            $state.go('employee-tabs.employee-social-attribute');
        }
    }

})();
