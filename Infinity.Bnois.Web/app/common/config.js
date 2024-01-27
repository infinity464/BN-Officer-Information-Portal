(function () {
    'use strict';
    var app = angular.module('app');
    var keyCodes = {
        backspace: 8,
        tab: 9,
        enter: 13,
        esc: 27,
        space: 32,
        pageup: 33,
        pagedown: 34,
        end: 35,
        home: 36,
        left: 37,
        up: 38,
        right: 39,
        down: 40,
        insert: 45,
        del: 46
    };

    var events = {
        controllerActivateSuccess: 'controller.activateSuccess',
        spinnerToggle: 'spinner.toggle'
    };

    var config = {
        events: events,
        keyCodes: keyCodes,
        version: '0.0.0.1'
    };

    app.value('config', config);
    app.config(['commonConfigProvider', function (cfg) {
        cfg.config.controllerActivateSuccessEvent = config.events.controllerActivateSuccess;
        //cfg.config.spinnerToggleEvent = config.events.spinnerToggle;
    }
    ]);
})();


angular.module("app").config(['$provide', function ($provide) {
    $provide.decorator('$state', ['$delegate', '$window',
        function ($delegate, $window) {
            var extended = {
                goNewTab: function (stateName, params) {
                    $window.open(
                        $delegate.href(stateName, params, { absolute: true }), '_blank');
                }
            };
            angular.extend($delegate, extended);
            return $delegate;
        }]);
}]);