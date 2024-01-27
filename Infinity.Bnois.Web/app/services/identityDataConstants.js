
(function () {
    "use strict";
        angular.module("app").constant("identityDataConstants", {
        MODULE_URL: identityServerAPI + "api/Configuration/modules/",
        FEATURE_URL: identityServerAPI + "api/Configuration/features/",
        ROLE_URL: identityServerAPI + "api/Configuration/roles/",
        USER_URL: identityServerAPI + "api/Configuration/users/",
        ACCOUNT_URL: identityServerAPI + "api/Configuration/accounts/",
    });
})();