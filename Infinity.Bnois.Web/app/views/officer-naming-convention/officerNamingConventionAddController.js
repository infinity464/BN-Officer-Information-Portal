
(function () {

    'use strict';

    var controllerId = 'officerNamingConventionAddController';

    angular.module('app').controller(controllerId, officerNamingConventionAddController);
    officerNamingConventionAddController.$inject = ['$stateParams', 'subCategoryService', 'notificationService', '$state'];

    function officerNamingConventionAddController($stateParams, subCategoryService, notificationService, $state) {
        var vm = this;
        vm.subCategoryId = 0;
        vm.title = 'ADD MODE';
        vm.subCategory = {};
        vm.subCategories = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.subCategoryForm = {};

        vm.setPrefix = setPrefix;
        vm.setRank = setRank;
        vm.setPrefix2 = setPrefix2;
        vm.setBranch = setBranch;
        vm.setSubBranch = setSubBranch;
        vm.setMedal = setMedal;
        vm.setAward = setAward;
        vm.setCourse = setCourse;
        vm.setBN = setBN;
        vm.setBNVR = setBNVR;

        vm.prefix = "";
        vm.rank = "";
        vm.prefix2 = "";
        vm.branch = "";
        vm.subBranch = "";
        vm.medal = "";
        vm.award = "";
        vm.course = "";
        vm.bn = "";
        vm.bnvr = "";

        vm.shortName = "";
        vm.subCategory.nmConEx = vm.prefix +
            vm.rank +
            " XYZ " +
            vm.prefix2 +
            vm.branch +
            vm.subBranch +
            vm.medal +
            vm.award +
            vm.course +
            vm.bn +
            vm.bnvr;


        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.subCategoryId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            subCategoryService.getSubCategory(vm.subCategoryId).then(function (data) {
                vm.subCategory = data.result.subCategory;
                vm.shortName = vm.subCategory.shortName;

                vm.setPrefix(vm.subCategory.prefix);
                vm.setRank(vm.subCategory.rank);
                vm.setPrefix2(vm.subCategory.prefix2);
                vm.setBranch(vm.subCategory.branch);
                vm.setSubBranch(vm.subCategory.subBranch);
                vm.setMedal(vm.subCategory.medal);
                vm.setAward(vm.subCategory.award);
                vm.setCourse(vm.subCategory.course);
                vm.setBN(vm.subCategory.bn);
                vm.setBNVR(vm.subCategory.bnvr);
              


                if (vm.subCategory.nmConEx == null  ) {
                    vm.subCategory.nmConEx = vm.prefix +
                        vm.rank +
                        " XYZ " +
                        vm.prefix2 +
                        vm.branch +
                        vm.subBranch +
                        vm.medal +
                        vm.award +
                        vm.course +
                        vm.bn +
                        vm.bnvr;

                    
                }
                   
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            updateSubCategory();

        }


        function updateSubCategory() {
            subCategoryService.updateSubCategory(vm.subCategoryId, vm.subCategory).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('officer-naming-conventions');
        }

        function setPrefix(result) {
            
            if (result) {
               
                vm.prefix = vm.shortName;
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ " +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;

            } else {
                vm.prefix = "";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ" +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            }
            
        }
        function setRank(result) {

            if (result) {
                vm.rank = " Lt Cdr";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ" +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            } else {
                vm.rank = "";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ" +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            }

        }


        function setPrefix2(result) {

            if (result) {
                vm.prefix2 = vm.shortName;
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ, " +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            } else {
                vm.prefix2 = "";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ" +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            }
        }

        function setBranch(result) {

            if (result) {
                vm.branch = " (X) ";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ, " +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            } else {
                vm.branch = "";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ" +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            }
        }

        function setSubBranch(result) {

            if (result) {
                vm.subBranch = " (AX), ";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ, " +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            } else {
                vm.subBranch = "";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ" +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            }
        }


        function setMedal(result) {

            if (result) {
                vm.medal = " BB, ";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ, " +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            } else {
                vm.medal = "";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ" +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            }
        }


        function setAward(result) {

            if (result) {
                vm.award = " AA,";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ, " +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            } else {
                vm.award = "";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ" +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            }
        }


        function setCourse(result) {

            if (result) {
                vm.course = " ncc, psc, ";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ, " +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            } else {
                vm.course = "";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ" +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            }
        }


        function setBN(result) {

            if (result) {
                vm.bn = " BN ";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ, " +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            } else {
                vm.bn = "";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ" +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            }
        }

        function setBNVR(result) {

            if (result) {
                vm.bnvr = " BNVR ";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ, " +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            } else {
                vm.bnvr = "";
                vm.subCategory.nmConEx = vm.prefix +
                    vm.rank +
                    " XYZ" +
                    vm.prefix2 +
                    vm.branch +
                    vm.subBranch +
                    vm.medal +
                    vm.award +
                    vm.course +
                    vm.bn +
                    vm.bnvr;
            }
        }

      

    }
})();
