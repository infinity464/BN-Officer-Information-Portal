(function () {
    'use strict';
    angular.module('app').service('courseFuturePlanService', ['dataConstants', 'apiHttpService', courseFuturePlanService]);

    function courseFuturePlanService(dataConstants, apiHttpService) {
        var service = {
            getCourseFuturePlans: getCourseFuturePlans,
            getCourseFuturePlan: getCourseFuturePlan,
            saveCourseFuturePlan: saveCourseFuturePlan,
            updateCourseFuturePlan: updateCourseFuturePlan,
            deleteCourseFuturePlan: deleteCourseFuturePlan
        };

        return service;
        function getCourseFuturePlans(pNo) {
            var url = dataConstants.EMPLOYEE_COURSE_FUTURE_PLAN_URL + 'get-course-future-plans?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getCourseFuturePlan(courseFuturePlanId) {
            var url = dataConstants.EMPLOYEE_COURSE_FUTURE_PLAN_URL + 'get-course-future-plan?id=' + courseFuturePlanId;
            return apiHttpService.GET(url);
        }

        function saveCourseFuturePlan(data) {
            var url = dataConstants.EMPLOYEE_COURSE_FUTURE_PLAN_URL + 'save-course-future-plan';
            return apiHttpService.POST(url, data);
        }

        function updateCourseFuturePlan(courseFuturePlanId, data) {
            var url = dataConstants.EMPLOYEE_COURSE_FUTURE_PLAN_URL + 'update-course-future-plan/' + courseFuturePlanId;
            return apiHttpService.PUT(url, data);
        }

        function deleteCourseFuturePlan(courseFuturePlanId) {
            var url = dataConstants.EMPLOYEE_COURSE_FUTURE_PLAN_URL + 'delete-course-future-plan/' + courseFuturePlanId;
            return apiHttpService.DELETE(url);
        }
        
    }
})();