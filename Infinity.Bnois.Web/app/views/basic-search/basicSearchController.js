

(function () {

    'use strict';

    var controllerId = 'basicSearchController';

    angular.module('app').controller(controllerId, basicSearchController);
    basicSearchController.$inject = ['$stateParams', 'basicSearchService', 'reportService','downloadService', 'notificationService', '$state'];

    function basicSearchController($stateParams, basicSearchService, reportService, downloadService ,notificationService, $state) {
        var vm = this;
        vm.pNo = 0;
        vm.columnFilters = [];
        vm.columnDisplays = [];

        vm.columnFilterList = [];
        vm.columnDisplayList = [];


        vm.basicSearch = {};
        
        vm.getColumnFilterValue = getColumnFilterValue;
        vm.columnFilterAction = columnFilterAction;
        vm.getColumnDisplayValue = getColumnDisplayValue;
        vm.columnDisplayAction = columnDisplayAction;
        vm.refreshButton = refreshButton;
        vm.searchOfficers = searchOfficers;
        vm.downloadSearchResult = downloadSearchResult;

        vm.dropdownSetting = {
            scrollableHeight: '250px',
            scrollable: true,
            enableSearch: true,
            checkBoxes: true,
            
        }


        vm.areas = [];
        vm.adminAuthorities = [];
        vm.branches = [];
        vm.subBranches = [];
        vm.exams = [];
        vm.institutes = [];
        vm.commissionTypes = [];
        vm.ranks = [];
        vm.countries = [];
        vm.courseCategories = [];
        vm.courseSubCategories = [];
        vm.courses = [];
        vm.trainingInstitutes = [];
        vm.passingYears = [];


        vm.basicSearch.trainingInstitutesSelected = [];
        vm.basicSearch.courseCountriesSelected = [];
        vm.basicSearch.coursesDoneSelected = [];
        vm.basicSearch.coursesNotDoneSelected = [];
        vm.basicSearch.coursesDoingSelected = [];
        vm.basicSearch.courseSubCategoriesDoneSelected = [];
        vm.basicSearch.courseSubCategoriesNotDoneSelected = [];
        vm.basicSearch.courseSubCategoriesDoingSelected = [];
        vm.basicSearch.courseCategoriesDoneSelected = [];
        vm.basicSearch.courseCategoriesNotDoneSelected = [];
        vm.basicSearch.courseCategoriesDoingSelected = [];
        vm.basicSearch.countriesSelected = [];
        vm.basicSearch.ranksSelected = [];
        vm.basicSearch.commissionTypesSelected = [];
        vm.basicSearch.institutesSelected = [];
        vm.basicSearch.examsSelected = [];
        vm.basicSearch.subBranchesSelected = [];
        vm.basicSearch.branchesSelected = [];
        vm.basicSearch.adminAuthoritiesSelected = [];
        vm.basicSearch.areasSelected = [];
      


        Init();
        function Init() {
            basicSearchService.getBasicSearchSelectModels().then(function (data) {
                vm.columnFilters = data.result.columnFilters;
                vm.columnDisplays = data.result.columnDisplays;
                vm.areas = data.result.areas;
                vm.adminAuthorities = data.result.adminAuthorities;
                vm.branches = data.result.branches;
                vm.subBranches = data.result.subBranches;
                vm.exams = data.result.exams;
                vm.results = data.result.results;
                vm.institutes = data.result.institutes;
                vm.commissionTypes = data.result.commissionTypes;
                vm.ranks = data.result.ranks;
                vm.countries = data.result.countries;
                vm.courseCategories = data.result.courseCategories;
                vm.courseSubCategories = data.result.courseSubCategories;
                vm.trainingInstitutes = data.result.trainingInstitutes;
                vm.courses = data.result.courses;
                vm.passingYears = data.result.passingYears;


                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

        
        };



        function getColumnFilterValue(value, checked) {
            if (value == 1 && checked) {
                vm.areaShow = true;
            }
            else if (value == 1 && !checked) {
                vm.areaShow = false;
            }

            else if (value == 2 && checked) {
                vm.adminShow = true;
            }
            else if (value == 2 && !checked) {
                vm.adminShow = false;
            }

            else if (value == 3 && checked) {
                vm.branchShow = true;
            }
            else if (value == 3 && !checked) {
                vm.branchShow = false;
            }
            else if (value == 4 && checked) {
                vm.subBranchShow = true;
            }
            else if (value == 4 && !checked) {
                vm.subBranchShow = false;
            }
            else if (value == 5 && checked) {
                vm.civilEducationShow = true;
            }
            else if (value == 5 && !checked) {
                vm.civilEducationShow = false;
            }
            else if (value == 6 && checked) {
                vm.commSvcShow = true;
            }
            else if (value == 6 && !checked) {
                vm.commSvcShow = false;
            }
            else if (value == 7 && checked) {
                vm.commissionTypeShow = true;
            }
            else if (value == 7 && !checked) {
                vm.commissionTypeShow = false;
            }
            else if (value == 8 && checked) {
                vm.courseShow = true;
            }
            else if (value == 8 && !checked) {
                vm.courseShow = false;
            }
            else if (value == 9 && checked) {
                vm.officerNameShow = true;
            }
            else if (value == 9 && !checked) {
                vm.officerNameShow = false;
            }
            else if (value == 10 && checked) {
                vm.pNoShow = true;
            }
            else if (value == 10 && !checked) {
                vm.pNoShow = false;
            }

            else if (value == 11 && checked) {
                vm.rankShow = true;
            }
            else if (value == 11 && !checked) {
                vm.rankShow = false;
            }
            else if (value == 12 && checked) {
                vm.seaServiceShow = true;
            }
            else if (value == 12 && !checked) {
                vm.seaServiceShow = false;
            }
            vm.buttonShow = true;
            vm.searchPanel = true;
            vm.officerList = false;

        }

        function columnFilterAction() {
           
            vm.columnFilterList = [];
            vm.basicSearch = {};


            vm.basicSearch.trainingInstitutesSelected = [];
            vm.basicSearch.courseCountriesSelected = [];
            vm.basicSearch.coursesDoneSelected = [];
            vm.basicSearch.coursesNotDoneSelected = [];
            vm.basicSearch.coursesDoingSelected = [];
            vm.basicSearch.courseSubCategoriesDoneSelected = [];
            vm.basicSearch.courseSubCategoriesNotDoneSelected = [];
            vm.basicSearch.courseSubCategoriesDoingSelected = [];
            vm.basicSearch.courseCategoriesDoneSelected = [];
            vm.basicSearch.courseCategoriesNotDoneSelected = [];
            vm.basicSearch.courseCategoriesDoingSelected = [];
            vm.basicSearch.countriesSelected = [];
            vm.basicSearch.ranksSelected = [];
            vm.basicSearch.commissionTypesSelected = [];
            vm.basicSearch.institutesSelected = [];
            vm.basicSearch.examsSelected = [];
            vm.basicSearch.subBranchesSelected = [];
            vm.basicSearch.branchesSelected = [];
            vm.basicSearch.adminAuthoritiesSelected = [];
            vm.basicSearch.areasSelected = [];




          
            vm.seaServiceShow = false;
          
            vm.rankShow = false;

            vm.officerNameShow = false;
            vm.pNoShow = false;
            vm.officerServiceShow = false;
            vm.areaShow = false;
            vm.adminShow = false;
            vm.branchShow = false;
            vm.subBranchShow = false;
            vm.civilEducationShow = false;
            vm.commSvcShow = false;
            vm.commissionTypeShow = false;
            vm.courseShow = false;
            vm.buttonShow = false;
            vm.officerList = false;
            vm.searchPanel = false;
        
        }



        function getColumnDisplayValue(value, checked) {
            basicSearchService.saveCheckedValue(checked, value).then(function (data) {

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

        }
        function columnDisplayAction() {

            vm.columnDisplayList = [];

            basicSearchService.deleteCheckedColumn().then(function (data) {
            });
        }


        function refreshButton() {
            columnFilterAction();
            columnDisplayAction();
        }

        function searchOfficers() {
            basicSearchService.searchOfficers(vm.basicSearch).then(function (data) {
                    vm.officerList = true;
                    vm.buttonShow = true;
                    vm.searchPanel = false;
                      vm.basicSearch = {};
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function downloadSearchResult() {
            var url = reportService.downloadSearchResult();
            downloadService.downloadReport(url);
        }
    }
})();
