

(function () {

    'use strict';

    var controllerId = 'moduleAddController';

    angular.module('app').controller(controllerId, moduleAddController);
    moduleAddController.$inject = ['$stateParams', 'moduleService', 'notificationService', '$state'];

    function moduleAddController($stateParams, moduleService, notificationService, $state) {
        var vm = this;
        vm.moduleId = 0;
        vm.title = 'ADD MODE';
        vm.module = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.moduleForm = {};

        if ($stateParams.moduleId !== undefined && $stateParams.moduleId !== null) {
            vm.moduleId = $stateParams.moduleId;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            moduleService.getModule(vm.moduleId).then(function (data) {
              vm.module = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.moduleId !== 0) {
                updateModule();
            } else {
                insertModule();
            }
        }

        function insertModule() {
            moduleService.saveModule(vm.module).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateModule() {
    
          moduleService.updateModule(vm.moduleId, vm.module ).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('modules');
        }

    }
})();
