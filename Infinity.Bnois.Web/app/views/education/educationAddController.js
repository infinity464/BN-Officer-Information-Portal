(function () {

    'use strict';
    var controllerId = 'educationAddController';
    angular.module('app').controller(controllerId, educationAddController);
    educationAddController.$inject = ['$stateParams', '$state', 'educationService', 'notificationService'];

    function educationAddController($stateParams, $state, educationService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.educationId = 0;
        vm.education = {};
        vm.examCategory = {};
        vm.examCategories = [];
        vm.examinations = [];
        vm.boards = [];
        vm.institutes = [];
        vm.years = [];
        vm.courseDurations = [];
        vm.subjects = [];
        vm.results = [];
        vm.grades = [];
        vm.title = 'ADD MODE';
        vm.getSubjectsByExamination = getSubjectsByExamination;
        vm.getExaminationByExamCategory = getExaminationByExamCategory;
        vm.getInstituteByBoardOrUniversity = getInstituteByBoardOrUniversity

        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.educationForm = {};

        vm.isShowGPA = true;
        vm.showGPA = showGPA;
        function showGPA(resultId) {
           // var gpaCodes = bpscDataConstants.gpaCodes;
            var gpaResult = vm.results.find(m => m.value == resultId);
            if (gpaResult.value == 28 || gpaResult.value == 29) {
                vm.isShowGPA = true;
            } else {
                vm.isShowGPA = false;
                vm.education.gpa = 0;
            }
        }
        

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
        }
        if ($stateParams.educationId > 0) {
            vm.educationId = $stateParams.educationId;
            vm.title = 'UPDATE MODE';
        }
        init();
        function init() {
            educationService.getEducation(vm.employeeId, vm.educationId).then(function (data) {
                vm.education = data.result.education;

                vm.examCategories = data.result.examCategories;
                vm.examinations = data.result.examinations;
                vm.boards = data.result.boards;
                vm.institutes = data.result.institutes;
                vm.results = data.result.results;
                vm.subjects = data.result.subjects;
                vm.years = data.result.years;
                vm.courseDurations = data.result.courseDurations;
                vm.grades = data.result.grades;
                vm.isShowDuration = true;
                if (vm.education.resultId != null) {
                    showGPA(vm.education.resultId);
                }
                
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function getExaminationByExamCategory(examCategoryId) {
            if (examCategoryId !== undefined) {
                educationService.getExaminationsByExamCategory(examCategoryId).then(function (data) {
                    vm.examinations = data.result.examinations;
                    vm.boards = data.result.boards;
                    vm.results = data.result.results;
                    vm.subjects = data.result.subjects;
                    vm.examCategory = data.result.examCategory;
                },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
            }
            else {
                vm.examinations = [];
                vm.boards = [];
                vm.results = [];
                vm.subjects = [];
                vm.examCategory = {};
            }
        }



        function getSubjectsByExamination(examinationId) {
            if (examinationId > 0 && examinationId != 'undefined') {
                educationService.getSubjectsByExamination(examinationId).then(function (data) {
                    vm.subjects = data.result;
                },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
            }
        }



        function save() {
            if (vm.educationId > 0 && vm.educationId !== '') {
                updateEducation();
            } else {
                insertEducation();
            }
        }
        function insertEducation() {
            educationService.saveEducation(vm.employeeId, vm.education).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateEducation() {
            educationService.updateEducation(vm.educationId, vm.education).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function getInstituteByBoardOrUniversity(boardId) {
            educationService.getInstituteByBoardOrUniversity(boardId).then(function (data) {
                vm.institutes = data.result;
                },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
            
        }
        function close() {
            $state.go('employee-tabs.employee-educations');
        }
    }

})();
