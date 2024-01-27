(function () {

    'use strict';
    var controllerId = 'extracurricularAddController';
    angular.module('app').controller(controllerId, extracurricularAddController);
    extracurricularAddController.$inject = ['$stateParams', '$state', 'extracurricularService', 'notificationService'];

    function extracurricularAddController($stateParams, $state, extracurricularService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.extracurricularId = 0;
        vm.extracurricular = {};
        vm.extracurricularTypes = [];
        vm.title = 'ADD MODE';
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.extracurricularForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
        }
        if ($stateParams.extracurricularId > 0) {
            vm.extracurricularId = $stateParams.extracurricularId;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }
        init();
        function init() {
            extracurricularService.getExtracurricular(vm.employeeId, vm. extracurricularId).then(function (data) {
                vm.extracurricular = data.result.extracurricular;
                vm.extracurricularTypes = data.result.extracurricularTypes;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        

        function save() {
            if (vm.extracurricularId > 0 && vm.extracurricularId !== '') {
                updateExtracurricular();
            } else {
                insertExtracurricular();
            }
        }
        function insertExtracurricular() {
            extracurricularService.saveExtracurricular(vm.employeeId, vm.extracurricular).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateExtracurricular() {
            extracurricularService.updateExtracurricular(vm.extracurricularId, vm.extracurricular).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        
        function close() {
            $state.go('employee-tabs.employee-extracurriculars');
        }
    }

})();
