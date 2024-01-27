(function () {
    "use strict";

    //API
  window.baseAPIUrl = 'http://localhost:24116/';
  window.identityServerUrl = "http://localhost:24116/core";
  window.identityServerAPI = "http://localhost:24116/";

    var commonServie = angular.module("common.services", []);

    commonServie.constant("appSettings",
        {
            scerpAPI: window.baseAPIUrl,
            IdentityServer: window.identityServerUrl,
            identityServerAPI: window.identityServerAPI
        });

    // oidc manager for dep injection
    commonServie.factory("OidcManager", function () {

        //// configure manager, including session management support
        var config = {
            authority: window.identityServerUrl,
            client_id: "infinity-bnois-api-client",
            redirect_uri: window.location.protocol + "//" + window.location.host + "/callback.html",
            post_logout_redirect_uri: window.location.protocol + "//" + window.location.host + "/index.html",
            // these two will be done dynamically from the buttons clicked, but are
            // needed if you want to use the silent_renew
            response_type: "id_token token",
            scope: "openid profile infinity-bnois-api-scope infinity-bnois-identity-api-scope roles all_claims",

            // silent renew will get a new access_token via an iframe 
            // just prior to the old access_token expiring (60 seconds prior)
            silent_redirect_uri: window.location.protocol + "//" + window.location.host + "/silent-renew.html",
            automaticSilentRenew: true,
            silent_renew: true,

        };

        var manager = new OidcTokenManager(config);

        return {
            OidcTokenManager: function () {
                return manager;
            }
        };
    });

}());

