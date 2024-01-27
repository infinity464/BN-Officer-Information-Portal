(function () {

    'use strict';
    var controllerId = 'officeTabController';
    angular.module('app').controller(controllerId, officeTabController);
    officeTabController.$inject = ['$stateParams', '$scope', '$state','$rootScope','officeService','notificationService'];

    function officeTabController($stateParams, $scope, $state,$rootScope,officeService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        //vm.officeId = 0;
        $rootScope.officeId = 0;
        $rootScope.offices = [];
        vm.officeDetails = officeDetails;
        vm.deleteOffice = deleteOffice;
        vm.addOffice = addOffice;
        vm.search = search;
       
        $rootScope.office = {};

        init();
        function init() {
            officeService.getOffices().then(function (data) {
                $rootScope.offices = data.result;
                   vm.permission = data.permission;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            $state.go('office-tabs.office-general', { id: $rootScope.officeId });
        }



        function officeDetails(officeId) {

            $rootScope.officeId = officeId;
            $state.go('office-tabs.office-general', { id: $rootScope.officeId });

        }


        function search(text) {
            var filterText = text.toLowerCase();

            if (filterText !== "") {
                $(".k-treeview .k-group .k-group .k-in").closest("li").hide();
                $(".k-treeview .k-group .k-group .k-in:contains(" + filterText + ")").each(function () {
                    $(this).parents("ul, li").each(function () {
                        $(this).show();
                    });
                });
            }
            else {
                $(".k-treeview .k-group").find("li").show();
            }

          
        }

        function addOffice(officeId) {
            notificationService.displaySuccess("Please enter office information");
            $rootScope.office = {};
            $rootScope.officeId = 0;
            $rootScope.office.parentId = officeId;
            $rootScope.saveButtonText = 'Save';
        }

        function deleteOffice(officeId) {
            officeService.deleteOffice(officeId).then(function (data) {
                notificationService.displaySuccess("Office deleted Successfully!!");
                    $rootScope.officeId = 0;
                    init();
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

//        $scope.onDataBound = function (e) {
//           e.sender.expand(".k-item");
//          
//           
//        }

    }

})();
