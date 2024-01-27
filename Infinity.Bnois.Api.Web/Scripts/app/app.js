/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license
 */

window.identityServer = (function () {
    "use strict";
    //$(".navbar").hide();
    var identityServer = {
        getModel: function () {
            var modelJson = document.getElementById("modelJson");
            var encodedJson = '';
            if (typeof (modelJson.textContent) !== undefined) {
                encodedJson = modelJson.textContent;
            } else {
                encodedJson = modelJson.innerHTML;
            }
            var json = Encoder.htmlDecode(encodedJson);
            var model = JSON.parse(json);
            return model;
        }
    };

    return identityServer;
})();

(function () {
    "use strict";

    (function () {
        var app = angular.module("app", ['ngCookies']);

        app.controller("LayoutCtrl", function ($scope, $cookies, Model) {
            $scope.model = Model;
            $scope.date = new Date();
            if (($cookies.get('signUpFormContent') === undefined || $cookies.get('signUpFormContent') === false) && ($cookies.get('loginFormContent') === undefined || $cookies.get('loginFormContent') === false)) {
                $scope.loginMainContent = true;
            } else {
                $scope.loginMainContent = false;
                if ($cookies.get('loginFormContent') !== undefined) {
                    $scope.loginFormContent = JSON.parse($cookies.get('loginFormContent'));
                }
                if ($cookies.get('signUpFormContent') !== undefined) {
                    $scope.signUpFormContent = JSON.parse($cookies.get('signUpFormContent'));
                }
                //$scope.signUpFormContent = $cookies.get('signUpFormContent');
            }
            if ($cookies.get('loginMainContent') !== undefined) {
                $scope.loginMainContent = JSON.parse($cookies.get('loginMainContent'));
            }
            
            $scope.signUpPageRedirect = function () {
                $scope.loginMainContent = false;
                $scope.loginFormContent = false;
                $scope.signUpFormContent = true;
                $cookies.put("loginMainContent", $scope.loginMainContent);
                $cookies.put("signUpFormContent", $scope.signUpFormContent);
                $cookies.put("loginFormContent", $scope.loginFormContent);
            };

            $scope.logInPageRedirect = function () {
                $scope.loginMainContent = false;
                $scope.signUpFormContent = false;
                $scope.loginFormContent = true;
                $cookies.put("loginMainContent", $scope.loginMainContent);
                $cookies.put("signUpFormContent", $scope.signUpFormContent);
                $cookies.put("loginFormContent", $scope.loginFormContent);

            };
            $scope.back = function () {
                $scope.loginMainContent = true;
                $scope.signUpFormContent = false;
                $scope.loginFormContent = false;
                $cookies.put("loginMainContent", $scope.loginMainContent);
                $cookies.put("signUpFormContent", $scope.signUpFormContent);
                $cookies.put("loginFormContent", $scope.loginFormContent);
            };
        });

        app.directive("antiForgeryToken", function () {
            return {
                restrict: 'E',
                replace: true,
                scope: {
                    token: "="
                },
                template: "<input type='hidden' name='{{token.name}}' value='{{token.value}}'>"
            };
        });

        app.directive("focusIf", function ($timeout) {
            return {
                restrict: 'A',
                scope: {
                    focusIf:'='
                },
                link: function (scope, elem, attrs) {
                    if (scope.focusIf) {
                        $timeout(function () {
                            elem.focus();
                        }, 100);
                    }
                }
            };
        });
    })();

    (function () {
        var model = identityServer.getModel();
        angular.module("app").constant("Model", model);
        if (model.autoRedirect && model.redirectUrl) {
            if (model.autoRedirectDelay < 0) {
                model.autoRedirectDelay = 0;
            }
            window.setTimeout(function () {
                window.location = model.redirectUrl;
            }, model.autoRedirectDelay * 200);
        }
    })();

})();
