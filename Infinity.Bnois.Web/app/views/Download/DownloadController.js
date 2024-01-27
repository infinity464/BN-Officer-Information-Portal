(function () {

    'use strict';

    var controllerId = 'DownloadController';

    angular.module('app').controller(controllerId, DownloadController);
    DownloadController.$inject = ['$stateParams', 'codeValue','downloadfileService', 'notificationService', '$state'];

    function DownloadController($stateParams, codeValue, downloadfileService, notificationService, $state) {
        var vm = this;
        vm.downloads = {};
        Init();
        function Init() {
            downloadfileService.getDownloadsForView().then(function (data) {
                vm.downloads = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };
        




    }
})();