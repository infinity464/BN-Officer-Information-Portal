(function () {
    'use strict';
    angular.module('app').service('trainingPlanService', ['dataConstants', 'apiHttpService', trainingPlanService]);

    function trainingPlanService(dataConstants, apiHttpService) {
        var service = {
            getTrainingPlans: getTrainingPlans,
            getTrainingPlan: getTrainingPlan,
            getCourses: getCourses,
            getTrainingPlanSelectModel: getTrainingPlanSelectModel,
            saveTrainingPlan: saveTrainingPlan,
            updateTrainingPlan: updateTrainingPlan,
            deleteTrainingPlan: deleteTrainingPlan
        };

        return service;
        function getTrainingPlans(pageSize, pageNumber,searchText) {
            var url = dataConstants.TRAINING_PLAN_URL + 'get-training-plans?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getTrainingPlan(trainingPlanId) {
            var url = dataConstants.TRAINING_PLAN_URL + 'get-training-plan?id=' + trainingPlanId;
            return apiHttpService.GET(url);
        }


        function getCourses(catId,subCatId,countryId) {
            var url = dataConstants.TRAINING_PLAN_URL + 'get-courses-by-category-sub-category?catId=' + catId + "&subCatId=" + subCatId + "&countryId=" + countryId;
            return apiHttpService.GET(url);
        }
        function getTrainingPlanSelectModel(trainingPlanId) {
            var url = dataConstants.TRAINING_PLAN_URL + 'get-training-plan-select-model?trainingPlanId=' + trainingPlanId;
            return apiHttpService.GET(url);
        }

        function saveTrainingPlan(data) {
            var url = dataConstants.TRAINING_PLAN_URL + 'save-training-plan';
            return apiHttpService.POST(url, data);
        }

        function updateTrainingPlan(trainingPlanId, data) {
            var url = dataConstants.TRAINING_PLAN_URL + 'update-training-plan/' + trainingPlanId;
            return apiHttpService.PUT(url, data);
        }

        function deleteTrainingPlan(trainingPlanId) {
            var url = dataConstants.TRAINING_PLAN_URL + 'delete-training-plan/' + trainingPlanId;
            return apiHttpService.DELETE(url);
        }
        
    }
})();