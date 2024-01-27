(function () {

    'use strict';
    var controllerId = 'officeStructuresController';
    angular.module('app').controller(controllerId, officeStructuresController);
    officeStructuresController.$inject = ['$stateParams', '$state','$scope','$rootScope','codeValue','officeService','notificationService'];

    function officeStructuresController($stateParams, $state, $scope, $rootScope,codeValue, officeService, notificationService) {
        /* jshint validthis:true */
        var vm = this;

        vm.offices = {};


        init();

        function init() {
            officeService.getOfficeStructures().then(function(data) {

                vm.offices = data.result[0];

                    var nodeTemplate = function (data) {
                        return `
        
        <a target="_blank" href="http://localhost:6970/office-appointment-structures/${data.officeId}">
        <div class="title">${data.shortName}</div>
        <div class="content">${data.count}</div>
  </a>
      `;
                    };



                    $('#chart-container').orgchart({
                        'data': vm.offices,
                        'nodeTitle': 'shortName',
                        'nodeContent': 'count',
                        'nodeId': 'officeId',
                        'visibleLevel': 2,
                        'zoom': true,
                        'nodeTemplate': nodeTemplate,





                    });
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

          
          
        }


    }
})();
