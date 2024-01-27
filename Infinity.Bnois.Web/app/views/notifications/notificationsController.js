

(function () {

    'use strict';

    var controllerId = 'notificationsController';

    angular.module('app').controller(controllerId, notificationsController);
    notificationsController.$inject = ['$stateParams','$window', 'codeValue','feedBackService', 'notificationService', '$state'];

    function notificationsController($stateParams, $window, codeValue, feedBackService, notificationService, $state) {
        var vm = this;
        //vm.ReadMore = ReadMore;
        vm.notification = '';

        Init();
        function Init() {
            feedBackService.getFeedbackForUser().then(function (data) {
                vm.notification = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

    }
})();
