(function () {

    'use strict';
    var controllerId = 'educationsController';
    angular.module('app').controller(controllerId, educationsController);
    educationsController.$inject = ['$stateParams', '$state', 'educationService', 'notificationService'];

    function educationsController($stateParams, $state, educationService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.educationId = 0;
        vm.educations = [];
        vm.title = 'Education';
        vm.addEducation = addEducation;
        vm.updateEducation = updateEducation;
        vm.deleteEducation = deleteEducation;
        vm.uploadCertificate = uploadCertificate;

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        init();
        function init() {
            educationService.getEducations(vm.employeeId).then(function (data) {
                vm.educations = data.result;
                    vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function addEducation() {
            $state.go('employee-tabs.employee-education-create', { id: vm.employeeId, educationId: vm.educationId });
        }

        function deleteEducation(education) {
            educationService.deleteEducation(education.educationId).then(function (data) {
                educationService.getEducations(vm.employeeId).then(function(data) {
                    vm.educations = data.result;
                });
                $state.go('employee-tabs.employee-educations');
            });
        }


        function updateEducation(education) {
            $state.go('employee-tabs.employee-education-modify', { id: vm.employeeId, educationId: education.educationId });
        }


        function uploadCertificate(education) {
            $state.go('employee-tabs.employee-education-upload-certificate', { educationId:education.educationId });
        }
    }

})();
