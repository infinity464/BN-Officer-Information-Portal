

(function () {

    'use strict';

    var controllerId = 'feedBackController';

    angular.module('app').controller(controllerId, feedBackController);
    feedBackController.$inject = ['$stateParams', 'codeValue', 'feedBackService', 'notificationService', '$state', '$location'];

    function feedBackController($stateParams, codeValue, feedBackService, notificationService, $state, location) {
        var vm = this;
        vm.childrens = [];
        vm.feedback = {};
        vm.pageChanged = pageChanged;
        vm.getFeedBackById = getFeedBackById;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;
        vm.feedbackHistory = {};
        vm.feedBackForm = {};
        vm.adminFeedBack = {};
        vm.insertAdminFeedBack = insertAdminFeedBack;
        vm.deleteAdminFeedBack = deleteAdminFeedBack;

        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
            vm.searchText = location.search().q;
        }
        init();
        function init() {
            feedBackService.getFeedBacks(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.feedback = data.result;
                vm.total = data.total;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function getFeedBackById(id) {
            feedBackService.getFeedBackById(id).then(function (data) {
                vm.feedbackHistory = data.result;
                vm.adminFeedBack.feedBackId = vm.feedbackHistory[0].feedBackId;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
            feedBackService.getFeedBacks(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.feedback = data.result;
                vm.total = data.total;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function insertAdminFeedBack() {
            feedBackService.saveAdminFeedBack(vm.adminFeedBack).then(function (data) {
                notificationService.displaySuccess("Successfully updated");
                getFeedBackById(vm.adminFeedBack.feedBackId);
                vm.adminFeedBack = {};
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function deleteAdminFeedBack(id) {
            feedBackService.deleteFeedback(id).then(function (data) {
                notificationService.displaySuccess("Successfully Deleted");
                vm.pageNumber = 1;
                pageChanged();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function pageChanged() {
            $state.go('feed-back', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }
})();
