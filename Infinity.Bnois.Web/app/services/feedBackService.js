(function () {
    'use strict';
    angular.module('app').service('feedBackService', ['dataConstants', 'apiHttpService', feedBackService]);

    function feedBackService(dataConstants, apiHttpService) {
        var service = {
            getFeedBacks: getFeedBacks,
            getFeedBackById: getFeedBackById,
            saveFeedBack: saveFeedBack,
            saveAdminFeedBack: saveAdminFeedBack,
            getFeedbackForUser: getFeedbackForUser,
            getFeedbackForUserNotification: getFeedbackForUserNotification,

            deleteFeedback: deleteFeedback
        };

        return service;
        function getFeedBacks(pageSize, pageNumber, searchText) {
            var url = dataConstants.FEED_BACK_URL + 'get-feed-backs?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        //function getBatch(id) {
        //    var url = dataConstants.BATCH_URL + 'get-batch?id=' + id;
        //    return apiHttpService.GET(url);
        //}

        function saveFeedBack(data) {
            var url = dataConstants.FEED_BACK_URL + 'save-feed-back';
            return apiHttpService.POST(url, data);
        }
        function saveAdminFeedBack(data) {
            var url = dataConstants.FEED_BACK_URL + 'save-admin-feed-back';
            return apiHttpService.POST(url, data);
        }

        function getFeedBackById(Id) {
            var url = dataConstants.FEED_BACK_URL + 'get-feed-back-by-feedback-id?id=' + Id;
            return apiHttpService.GET(url);
        }
        function getFeedbackForUser() {
            var url = dataConstants.FEED_BACK_URL + 'get-feed-back-for-user';
            return apiHttpService.GET(url);
        }
        function getFeedbackForUserNotification() {
            var url = dataConstants.FEED_BACK_URL + 'get-feed-back-for-user-notification';
            return apiHttpService.GET(url);
        }

        function deleteFeedback(id) {
            var url = dataConstants.FEED_BACK_URL + 'delete-feed-back?id=' + id;
            return apiHttpService.DELETE(url);
        }

    }
})();