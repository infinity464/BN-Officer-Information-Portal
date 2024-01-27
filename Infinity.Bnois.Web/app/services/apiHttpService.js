(function () {
    'use strict';
    angular.module('app').service('apiHttpService', ['$http', '$q', apiHttpService]);

    function apiHttpService($http, $q) {

        var service = {
            GET: getRequest,
            PUT: putRequest,
            POST: postRequest,
            DELETE: deleteRequest
        };

        return service;

        function getRequest(url) {
            var deferred = $q.defer();

            $http.get(url).success(function (data) {
                deferred.resolve(data);
            }).error(function (error) {
                deferred.reject(error);
            });

            return deferred.promise;
        }


        function putRequest(url, data) {
            var deferred = $q.defer();

            $http.put(url, data).success(function (data) {
                deferred.resolve(data);
            }).error(function (error) {
                deferred.reject(error);
            });

            return deferred.promise;
        }

        function postRequest(url, data) {
            var deferred = $q.defer();

            $http.post(url, data).success(function (data) {
                deferred.resolve(data);
            }).error(function (error) {
                deferred.reject(error);
            });

            return deferred.promise;
        }

        function deleteRequest(url) {
            var deferred = $q.defer();

            $http.delete(url).success(function (data) {
                deferred.resolve(data);
            }).error(function (error) {
                deferred.reject(error);
            });

            return deferred.promise;
        }
    }
})();