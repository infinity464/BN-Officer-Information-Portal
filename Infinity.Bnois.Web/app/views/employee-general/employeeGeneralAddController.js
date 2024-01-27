(function () {

    'use strict';
    var controllerId = 'employeeGeneralAddController';
    angular.module('app').controller(controllerId, employeeGeneralAddController);
    employeeGeneralAddController.$inject = ['$stateParams', '$state', 'employeeGeneralService', 'notificationService'];

    function employeeGeneralAddController($stateParams, $state, employeeGeneralService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.employeeGeneralId = 0;
        vm.isMarried = false;
        vm.employeeGeneral = {};
        vm.categories = [];
        vm.subCategories = [];
        vm.branches = [];
        vm.subBranches = [];
        vm.commissionTypes = [];
        vm.subjects = [];
        vm.nationalities = [];
        vm.religions = [];
        vm.religionCasts = [];
        vm.officerStreams = [];
        vm.maritalTypes = [];
        vm.executionRemarks = [];
        vm.employeeGeneralForm = {};
        vm.getMaritalStatus = getMaritalStatus;
        vm.save = save;
        vm.close = close;
        vm.saveButtonText = 'Save';
        vm.title = 'ADD MODE';
    

        vm.getSubCategoryByCategoryId = getSubCategoryByCategoryId;
        vm.getReligionCastByReligionId = getReligionCastByReligionId;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
            vm.title = 'UPDATE MODE';
            vm.saveButtonText = 'Update';
        }
      

        init();
        function init() {
            employeeGeneralService.getEmployeeGeneral(vm.employeeId).then(function (data) {
                vm.employeeGeneral = data.result.employeeGeneral;
                vm.categories = data.result.categories;
                vm.branches = data.result.branches;
                vm.subBranches = data.result.subBranches;
                vm.commissionTypes = data.result.commissionTypes;
                vm.subjects = data.result.subjects;
                vm.subCategories = data.result.subCategories;
                vm.nationalities = data.result.nationalities;
                vm.maritalTypes = data.result.maritalTypes;
                vm.executionRemarks = data.result.executionRemarks;
                getMaritalStatus(vm.employeeGeneral);
                vm.religions = data.result.religions;
                vm.officerStreams = data.result.officerStreams;
                vm.religionCasts = data.result.religionCasts;
                vm.employeeGeneral.doB = new Date(vm.employeeGeneral.doB);

                if (vm.employeeGeneral.bExecutionDate != null) {
                    vm.employeeGeneral.bExecutionDate = new Date(vm.employeeGeneral.bExecutionDate);
                }
                if (vm.employeeGeneral.joiningDate != null) {
                    vm.employeeGeneral.joiningDate = new Date(vm.employeeGeneral.joiningDate);
                }
                if (vm.employeeGeneral.deadDate != null) {
                    vm.employeeGeneral.deadDate = new Date(vm.employeeGeneral.deadDate);
                }
                 if (vm.employeeGeneral.commissionDate != null) {
                        vm.employeeGeneral.commissionDate = new Date(vm.employeeGeneral.commissionDate);
                    }

                if (vm.employeeGeneral.marriageDate != null) {
                    vm.employeeGeneral.marriageDate = new Date(vm.employeeGeneral.marriageDate);
                    }

                if (vm.employeeGeneral.lieutenantDate != null) {
                    vm.employeeGeneral.lieutenantDate = new Date(vm.employeeGeneral.lieutenantDate);
                    }
                   
                if (vm.employeeGeneral.migrationDate != null) {
                    vm.employeeGeneral.migrationDate = new Date(vm.employeeGeneral.migrationDate);
                }
                if (vm.employeeGeneral.seniorityDate != null) {
                    vm.employeeGeneral.seniorityDate = new Date(vm.employeeGeneral.seniorityDate);
                }
                if (vm.employeeGeneral.lastRLAvailedDate != null) {
                    vm.employeeGeneral.lastRLAvailedDate = new Date(vm.employeeGeneral.lastRLAvailedDate);
                }

                if (vm.employeeGeneral.contractEndDate != null) {
                    vm.employeeGeneral.contractEndDate = new Date(vm.employeeGeneral.contractEndDate);
                }
                  
                    if (vm.employeeGeneral.nationalityId== null) {
                        vm.employeeGeneral.nationalityId = 12;
                    }
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function getSubCategoryByCategoryId(categoryId) {
            employeeGeneralService.getSubCategoryByCategoryId(categoryId).then(function (data) {
                vm.subCategories = data.result.subCategories;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function getReligionCastByReligionId(religionId) {
            employeeGeneralService.getReligionCastByReligionId(religionId).then(function (data) {
                vm.religionCasts = data.result.religionCasts;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function save() {
            updateEmployeeGeneral();
        }

        function updateEmployeeGeneral() {
            employeeGeneralService.updateEmployeeGeneral(vm.employeeId, vm.employeeGeneral).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function close() {
            $state.go('employee-tabs.employee-generals');
        }

        function getMaritalStatus(employeeGeneral) {
            if (employeeGeneral.maritalTypeId == 1) {
                vm.isMarried = true;
            }
            else {
                vm.isMarried = false;
                employeeGeneral.marriageDate = null;
            }
        }

    }

})();
