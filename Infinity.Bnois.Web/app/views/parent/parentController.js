(function () {
    'use strict';
    var controllerId = 'parentController';
    angular.module('app').controller(controllerId, parentController);
    parentController.$inject = ['$stateParams', '$state', 'parentService','religionCastService','notificationService'];

    function parentController($stateParams, $state, parentService, religionCastService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.relationType = 0;
        vm.title = '';
        vm.parent = {};
        vm.religions = [];
        vm.religionCasts = [];
        vm.countries = [];
        vm.nationalities = [];
        vm.occupations = [];
        vm.rankCategories = [];
        vm.employeeId = 0;
        vm.relationType = '';
        vm.parentForm = {};
        vm.save = save;
        vm.updateParent = updateParent;
        vm.getReligionCastByReligion = getReligionCastByReligion;
        vm.uploadParentImage = uploadParentImage;
        vm.close = close;
        vm.saveButtonText = 'Save';
        vm.relationName = '';

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
            vm.saveButtonText = 'Update';
        }
        if ($stateParams.relationType !== undefined && $stateParams.relationType !== null) {
            vm.relationType = $stateParams.relationType;
            if (vm.relationType == 1) {
                vm.title = 'Father Information';
                vm.relationName = 'father';
            }
            else if (vm.relationType == 2) {
                vm.title = 'Mother Information';
                vm.relationName = 'mother';
            }
            else if (vm.relationType == 3) {
                vm.title = 'Step Father Information';
                vm.relationName = 'stepfather';
            }
            else if (vm.relationType == 4) {
                vm.title = 'Step Mother Information';
                vm.relationName = 'stepmother';
            }
            else {
                vm.title = '';
            }
           
        }
        init();
        function init() {
            parentService.getParent(vm.employeeId, vm.relationType).then(function (data) {
                vm.parent = data.result.parent;
                if (vm.parent.parentId>0) {
                    vm.religionCasts = data.result.religionCasts;

                    if (vm.parent.doB != null) {
                        vm.parent.doB = new Date(data.result.parent.doB);
                    }

                    if (vm.parent.migrationDate != null) {
                        vm.parent.migrationDate = new Date(data.result.parent.migrationDate);
                    }

                    if (vm.parent.previousNationalityDate != null) {
                        vm.parent.previousNationalityDate = new Date(data.result.parent.previousNationalityDate);
                    }
                    
                }
                vm.religions = data.result.religions;
                vm.countries = data.result.countries;
                vm.nationalities = data.result.nationalities;
                vm.occupations = data.result.occupations;
                vm.rankCategories = data.result.rankCategories;

                    if (vm.parent.nationalityId == null) {
                        vm.parent.nationalityId = 12;
                    }
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function save() {
            update();
        }

        function updateParent(parent) {
            $state.go('employee-tabs.employee-parents-modify', { relationType: vm.relationType});
        }

        function update() {
            vm.parent.relationType = vm.relationType;
            parentService.updateParent(vm.employeeId, vm.parent).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function close() {
            $state.go('employee-tabs.employee-' + vm.relationName, { relationType: vm.relationType });
        }


        function getReligionCastByReligion(religionId) {
            religionCastService.getReligionCastByReligion(religionId).then(function (data) {
                vm.religionCasts = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function uploadParentImage() {
            $state.go('employee-tabs.employee-parent-image', {relationType: vm.relationType });
        }
    }

})();
