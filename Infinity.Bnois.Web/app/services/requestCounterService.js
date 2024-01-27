(function () {
    'use strict';

    angular.module('app').factory('requestCounter', requestCounter);

    angular.module('app').config(function ($httpProvider) {
        $httpProvider.interceptors.push('requestCounter');
    });

    requestCounter.$inject = ['$q'];

    function requestCounter($q) {
        var requests = 0;

        function request(config) {
            requests += 1;
            return $q.when(config);
        }

        function requestError(error) {
            requests -= 1;
            return $q.reject(error);
        }

        function response(response) {
            requests -= 1;
            return $q.when(response);
        }

        function responseError(error) {
            requests -= 1;
            return $q.reject(error);
        }

        function getRequestCount() {
            return requests;
        }

        return {
            request: request,
            response: response,
            requestError: requestError,
            responseError: responseError,
            getRequestCount: getRequestCount
        };
    }
})();