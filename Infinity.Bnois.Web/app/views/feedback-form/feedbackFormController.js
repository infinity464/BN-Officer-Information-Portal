

(function () {

    'use strict';

    var controllerId = 'feedbackFormController';

    angular.module('app').controller(controllerId, feedbackFormController);
    feedbackFormController.$inject = ['$stateParams', 'codeValue','feedBackService', 'notificationService', '$state'];

    function feedbackFormController($stateParams, codeValue, feedBackService,  notificationService, $state) {
        var vm = this;
        vm.childrens = [];
        vm.insertFeedBack = insertFeedBack;
        vm.feedBack = {};
        vm.feedBackForm = {};

        if ($stateParams.pno !== undefined && $stateParams.pno !== null) {
            vm.pNo = $stateParams.pno;
        }

        Init();
        function Init() {
            
        };
        function insertFeedBack() {
            feedBackService.saveFeedBack(vm.feedBack).then(function (data) {
                vm.feedBack = {};
                notificationService.displaySuccess("Your feedback submitted successfully");
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }




    }
})();
