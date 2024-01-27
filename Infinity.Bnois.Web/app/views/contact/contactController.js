

(function () {

    'use strict';

    var controllerId = 'contactController';

    angular.module('app').controller(controllerId, contactController);
    contactController.$inject = ['$stateParams', 'codeValue', 'notificationService', '$state'];

    function contactController($stateParams, codeValue, notificationService, $state) {
        var vm = this;
        vm.showConatcToggle = showConatcToggle;

        Init();
        function Init() {
        };
        function showConatcToggle() {
            var x = document.getElementById("floatBerBox");
            x.style.display = "block";
        }




    }
})();
