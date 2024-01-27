

(function () {

  'use strict';

  var controllerId = 'featureAddController';

  angular.module('app').controller(controllerId, featureAddController);
  featureAddController.$inject = ['$stateParams', 'featureService', 'notificationService', '$state'];

  function featureAddController($stateParams, featureService, notificationService, $state) {
    var vm = this;
    vm.featureId = 0;
    vm.title = 'ADD MODE';
    vm.feature = {};
    vm.modules = [];
    vm.featureTypes = [];
    vm.saveButtonText = 'Save';
    vm.save = save;
    vm.close = close;
    vm.featureForm = {};
   

    if ($stateParams.featureId !== undefined && $stateParams.featureId !== null) {
        vm.featureId = $stateParams.featureId;
      vm.saveButtonText = 'Update';
        vm.title = 'UPDATE MODE';
    }

    Init();
    function Init() {
      featureService.getFeature(vm.featureId).then(function (data) {
        vm.feature = data.result.feature;
        vm.modules = data.result.modules;
        vm.featureTypes = data.result.featureTypes;
      },
        function (errorMessage) {
          notificationService.displayError(errorMessage.message);
        });
    };

    function save() {
      if (vm.featureId !== 0) {
        updateFeature();
      } else {
        insertFeature();
      }
    }

    function insertFeature() {
      featureService.saveFeature(vm.feature).then(function (data) {
        close();
      },
        function (errorMessage) {
          notificationService.displayError(errorMessage.message);
        });
    }
    function updateFeature() {
      featureService.updateFeature(vm.featureId, vm.feature).then(function (data) {
        close();
      },
        function (errorMessage) {
          notificationService.displayError(errorMessage.message);
        });
    }

    function close() {
      $state.go('features');
    }

  }
})();
