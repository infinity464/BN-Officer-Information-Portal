(function () {
    'use strict';

    var app = angular.module('app', [

        'ngAnimate',
        'ngRoute',
        'ngSanitize',
        'ngResource',
        'ngCookies',
        'ui.router',
        'ui.bootstrap',
        'ngComboDatePicker',
        'angularFileUpload',
        'ngImgCrop',
        'common',
        'chart.js',
        // 3rd Party Modules
        'common.services',
        'checklist-model',
        'kendo.directives',
        'angularjs-dropdown-multiselect',
        'angucomplete-alt'
       
    ]);

    app.config(configure);

    configure.$inject = ['$httpProvider', '$compileProvider','ChartJsProvider'];

    function configure($httpProvider, $compileProvider, ChartJsProvider) {
        $httpProvider.defaults.useXDomain = true;
        $httpProvider.defaults.withCredentials = true;
        delete $httpProvider.defaults.headers.common["X-Requested-With"];
      //  ChartJsProvider.setOptions({ colors: ['#803690', '#00ADF9', '#DCDCDC', '#46BFBD', '#FDB45C', '#949FB1', '#4D5360'] });
        $httpProvider.interceptors.push(function (appSettings, OidcManager) {
            return {
                'request': function (config) {

                    // if the access token has expired, we need to redirect to
                    // the login page.

                    if (OidcManager.OidcTokenManager().expired) {
                        OidcManager.OidcTokenManager().redirectForToken();
                    }

                    // if it's a request to the API, we need to provide the
                    // access token as bearer token.  
                    var mgr = OidcManager.OidcTokenManager();
                    //console.log($httpProvider.defaults.withCredentials);
                    // config.url = config.url.replace("{company}", mgr.profile.company_id);
                    if (config.url.indexOf(appSettings.scerpAPI) === 0 || config.url.indexOf(appSettings.IdentityServer) === 0 || config.url.indexOf(appSettings.identityServerAPI) === 0) {
                        config.headers.Authorization = 'Bearer ' + OidcManager.OidcTokenManager().access_token;

                    }

                    return config;
                },
                //'responseError': function (rejection) {
                //    // do something on error

                //    if (rejection.status == 401) {
                //        //$rootScope.signOut();
                //        //alert('aaa');
                //        //OidcManager.OidcTokenManager().removeToken();
                //       // window.location = "/";
                //    }

                //    return $q.reject(rejection);
                //},

            };
        });

    }

    app.run(['$route', '$rootScope', '$q', function ($route, $rootScope, $q) {
        $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
            if (current.$$route !== undefined) {
                $rootScope.title = current.$$route.title + ' | Bangladesh Navy';
            } else {
                $rootScope.title = "Bangladesh Navy Officers' Information System";
            }
        });
    }]);
})()