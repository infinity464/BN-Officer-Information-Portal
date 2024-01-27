(function () {

    'use strict';

    var controllerId = 'flashNoticeAddController';

    angular.module('app').controller(controllerId, flashNoticeAddController);
    flashNoticeAddController.$inject = ['$stateParams', '$scope', 'flashNoticeService','notificationService', '$state', '$location'];

    function flashNoticeAddController($stateParams, $scope, flashNoticeService, notificationService, $state, location) {
        var vm = this;
        vm.saveButtonText = 'Save';
        vm.title = 'Save MODE';
        vm.flashNoticeId = 0;
        vm.flashNotice = {};
        vm.close = close;
        vm.flashNoticeIdForm = {};
        vm.save = save;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.flashNoticeId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }


        Init();
        function Init() {
            flashNoticeService.GetFlashNoticeById(vm.flashNoticeId).then(function (data) {
                vm.flashNotice = data.result;
                if (vm.flashNoticeId > 0) {
                    vm.flashNotice.fromDate = new Date(vm.flashNotice.fromDate);
                    vm.flashNotice.toDate = new Date(vm.flashNotice.toDate);
                }
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };


        function close() {
            $state.go('flash-notice');
        };



        function save() {
            if (vm.flashNoticeId === 0 || vm.flashNoticeId === '') {
                insert();
            } else {
                update();
            }
        };

        function insert() {
            flashNoticeService.SaveFlashNotice(vm.flashNotice).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };
        function update() {
            flashNoticeService.UpdateFlashNotice(vm.flashNoticeId, vm.flashNotice).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };
    }
})();