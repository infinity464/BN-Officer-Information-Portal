(function () {

    'use strict';

    var controllerId = 'flashNoticeController';

    angular.module('app').controller(controllerId, flashNoticeController);
    flashNoticeController.$inject = ['flashNoticeService', 'notificationService', '$state'];

    function flashNoticeController(flashNoticeService, notificationService, $state) {
        var vm = this;
        vm.activeflashNotices = {};
        vm.inActiveflashNotices = {};
        vm.createFlashNotice = createFlashNotice;
        vm.updateFlashNotice = updateFlashNotice;
        vm.deleteFlashNotice = deleteFlashNotice;


        init();
        function init() {
            flashNoticeService.getFlashNotices().then(function (data) {
                vm.activeflashNotices = data.result.activeFlashNotice;
                vm.inActiveflashNotices = data.result.inActiveFlashNotice;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        
        function createFlashNotice() {
            $state.go('flash-notice-create');
        }

        function updateFlashNotice(id) {
            $state.go('flash-notice-modify', { id: id });
        }

        function deleteFlashNotice(id) {
            flashNoticeService.DeleteFlashNotice(id).then(function (data) {
                init();

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

    }
})();