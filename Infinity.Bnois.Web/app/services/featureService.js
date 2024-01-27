(function () {
    'use strict';
    angular.module('app').service('featureService', ['identityDataConstants', 'apiHttpService', featureService]);
    function featureService(identityDataConstants, apiHttpService) {
        var service = {
            getFeatures: getFeatures,
            getFeature: getFeature,
            saveFeature: saveFeature,
            updateFeature: updateFeature,
            deleteFeature: deleteFeature,
            downloadFeaturesReport: downloadFeaturesReport
        };

        return service;
        function getFeatures(pageSize, pageNumber, searchString) {
          var url = identityDataConstants.FEATURE_URL + 'get-features?pageSize=' + pageSize + "&pageNumber=" + pageNumber + "&searchString=" + searchString;
            return apiHttpService.GET(url);
        }

        function getFeature(featureId) {
          var url = identityDataConstants.FEATURE_URL + 'get-feature?featureId=' + featureId;
            return apiHttpService.GET(url);
        }

        function saveFeature(data) {
          var url = identityDataConstants.FEATURE_URL + 'save-feature';
            return apiHttpService.POST(url, data);
        }

        function updateFeature(featureId, data) {
            var url = identityDataConstants.FEATURE_URL + 'update-feature/' + featureId;
            return apiHttpService.PUT(url, data);
        }

        function deleteFeature(featureId) {
          var url = identityDataConstants.FEATURE_URL + 'delete-feature/' + featureId;
            return apiHttpService.DELETE(url);
        }

        function downloadFeaturesReport() {
            return identityDataConstants.FEATURE_URL + 'download-feature' ;
        }
    }
})();