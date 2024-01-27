

(function () {

    'use strict';

    var controllerId = 'usefulLinkController';

    angular.module('app').controller(controllerId, usefulLinkController);
    usefulLinkController.$inject = ['$stateParams', 'codeValue', 'notificationService', '$state'];

    function usefulLinkController($stateParams, codeValue, notificationService, $state) {
        var vm = this;


        Init();
        function Init() {
            
        };





    }
})();
