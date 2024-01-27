(function () {

    'use strict';
    var controllerId = 'extracurricularsController';
    angular.module('app').controller(controllerId, extracurricularsController);
    extracurricularsController.$inject = ['$stateParams', '$state', 'extracurricularService', 'notificationService'];

    function extracurricularsController($stateParams, $state, extracurricularService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.extracurricularId = 0;
        vm.extracurriculars = [];
        vm.title = 'Extracurricular';
        vm.addExtracurricular = addExtracurricular;
        vm.updateExtracurricular = updateExtracurricular;
        vm.deleteExtracurricular = deleteExtracurricular;

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        init();
        function init() {
            extracurricularService.getExtracurriculars(vm.employeeId).then(function (data) {
                console.log(data.result);
                vm.extracurriculars = data.result;
                    vm.permission = data.permission;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function addExtracurricular() {
            $state.go('employee-tabs.employee-extracurricular-create', { id: vm.employeeId, extracurricularId: vm.extracurricularId });
        }

 


        function updateExtracurricular(extracurricular) {
            $state.go('employee-tabs.employee-extracurricular-modify', { id: vm.employeeId, extracurricularId: extracurricular.extracurricularId });
        }

        function deleteExtracurricular(extracurricular) {
            extracurricularService.deleteExtracurricular(extracurricular.extracurricularId).then(function (data) {
                extracurricularService.getExtracurriculars(vm.employeeId).then(function (data) { 
                    vm.extracurriculars = data.result;
                });
                $state.go('employee-tabs.employee-extracurriculars');
            });
        }
    }

})();
