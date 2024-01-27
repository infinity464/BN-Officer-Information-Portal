

(function () {

    'use strict';

    var controllerId = 'advanceSearchController';

    angular.module('app').controller(controllerId, advanceSearchController);
    advanceSearchController.$inject = ['$stateParams', 'advanceSearchService','visitSubCategoryService','medalAwardService', 'punishmentSubCategoryService','officeService', 'officeAppointmentService','courseService' ,'downloadService','reportService', 'notificationService', '$state'];

    function advanceSearchController($stateParams, advanceSearchService, visitSubCategoryService, medalAwardService,punishmentSubCategoryService,officeService, officeAppointmentService, courseService,downloadService, reportService ,notificationService, $state) {
        var vm = this;
        vm.pNo = 0;
        vm.columnFilters = [];
        vm.columnDisplays = [];

        vm.columnFilterList = [];
        vm.columnDisplayList = [];
        
        vm.totalResult = 0;

        vm.advanceSearch = {};
        vm.advanceSearch2 = {};
        vm.advanceSearch3 = {};
        
        vm.getColumnFilterValue = getColumnFilterValue;
        vm.columnFilterAction = columnFilterAction;
        vm.getColumnDisplayValue = getColumnDisplayValue;
        vm.columnDisplayAction = columnDisplayAction;
        vm.refreshButton = refreshButton;
        vm.searchOfficers = searchOfficers;
        vm.attachOfficeSelect = attachOfficeSelect;
        vm.attachOfficeDeSelect = attachOfficeDeSelect;
        vm.attachOfficeSelectAll = attachOfficeSelectAll;
        vm.notServedAttachOfficeSelect = notServedAttachOfficeSelect;
        vm.notServedAttachOfficeDeSelect = notServedAttachOfficeDeSelect;
        vm.notServedAttachOfficeSelectAll = notServedAttachOfficeSelectAll;
        vm.servingAttachOfficeSelect = servingAttachOfficeSelect;
        vm.servingAttachOfficeDeSelect = servingAttachOfficeDeSelect;
        vm.servingAttachOfficeSelectAll = servingAttachOfficeSelectAll;
        vm.getServingOfficeSelectModelByShip = getServingOfficeSelectModelByShip;
        vm.getOfficeSelectModelByShip = getOfficeSelectModelByShip;
        vm.getNotServedOfficeSelectModelByShip = getNotServedOfficeSelectModelByShip;
        vm.getServedChildOfficeSelectModel = getServedChildOfficeSelectModel;
        vm.getNotServedChildOfficeSelectModel = getNotServedChildOfficeSelectModel;
        vm.getServingChildOfficeSelectModel = getServingChildOfficeSelectModel;
        vm.getServingChildOfficeSelectModelDeSelect = getServingChildOfficeSelectModelDeSelect;

        vm.doneCourseCategorySelect = doneCourseCategorySelect;
        vm.doneCourseCategoryDeSelect = doneCourseCategoryDeSelect;
        vm.doneCourseCategorySelectAll = doneCourseCategorySelectAll;
        vm.notDoneCourseCategorySelect = notDoneCourseCategorySelect;
        vm.notDoneCourseCategoryDeSelect = notDoneCourseCategoryDeSelect;
        vm.notDoneCourseCategorySelectAll = notDoneCourseCategorySelectAll;
        vm.doingCourseCategorySelect = doingCourseCategorySelect;
        vm.doingCourseCategoryDeSelect = doingCourseCategoryDeSelect;
        vm.doingCourseCategorySelectAll = doingCourseCategorySelectAll;
        vm.doneCourseSubCategorySelect = doneCourseSubCategorySelect;
        vm.doneCourseSubCategoryDeSelect = doneCourseSubCategoryDeSelect;
        vm.doneCourseSubCategorySelectAll = doneCourseSubCategorySelectAll;
        vm.notDoneCourseSubCategorySelect = notDoneCourseSubCategorySelect;
        vm.notDoneCourseSubCategoryDeSelect = notDoneCourseSubCategoryDeSelect;
        vm.notDoneCourseSubCategorySelectAll = notDoneCourseSubCategorySelectAll;
        vm.doingCourseSubCategorySelect = doingCourseSubCategorySelect;
        vm.doingCourseSubCategoryDeSelect = doingCourseSubCategoryDeSelect;
        vm.doingCourseSubCategorySelectAll = doingCourseSubCategorySelectAll; 
        vm.punishmentCategorySelect = punishmentCategorySelect;
        vm.punishmentCategoryDeSelect = punishmentCategoryDeSelect;
        vm.punishmentCategorySelectAll = punishmentCategorySelectAll;
        vm.publicationCategorySelect = publicationCategorySelect;
        vm.publicationCategoryDeSelect = publicationCategoryDeSelect;
        vm.publicationCategorySelectAll = publicationCategorySelectAll;
        vm.visitCategorySelect = visitCategorySelect;
        vm.visitCategoryDeSelect = visitCategoryDeSelect;
        vm.visitCategorySelectAll = visitCategorySelectAll;


        vm.dropdownSetting = {
            scrollableHeight: '250px',
            scrollable: true,
            enableSearch: true,
            checkBoxes: true,
            clearSearchOnClose: true
            
        }


        vm.transferDropdownSetting = {
            scrollableHeight: '250px',
            scrollable: true,
            enableSearch: false,
            checkBoxes: true,
            showAllSelectedText: true,
            showCheckAll: false,
            showUncheckAll: false,

        }


        vm.buttonShow = true;

        vm.areas = [];
        vm.areaTypes = [];
        vm.adminAuthorities = [];
        vm.batches = [];
        vm.branches = [];
        vm.subBranches = [];
        vm.bloodGroups = []; 
        vm.exams = [];
        vm.institutes = [];
        vm.commissionTypes = [];
        vm.currentStatus = [];
        vm.districts = [];
        vm.medicalCategories = [];
        vm.ranks = [];
        vm.subjects = [];
        vm.countries = [];
        vm.visitCategories = [];
        vm.physicalStructures = [];
        vm.promotionStatus = [];       
        vm.categories = [];       
        vm.subCategories = [];
        vm.religions = [];
        vm.religionCasts = [];
        vm.officerServices = [];
        vm.occasions = [];        
        vm.serviceExamCategories = [];

        vm.doneCourseCategories = [];
        vm.doneCourseSubCategories = [];
        vm.doneCourses = [];

        vm.notDoneCourseCategories = [];
        vm.notDoneCourseSubCategories = [];
        vm.notDoneCourses = [];

        vm.doingCourseCategories = [];
        vm.doingCourseSubCategories = [];
        vm.doingCourses = [];

        vm.doneCoursesByCategory = [];
        vm.doingCoursesByCategory = [];
        vm.notDoneCoursesByCategory = [];

        vm.trainingInstitutes = [];
        vm.passingYears = [];
        vm.searchResults = [];
        vm.transferAreas = [];
        vm.offices = [];
        vm.officeAppointments = [];
        vm.notServedOfficeAppointments = [];
        vm.servingOfficeAppointments = [];

        vm.punishmentCategories = [];
        vm.punishmentSubCategories = [];
        vm.publications = [];
        vm.publicationCategories = [];
        vm.visitSubCategories = [];
        vm.clearances = [];
        vm.appointmentCategories = [];
        vm.patterns = [];


//        Copy Array
        vm.publicationsCopy = [];
        vm.punishmentSubCategoriesCopy = [];
        vm.officeAppointmentsCopy = [];
        vm.notServedOfficeAppointmentsCopy = [];
        vm.servingOfficeAppointmentsCopy = [];
        vm.doingCourseSubCategoriesCopy = [];
        vm.doingCoursesCopy = [];
        vm.notDoneCourseSubCategoriesCopy = [];
        vm.notDoneCoursesCopy = [];
        vm.visitSubCategoriesCopy = [];

        vm.officeAppointmentsByShip = [];
        vm.notServedOfficeAppointmentsByShip = [];
        vm.servingOfficeAppointmentsByShip = [];

        vm.advanceSearch.trainingInstitutesSelected = [];
        vm.advanceSearch.transferAdminAuthoritiesSelected = [];
        vm.advanceSearch.courseCountriesSelected = [];
        vm.advanceSearch.coursesDoneSelected = [];
        vm.advanceSearch.coursesNotDoneSelected = [];
        vm.advanceSearch.coursesDoingSelected = [];
        vm.advanceSearch.courseSubCategoriesDoneSelected = [];
        vm.advanceSearch.courseSubCategoriesNotDoneSelected = [];
        vm.advanceSearch.courseSubCategoriesDoingSelected = [];
        vm.advanceSearch.courseCategoriesDoneSelected = [];
        vm.advanceSearch.courseCategoriesNotDoneSelected = [];
        vm.advanceSearch.courseCategoriesDoingSelected = [];
        vm.advanceSearch.oprRanksSelected = [];
        vm.advanceSearch.occasionsSelected = [];
        vm.advanceSearch.officerServicesSelected = [];
        vm.advanceSearch.religionCastsSelected = [];
        vm.advanceSearch.religionsSelected = [];
        vm.advanceSearch.subCategoriesSelected = [];
        vm.advanceSearch.categoriesSelected = [];
        vm.advanceSearch.promotionStatusSelected = [];
        vm.advanceSearch.physicalStructuresSelected = [];
        vm.advanceSearch.visitCategoriesSelected = [];
        vm.advanceSearch.visitSubCategoriesSelected = [];
        vm.advanceSearch.visitCountriesSelected = [];
        vm.advanceSearch.countriesSelected = [];
        vm.advanceSearch.subjectsSelected = [];
        vm.advanceSearch.ranksSelected = [];
        vm.advanceSearch.medicalCategoriesSelected = [];
        vm.advanceSearch.districtsSelected = [];
        vm.advanceSearch.currentStatusSelected = [];
        vm.advanceSearch.commissionTypesSelected = [];
        vm.advanceSearch.institutesSelected = [];
        vm.advanceSearch.examsSelected = [];
        vm.advanceSearch.bloodGroupsSelected = [];
        vm.advanceSearch.subBranchesSelected = [];
        vm.advanceSearch.branchesSelected = [];
        vm.advanceSearch.batchesSelected = [];
        vm.advanceSearch.adminAuthoritiesSelected = [];
        vm.advanceSearch.areasSelected = [];
        vm.advanceSearch.areaTypesSelected = [];
        vm.advanceSearch.transferAreasSelected = [];
        vm.advanceSearch.missionCountriesSelected = [];
        vm.advanceSearch.resultsSelected = [];
        vm.advanceSearch.officesSelected = [];
        vm.advanceSearch.notServedOfficesSelected = [];
        vm.advanceSearch.servingOfficesSelected = [];
        vm.advanceSearch.appointmentsSelected = [];
        vm.advanceSearch.notServedAppointmentsSelected = [];
        vm.advanceSearch.servingAppointmentsSelected = [];
        vm.advanceSearch.punishmentCategoriesSelected = [];
        vm.advanceSearch.punishmentSubCategoriesSelected = [];

        vm.advanceSearch.publicationCategoriesSelected = [];
        vm.advanceSearch.publicationsSelected = [];
        vm.advanceSearch.medalsSelected = [];
        vm.advanceSearch.awardsSelected = [];
        vm.advanceSearch.commendationsSelected = [];
        vm.advanceSearch.appreciationsSelected = [];
        vm.advanceSearch.leaveCountriesSelected = [];
        vm.advanceSearch.shipsSelected = [];
        vm.advanceSearch.clearancesSelected = [];
        vm.advanceSearch.doingCourseCountriesSelected = [];
        vm.advanceSearch.notDoneCourseCountriesSelected = [];
        vm.advanceSearch.appointmentCategoriesSelected = [];
        vm.advanceSearch.educationSubjectsSelected = [];
        vm.advanceSearch.servingOrgSelected = [];


        vm.statusTypes = [
            { 'value': 1, 'text': 'Medical Category' }, { 'value': 2, 'text': 'Eye Vision' },
            { 'value': 3, 'text': 'Commission Type' }, { 'value': 4, 'text': 'Branch' }
        ];

        Init();
        function Init() {

            advanceSearchService.deleteCheckedColumn().then(function (data) {
            });
               


            advanceSearchService.getAdvanceSearchSelectModels().then(function (data) {
                vm.columnFilters = data.result.columnFilters;
                vm.columnDisplays = data.result.columnDisplays;
                vm.areas = data.result.areas;
                vm.areaTypes = data.result.areaTypes;
                vm.adminAuthorities = data.result.adminAuthorities;
                vm.batches = data.result.batches;
                vm.branches = data.result.branches;
                vm.subBranches = data.result.subBranches;
                vm.bloodGroups = data.result.bloodGroups;
                vm.exams = data.result.exams;
                vm.results = data.result.results;
                vm.institutes = data.result.institutes;
                vm.commissionTypes = data.result.commissionTypes;
                vm.currentStatus = data.result.currentStatus;
                vm.districts = data.result.districts;
                vm.medicalCategories = data.result.medicalCategories;
                vm.ranks = data.result.ranks;
                vm.subjects = data.result.subjects;
                vm.countries = data.result.countries;
                vm.visitCategories = data.result.visitCategories;
                vm.physicalStructures = data.result.physicalStructures;
                vm.promotionStatus = data.result.promotionStatus;
                vm.categories = data.result.categories;
                vm.subCategories = data.result.subCategories;
                vm.religions = data.result.religions;
                vm.religionCasts = data.result.religionCasts;
                vm.officerServices = data.result.officerServices;
                vm.occasions = data.result.occasions;
                vm.serviceExamCategories = data.result.serviceExamCategories;

                vm.trainingInstitutes = data.result.trainingInstitutes;
               

                vm.doneCourseCategories = data.result.courseCategories;
                vm.doneCourseSubCategoriesCopy = data.result.courseSubCategories;
                vm.doneCoursesCopy = data.result.courses;

                vm.notDoneCourseCategories = data.result.courseCategories;
                vm.notDoneCourseSubCategoriesCopy = data.result.courseSubCategories;
                vm.notDoneCoursesCopy = data.result.courses;

                vm.doingCourseCategories = data.result.courseCategories;
                vm.doingCourseSubCategoriesCopy = data.result.courseSubCategories;
                vm.doingCoursesCopy = data.result.courses;


                vm.passingYears = data.result.passingYears;
                vm.transferAreas = data.result.transferAreas;
                vm.offices = data.result.offices;
                vm.notServedOffices = data.result.offices;
                vm.servingOffices = data.result.offices;
                vm.punishment = data.result.offices;
                vm.servingOffices = data.result.offices;
                vm.punishmentCategories = data.result.punishmentCategories;
                vm.punishmentSubCategoriesCopy = data.result.punishmentSubCategories;
                vm.servingOfficeAppointmentsCopy = data.result.officeAppointments;
                vm.notServedOfficeAppointmentsCopy = data.result.officeAppointments;
                vm.officeAppointmentsCopy = data.result.officeAppointments;
                vm.awards = data.result.awards;
                vm.medals = data.result.medals;
                vm.publicationCategories = data.result.publicationCategories;
                vm.publicationsCopy = data.result.publications;
                vm.achievements = data.result.achievements;
                vm.leaveCountries = data.result.countries;
                vm.ships = data.result.ships;
                vm.visitSubCategoriesCopy = data.result.visitSubCategories;
                vm.clearances = data.result.clearances;
                vm.appointmentCategories = data.result.appointmentCategories;
                vm.patterns = data.result.patterns;
                vm.patterns.push({
                    "value": -404,
                    "text": "Coast Guard"
                });
                  
                vm.achievements.push({
                    "value": -404,
                    "text": "Notable Achievement"
                });
                  

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
                vm.serviceExtensionShow = true;
            }
            else if (value == 2 && !checked) {
                vm.serviceExtensionShow = false;
            }
            else if (value == 3 && checked) {
                vm.ageShow = true;
            }
            else if (value == 3 && !checked) {
                vm.ageShow = false;
            }
            else if (value == 4 && checked) {
                vm.batchShow = true;
            }
            else if (value == 4 && !checked) {
                vm.batchShow = false;
            }
            else if (value == 5 && checked) {
                vm.branchShow = true;
            }
            else if (value == 5 && !checked) {
                vm.branchShow = false;
            }
            else if (value == 6 && checked) {
                vm.subBranchShow = true;
            }
            else if (value == 6 && !checked) {
                vm.subBranchShow = false;
            }
            else if (value == 7 && checked) {
                vm.bloodGroupShow = true;
            }
            else if (value == 7 && !checked) {
                vm.bloodGroupShow = false;
            }
            else if (value == 8 && checked) {
                vm.civilEducationShow = true;
            }
            else if (value == 8 && !checked) {
                vm.civilEducationShow = false;
            }
            else if (value == 9 && checked) {
                vm.commSvcShow = true;
            }
            else if (value == 9 && !checked) {
                vm.commSvcShow = false;
            }
            else if (value == 10 && checked) {
                vm.commissionTypeShow = true;
            }
            else if (value == 10 && !checked) {
                vm.commissionTypeShow = false;
            }
            else if (value == 11 && checked) {
                vm.courseShow = true;
            }
            else if (value == 11 && !checked) {
                vm.courseShow = false;
            }
            else if (value == 12 && checked) {
                vm.currentStatusShow = true;
            }
            else if (value == 12 && !checked) {
                vm.currentStatusShow = false;
            }
            else if (value == 13 && checked) {
                vm.districtShow = true;
            }
            else if (value == 13 && !checked) {
                vm.districtShow = false;
            }
            else if (value == 14 && checked) {
                vm.drivingShow = true;
            }
            else if (value == 14 && !checked) {
                vm.drivingShow = false;
            }
            else if (value == 15 && checked) {
                vm.foreignVisitShow = true;
            }
            else if (value == 15 && !checked) {
                vm.foreignVisitShow = false;
            }
            else if (value == 16 && checked) {
                vm.genderShow = true;
            }
            else if (value == 16 && !checked) {
                vm.genderShow = false;
            }
            else if (value == 17 && checked) {
                vm.heightShow = true;
            }
            else if (value == 17 && !checked) {
                vm.heightShow = false;
            }
            else if (value == 18 && checked) {
                vm.joiningDateShow = true;
            }
            else if (value == 18 && !checked) {
                vm.joiningDateShow = false;
            }
            else if (value == 19 && checked) {
                vm.lprDateShow = true;
            }
            else if (value == 19 && !checked) {
                vm.lprDateShow = false;
            }
            else if (value == 20 && checked) {
                vm.maritalStatusShow = true;
            }
            else if (value == 20 && !checked) {
                vm.maritalStatusShow = false;
            }
            else if (value == 21 && checked) {
                vm.medicalCategoryShow = true;
            }
            else if (value == 21 && !checked) {
                vm.medicalCategoryShow = false;
            }
            else if (value == 22 && checked) {
                vm.officerCategoryShow = true;
            }
            else if (value == 22 && !checked) {
                vm.officerCategoryShow = false;
            }
            else if (value == 23 && checked) {
                vm.officerServiceShow = true;
            }
            else if (value == 23 && !checked) {
                vm.officerServiceShow = false;
            }
            else if (value == 24 && checked) {
                vm.officerNameShow = true;
            }
            else if (value == 24 && !checked) {
                vm.officerNameShow = false;
            }
            else if (value == 25 && checked) {
                vm.oprShow = true;
            }
            else if (value == 25 && !checked) {
                vm.oprShow = false;
            }
            else if (value == 26 && checked) {
                vm.passportShow = true;
            }
            else if (value == 26 && !checked) {
                vm.passportShow = false;
            }
            else if (value == 27 && checked) {
                vm.physicalStructureShow = true;
            }
            else if (value == 27 && !checked) {
                vm.physicalStructureShow = false;
            }
            else if (value == 28 && checked) {
                vm.promotionStatusShow = true;
            }
            else if (value == 28 && !checked) {
                vm.promotionStatusShow = false;
            }
            else if (value == 29 && checked) {
                vm.rankShow = true;
            }
            else if (value == 29 && !checked) {
                vm.rankShow = false;
            }
            else if (value == 30 && checked) {
                vm.religionShow = true;
            }
            else if (value == 30 && !checked) {
                vm.religionShow = false;
            }
            else if (value == 31 && checked) {
                vm.retirementDateShow = true;
            }
            else if (value == 31 && !checked) {
                vm.retirementDateShow = false;
            }
            else if (value == 32 && checked) {
                vm.returnAbroadShow = true;
            }
            else if (value == 32 && !checked) {
                vm.returnAbroadShow = false;
            }
            else if (value == 33 && checked) {
                vm.selfExamShow = true;
            }
            else if (value == 33 && !checked) {
                vm.selfExamShow = false;
            }
            else if (value == 34 && checked) {
                vm.seaServiceShow = true;
            }
            else if (value == 34 && !checked) {
                vm.seaServiceShow = false;
            }
            else if (value == 35 && checked) {
                vm.subjectShow = true;
            }
            else if (value == 35 && !checked) {
                vm.subjectShow = false;
            }
            else if (value == 36 && checked) {
                vm.transferShow = true;
            }
            else if (value == 36 && !checked) {
                vm.transferShow = false;
            }
            else if (value == 37 && checked) {
                vm.missionShow = true;
               
            }
            else if (value == 37 && !checked) {
                vm.missionShow = false;
               
            }
            else if (value == 38 && checked) {
                vm.freedomFighterShow = true;
               
            }
            else if (value == 38 && !checked) {
                vm.freedomFighterShow = false;
               
            }
            else if (value == 39 && checked) {
                vm.punishmentShow = true;
               
            }
            else if (value == 39 && !checked) {
                vm.punishmentShow = false;
               
            }

            else if (value == 40 && checked) {
                vm.medalShow = true;
               

            }
            else if (value == 40 && !checked) {
                vm.medalShow = false;

            }

            else if (value == 41 && checked) {
                vm.awardShow = true;
               

            }
            else if (value == 41 && !checked) {
                vm.awardShow = false;

            }


            else if (value == 42 && checked) {
                vm.publicationShow = true;
               

            }
            else if (value == 42 && !checked) {
                vm.publicationShow = false;

            }


            else if (value == 43 && checked) {
                vm.commendationShow = true;
              

            }
            else if (value == 43 && !checked) {
                vm.commendationShow = false;

            }

            else if (value == 44 && checked) {
                vm.appreciationShow = true;
                

            }
            else if (value == 44 && !checked) {
                vm.appreciationShow = false;

            }

            else if (value == 45 && checked) {
                vm.exBdLeaveShow = true;
                

            }
            else if (value == 45 && !checked) {
                vm.exBdLeaveShow = false;

            }
            else if (value == 46 && checked) {
                vm.clearanceShow = true;


            }
            else if (value == 46 && !checked) {
                vm.clearanceShow = false;

            }


            else if (value == 47 && checked) {
                vm.issbShow = true;


            }
            else if (value == 47 && !checked) {
                vm.issbShow = false;

            }

            else if (value == 48 && checked) {
                vm.shipShow = true;


            }
            else if (value == 48 && !checked) {
                vm.shipShow = false;

            }
            else if (value == 49 && checked) {
                vm.rlDueShow = true;

            }
            else if (value == 49 && !checked) {
                vm.rlDueShow = false;

            }
            else if (value == 50 && checked) {
                vm.statusChangeShow = true;

            }
            else if (value == 50 && !checked) {
                vm.statusChangeShow = false;

            }


            vm.buttonShow = true;
            vm.searchPanel = true;
           
        }

        function columnFilterAction() {
           
            vm.columnFilterList = [];
            vm.advanceSearch = {};



            vm.advanceSearch.trainingInstitutesSelected = [];
            vm.advanceSearch.transferAdminAuthoritiesSelected = [];
            vm.advanceSearch.courseCountriesSelected = [];
            vm.advanceSearch.coursesDoneSelected = [];
            vm.advanceSearch.coursesNotDoneSelected = [];
            vm.advanceSearch.coursesDoingSelected = [];
            vm.advanceSearch.courseSubCategoriesDoneSelected = [];
            vm.advanceSearch.courseSubCategoriesNotDoneSelected = [];
            vm.advanceSearch.courseSubCategoriesDoingSelected = [];
            vm.advanceSearch.courseCategoriesDoneSelected = [];
            vm.advanceSearch.courseCategoriesNotDoneSelected = [];
            vm.advanceSearch.courseCategoriesDoingSelected = [];
            vm.advanceSearch.oprRanksSelected = [];
            vm.advanceSearch.occasionsSelected = [];
            vm.advanceSearch.officerServicesSelected = [];
            vm.advanceSearch.religionCastsSelected = [];
            vm.advanceSearch.religionsSelected = [];
            vm.advanceSearch.subCategoriesSelected = [];
            vm.advanceSearch.categoriesSelected = [];
            vm.advanceSearch.promotionStatusSelected = [];
            vm.advanceSearch.physicalStructuresSelected = [];
            vm.advanceSearch.visitCategoriesSelected = [];
            vm.advanceSearch.visitSubCategoriesSelected = [];
            vm.advanceSearch.visitCountriesSelected = [];
            vm.advanceSearch.countriesSelected = [];
            vm.advanceSearch.subjectsSelected = [];
            vm.advanceSearch.ranksSelected = [];
            vm.advanceSearch.medicalCategoriesSelected = [];
            vm.advanceSearch.districtsSelected = [];
            vm.advanceSearch.currentStatusSelected = [];
            vm.advanceSearch.commissionTypesSelected = [];
            vm.advanceSearch.institutesSelected = [];
            vm.advanceSearch.examsSelected = [];
            vm.advanceSearch.bloodGroupsSelected = [];
            vm.advanceSearch.subBranchesSelected = [];
            vm.advanceSearch.branchesSelected = [];
            vm.advanceSearch.batchesSelected = [];
            vm.advanceSearch.adminAuthoritiesSelected = [];
            vm.advanceSearch.areasSelected = [];
            vm.advanceSearch.areaTypesSelected = [];
            vm.advanceSearch.transferAreasSelected = [];
            vm.advanceSearch.missionCountriesSelected = [];
            vm.advanceSearch.resultsSelected = [];
            vm.advanceSearch.officesSelected = [];
            vm.advanceSearch.notServedOfficesSelected = [];
            vm.advanceSearch.servingOfficesSelected = [];
            vm.advanceSearch.appointmentsSelected = [];
            vm.advanceSearch.notServedAppointmentsSelected = [];
            vm.advanceSearch.servingAppointmentsSelected = [];
            vm.advanceSearch.punishmentCategoriesSelected = [];
            vm.advanceSearch.punishmentSubCategoriesSelected = [];


            vm.advanceSearch.publicationCategoriesSelected = [];
            vm.advanceSearch.publicationsSelected = [];
            vm.advanceSearch.medalsSelected = [];
            vm.advanceSearch.awardsSelected = [];
            vm.advanceSearch.commendationsSelected = [];
            vm.advanceSearch.appreciationsSelected = [];
            vm.advanceSearch.leaveCountriesSelected = [];
            vm.advanceSearch.shipsSelected = [];
            vm.advanceSearch.clearancesSelected = [];
            vm.advanceSearch.doingCourseCountriesSelected = [];
            vm.advanceSearch.notDoneCourseCountriesSelected = [];
            vm.advanceSearch.appointmentCategoriesSelected = [];
            vm.advanceSearch.educationSubjectsSelected = [];
            vm.advanceSearch.servingOrgSelected = [];

            vm.missionShow = false;
            vm.transferShow = false;
            vm.subjectShow = false;
            vm.seaServiceShow = false;
            vm.selfExamShow = false;
            vm.returnAbroadShow = false;
            vm.retirementDateShow = false;
            vm.religionShow = false;
            vm.rankShow = false;
            vm.promotionStatusShow = false;
            vm.physicalStructureShow = false;
            vm.passportShow = false;
            vm.oprShow = false;
            vm.officerNameShow = false;
            vm.officerServiceShow = false;
            vm.officerCategoryShow = false;
            vm.medicalCategoryShow = false;
            vm.areaShow = false;
            vm.adminShow = false;
            vm.ageShow = false;
            vm.batchShow = false;
            vm.branchShow = false;
            vm.subBranchShow = false;
            vm.bloodGroupShow = false;
            vm.civilEducationShow = false;
            vm.commSvcShow = false;
            vm.commissionTypeShow = false;
            vm.courseShow = false;
            vm.currentStatusShow = false;
            vm.districtShow = false;
            vm.drivingShow = false;
            vm.foreignVisitShow = false;
            vm.freedomFighterShow = false;
            vm.genderShow = false;
            vm.heightShow = false;
            vm.joiningDateShow = false;
            vm.lprDateShow = false;
            vm.maritalStatusShow = false;
            vm.buttonShow = true;
            vm.searchPanel = false;
            vm.punishmentShow = false;
            vm.medalShow = false;
            vm.awardShow = false;
            vm.publicationShow = false;
            vm.commendationShow = false;
            vm.appreciationShow = false;
            vm.exBdLeaveShow = false;
            vm.clearanceShow = false;
            vm.issbShow = false;
            vm.shipShow = false;
            vm.serviceExtensionShow = false;
            vm.rlDueShow = false;
            vm.statusChangeShow = false;
          
            vm.publications = [];
            vm.punishmentSubCategories = [];
            vm.officeAppointments = [];
            vm.notServedOfficeAppointments = [];
            vm.servingOfficeAppointments = [];
            vm.doingCourseSubCategories = [];
            vm.doingCourses = [];
            vm.notDoneCourseSubCategories = [];
            vm.notDoneCourses = [];
            vm.doneCourseSubCategories = [];
            vm.doneCourses = [];
            vm.visitSubCategories = [];

            vm.doneCoursesByCategory = [];
            vm.doingCoursesByCategory = [];
            vm.notDoneCoursesByCategory = [];


            vm.officeAppointmentsByShip = [];
            vm.notServedOfficeAppointmentsByShip = [];
            vm.servingOfficeAppointmentsByShip = [];

        }


        function getColumnDisplayValue(value, checked) {
            advanceSearchService.saveCheckedValue(checked, value).then(function (data) {
                  
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

        }
        function columnDisplayAction() {
          
            vm.columnDisplayList =[];
           
            advanceSearchService.deleteCheckedColumn().then(function (data) {
                vm.officerList = false;
            });
        }


        function refreshButton() {
            columnFilterAction();
            columnDisplayAction();
            Init();


        }

        function searchOfficers() {

            advanceSearchService.searchOfficers(vm.advanceSearch).then(function (data) {
                $state.goNewTab('advance-search-result');

            });
  
        }




        function getOfficeSelectModelByShip(ship) {
//            vm.advanceSearch.officesSelected = [];
          
            officeService.getOfficeSelectModelByShip(ship).then(function (data) {


                vm.offices = data.result;

            });
            officeAppointmentService.getAppointmentByShipType(ship).then(function (data) {


                vm.officeAppointmentsByShip = data.result;

            });

        }



        function getNotServedOfficeSelectModelByShip(ship) {
//            vm.advanceSearch.notServedOfficesSelected = [];
           
            officeService.getOfficeSelectModelByShip(ship).then(function (data) {


                vm.notServedOffices = data.result;

            });

            officeAppointmentService.getAppointmentByShipType(ship).then(function (data) {


                vm.notServedOfficeAppointmentsByShip = data.result;

            });

        }


        function getServingOfficeSelectModelByShip(ship) {
//            vm.advanceSearch.servingOfficesSelected = [];
            officeService.getOfficeSelectModelByShip(ship).then(function (data) {
                vm.servingOffices = data.result;

            });

            officeAppointmentService.getAppointmentByShipType(ship).then(function (data) {


                vm.servingOfficeAppointmentsByShip = data.result;

            });

        }


        function getServedChildOfficeSelectModel(patternId) {
            officeService.getChildOfficeSelectModels(patternId).then(function (data) {
                vm.offices = data.result;
            });
        }

        function getServingChildOfficeSelectModel(item) {
            if (vm.servingOffices.length > 600) {
                vm.servingOffices = [];
            }
            officeService.getChildOfficeSelectModels(item.value).then(function (data) {

                data.result.forEach(function (office) {
                    vm.servingOffices.push(office);
                });


            }); 

        
        }

         function getServingChildOfficeSelectModelDeSelect(item) {
             officeService.getChildOfficeSelectModels(item.value).then(function (data) {

                 data.result.forEach(function (office) {
                     vm.servingOffices.pop(office);
                 });


             }); 
        }


        function getNotServedChildOfficeSelectModel(patternId) {
            officeService.getChildOfficeSelectModels(patternId).then(function (data) {
                
                vm.notServedOffices = data.result;

            });
        }


        function attachOfficeSelect(item) {

            officeAppointmentService.getOfficeAppointmentByOffice(item.value).then(function (data) {

                data.result.forEach(function (office) {
                    vm.officeAppointments.push(office);
                });
                

            }); 

        }
        function attachOfficeDeSelect(item) {

            officeAppointmentService.getOfficeAppointmentByOffice(item.value).then(function (data) {

                data.result.forEach(function (office) {
                    vm.officeAppointments.pop(office);
                });


            }); 

        }
        function attachOfficeSelectAll() {

            if (vm.officeAppointmentsByShip.length == 0) {
                vm.officeAppointments = vm.officeAppointmentsCopy;

            }
            else {
                vm.officeAppointments = vm.officeAppointmentsByShip;
            }


        }


        function notServedAttachOfficeSelect(item) {

            officeAppointmentService.getOfficeAppointmentByOffice(item.value).then(function (data) {

                data.result.forEach(function (office) {
                    vm.notServedOfficeAppointments.push(office);
                });


            });

        }

        function notServedAttachOfficeDeSelect(item) {

            officeAppointmentService.getOfficeAppointmentByOffice(item.value).then(function (data) {

                data.result.forEach(function (office) {
                    vm.notServedOfficeAppointments.pop(office);
                });


            });

        }

        function notServedAttachOfficeSelectAll() {
         
            if (vm.notServedOfficeAppointmentsByShip.length == 0) {
                vm.notServedOfficeAppointments = vm.notServedOfficeAppointmentsCopy;

            }
            else {
                vm.notServedOfficeAppointments = vm.notServedOfficeAppointmentsByShip;
            }

        }


        function servingAttachOfficeSelect(item) {
            officeAppointmentService.getOfficeAppointmentByOffice(item.value).then(function (data) {

                data.result.forEach(function (office) {
                    vm.servingOfficeAppointments.push(office);
                });


            });

        }
        function servingAttachOfficeDeSelect(item) {

            officeAppointmentService.getOfficeAppointmentByOffice(item.value).then(function (data) {

                data.result.forEach(function (office) {
                    vm.servingOfficeAppointments.pop(office);
                });


            });

        }


        function servingAttachOfficeSelectAll() {
           

            if (vm.servingOfficeAppointmentsByShip.length == 0) {
                vm.servingOfficeAppointments = vm.servingOfficeAppointmentsCopy;

            }
            else {
                vm.servingOfficeAppointments = vm.servingOfficeAppointmentsByShip;
            }


        }


        function doneCourseCategorySelect(item) {

            
            courseService.getCourseSubCategories(item.value).then(function (data) {

                data.result.courseSubCategories.forEach(function (office) {
                    vm.doneCourseSubCategories.push(office);
                });


            });

            courseService.getCourseByCategory(item.value).then(function (data) {

                data.result.forEach(function (office) {
                    vm.doneCoursesByCategory.push(office);
                });


            });


        }

        function doneCourseCategoryDeSelect(item) {

            courseService.getCourseSubCategories(item.value).then(function (data) {

                data.result.courseSubCategories.forEach(function (office) {
                    vm.doneCourseSubCategories.pop(office);
                });

                courseService.getCourseByCategory(item.value).then(function (data) {

                    data.result.forEach(function (office) {
                        vm.doneCoursesByCategory.pop(office);
                    });


                });
            });

        }


        function doneCourseCategorySelectAll() {

            vm.doneCourseSubCategories = vm.doneCourseSubCategoriesCopy;


        }

        function notDoneCourseCategorySelect(item) {

           
            courseService.getCourseSubCategories(item.value).then(function (data) {

                data.result.courseSubCategories.forEach(function (office) {
                    vm.notDoneCourseSubCategories.push(office);
                });


            });

            courseService.getCourseByCategory(item.value).then(function (data) {

                data.result.forEach(function (office) {
                    vm.notDoneCoursesByCategory.push(office);
                });


            });

        }
        function notDoneCourseCategoryDeSelect(item) {

            courseService.getCourseSubCategories(item.value).then(function (data) {

                data.result.courseSubCategories.forEach(function (office) {
                    vm.notDoneCourseSubCategories.pop(office);
                });


            });

        }


        function notDoneCourseCategorySelectAll() {

            vm.notDoneCourseSubCategories = vm.notDoneCourseSubCategoriesCopy;


        }


        function doingCourseCategorySelect(item) {


            courseService.getCourseSubCategories(item.value).then(function (data) {

                data.result.courseSubCategories.forEach(function (office) {
                    vm.doingCourseSubCategories.push(office);
                });


            });

            courseService.getCourseByCategory(item.value).then(function (data) {

                data.result.forEach(function (office) {
                    vm.doingCoursesByCategory.push(office);
                });


            });

        }
        function doingCourseCategoryDeSelect(item) {

            courseService.getCourseSubCategories(item.value).then(function (data) {

                data.result.courseSubCategories.forEach(function (office) {
                    vm.doingCourseSubCategories.pop(office);
                });


            });

        }


        function doingCourseCategorySelectAll() {

            vm.doingCourseSubCategories = vm.doingCourseSubCategoriesCopy;


        }



        function doneCourseSubCategorySelect(item) {


            courseService.getCourseBySubCategory(item.value).then(function (data) {

                data.result.forEach(function (office) {
                    vm.doneCourses.push(office);
                });


            });

        }
        function doneCourseSubCategoryDeSelect(item) {

            courseService.getCourseBySubCategory(item.value).then(function (data) {

                data.result.forEach(function (office) {
                    vm.doneCourses.pop(office);
                });


            });

        }


        function doneCourseSubCategorySelectAll() {

            if (vm.doneCoursesByCategory.length == 0) {
                vm.doneCourses = vm.doneCoursesCopy;
            }
            else {
                vm.doneCourses = vm.doneCoursesByCategory;
            }
           


        }


        function notDoneCourseSubCategorySelect(item) {


            courseService.getCourseBySubCategory(item.value).then(function (data) {

                data.result.forEach(function (office) {
                    vm.notDoneCourses.push(office);
                });


            });

        }
        function notDoneCourseSubCategoryDeSelect(item) {

            courseService.getCourseBySubCategory(item.value).then(function (data) {

                data.result.forEach(function (office) {
                    vm.notDoneCourses.pop(office);
                });


            });

        }


        function notDoneCourseSubCategorySelectAll() {

            if (vm.notDoneCoursesByCategory.length == 0) {
                vm.notDoneCourses = vm.notDoneCoursesCopy;
            }
            else {
                vm.notDoneCourses = vm.notDoneCoursesByCategory;
            }
            


        }




        function doingCourseSubCategorySelect(item) {

            courseService.getCourseBySubCategory(item.value).then(function (data) {

                data.result.forEach(function (office) {
                    vm.doingCourses.push(office);
                });


            });

        }
        function doingCourseSubCategoryDeSelect(item) {

            courseService.getCourseBySubCategory(item.value).then(function (data) {

                data.result.forEach(function (office) {
                    vm.doingCourses.pop(office);
                });


            });

        }


        function doingCourseSubCategorySelectAll() {

          
            if (vm.doingCoursesByCategory.length == 0) {
                vm.doingCourses = vm.doingCoursesCopy;
            }
            else {
                vm.doingCourses = vm.doingCoursesByCategory;
            }

        }

        function punishmentCategorySelect(item) {

            punishmentSubCategoryService.getPunishmentSubCategorySelectModelsByPunishmentCategory(item.value).then(function (data) {

                data.result.forEach(function (itemData) {
                    vm.punishmentSubCategories.push(itemData);
                });


            });

        }
        function punishmentCategoryDeSelect(item) {

            punishmentSubCategoryService.getPunishmentSubCategorySelectModelsByPunishmentCategory(item.value).then(function (data) {

                data.result.forEach(function (itemData) {
                    vm.punishmentSubCategories.pop(itemData);
                });


            });

        }


        function punishmentCategorySelectAll() {

            vm.punishmentSubCategories = vm.punishmentSubCategoriesCopy;


        }



        function publicationCategorySelect(item) {

            medalAwardService.getPublicationsByCategory(item.value).then(function (data) {

                data.result.publications.forEach(function (itemData) {
                    vm.publications.push(itemData);
                });


            });

        }

        function publicationCategoryDeSelect(item) {

            medalAwardService.getPublicationsByCategory(item.value).then(function (data) {

                data.result.publications.forEach(function (itemData) {
                    vm.publications.pop(itemData);
                });


            });

        }


        function publicationCategorySelectAll() {

            vm.publications = vm.publicationsCopy;


        }


        function visitCategorySelect(item) {

            visitSubCategoryService.getVisitSubCategorySelectModelsByVisitCategory(item.value).then(function (data) {

                data.result.forEach(function (itemData) {
                    vm.visitSubCategories.push(itemData);
                });


            });

        }

        function visitCategoryDeSelect(item) {

            visitSubCategoryService.getVisitSubCategorySelectModelsByVisitCategory(item.value).then(function (data) {

                data.result.forEach(function (itemData) {
                    vm.visitSubCategories.pop(itemData);
                });


            });

        }


        function visitCategorySelectAll() {

            vm.visitSubCategories = vm.visitSubCategoriesCopy;


        }


    }
})();
