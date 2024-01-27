

(function () {

    'use strict';

    var controllerId = 'flashNoticeForMemberController';

    angular.module('app').controller(controllerId, flashNoticeForMemberController);
    flashNoticeForMemberController.$inject = ['$stateParams','flashNoticeService', 'notificationService', '$state'];

    function flashNoticeForMemberController($stateParams, flashNoticeService, notificationService, $state) {
        var vm = this;
        vm.notices = {};

        Init();
        function Init() {
            flashNoticeService.getFlashNoticeForMemberUser().then(function (data) {
                vm.notices = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
    }
})();
