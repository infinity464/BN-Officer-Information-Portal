(function () {

    'use strict';
    var controllerId = 'physicalConditionAddController';
    angular.module('app').controller(controllerId, physicalConditionAddController);
    physicalConditionAddController.$inject = ['$stateParams', '$state', 'physicalConditionService', 'notificationService'];

    function physicalConditionAddController($stateParams, $state, physicalConditionService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.physicalCondition = {};
        vm.eyeColors = [];
        vm.skinColors = [];
        vm.hairColors = [];
        vm.eyeVisions = [];
        vm.bloodGroups = [];
        vm.medicalCategories = [];
        vm.physicalStructures = [];
        vm.physicalConditionForm = {};
        vm.save = save;
        vm.close = close;
        vm.saveButtonText = 'Save';
        vm.titile = 'ADD MODE';

        //----------------------------------------------------
        vm.inchInput = 0;
        vm.feet = 0;
        vm.inch = 0;
        vm.convertFeetAndInch = convertFeetAndInch;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
            vm.titile = 'UPDATE MODE';
        }
        init();
        function init() {
            physicalConditionService.getPhysicalCondition(vm.employeeId).then(function (data) {
                vm.physicalCondition = data.result.physicalCondition;
                vm.eyeColors = data.result.eyeColors;
                vm.hairColors = data.result.hairColors;
                vm.skinColors = data.result.skinColors;
                vm.bloodGroups = data.result.bloodGroups;
                vm.eyeVisions = data.result.eyeVisions;
                vm.medicalCategories = data.result.medicalCategories;
                vm.physicalStructures = data.result.physicalStructures;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        
        
        function save() {
            updatePhysicalCondition();
        }

        function updatePhysicalCondition() {
            physicalConditionService.updatePhysicalCondition(vm.employeeId, vm.physicalCondition).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function close() {
            $state.go('employee-tabs.physical-conditions');
        }

        //Centimetre to Feet and Inch Convertor
        // 1cm=0.393701 inch
        function convertFeetAndInch(cmInput) {
            vm.inch = cmInput * 0.393701;
            vm.feet = parseInt(vm.inch / 12);
            vm.inch = (vm.inch % 12).toFixed(2);
        }

    }

})();
