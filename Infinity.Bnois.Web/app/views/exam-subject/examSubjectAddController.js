(function () {
    'use strict';
    var controllerId = 'examSubjectAddController';
    angular.module('app').controller(controllerId, examSubjectAddController);
    examSubjectAddController.$inject = ['$stateParams', '$scope', 'dataConstants', 'examSubjectService', 'notificationService', '$state'];

    function examSubjectAddController($stateParams, $scope, dataConstants, examSubjectService, notificationService, $state) {
        var vm = this;
        vm.examSubjectId = 0;
        vm.examSubject = {};
        vm.examCategories = [];
        vm.examCategoryId = 0;
        vm.title = 'ADD MODE';
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.examSubjectForm = {};
        vm.examinations = [];
        vm.selected = {};
        vm.getExaminationByExamCategory = getExaminationByExamCategory;
        vm.remoteUrl = dataConstants.SUBJECT_URL + 'filter-subjects?searchStr=';

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.examSubjectId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            examSubjectService.getExamSubject(vm.examSubjectId).then(function (data) {
                vm.examSubject = data.result.examSubject;
                vm.examCategories = data.result.examCategories;
                vm.examinations = data.result.examinations;
                changeInputValue();
                setExamCategoryId();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            vm.examSubject.subjectId = vm.selected.originalObject.value;
            if (vm.examSubjectId > 0) {
                updateExamSubject();
            } else {
                insertExamSubject();
            }
        }

        function insertExamSubject() {
            examSubjectService.saveExamSubject(vm.examSubject).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateExamSubject() {
            examSubjectService.updateExamSubject(vm.examSubjectId, vm.examSubject).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function getExaminationByExamCategory(examCategoryId) {
           
            examSubjectService.getExaminationsByExamCategory(examCategoryId).then(function (data) {
                vm.examinations = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function close() {
          $state.go('exam-subjects');
        }

        function clearInputValue() {
            $scope.$broadcast('angucomplete-alt:clearInput');
        }
        function changeInputValue() {
            if (vm.examSubjectId > 0) {
             var obj = { value: vm.examSubject.subject.subjectId, text: vm.examSubject.subject.name  };
             $scope.$broadcast('angucomplete-alt:changeInput', 'subjectId', obj);
            }
        }
        function setExamCategoryId() {
            if (vm.examSubjectId > 0) {
                vm.examCategoryId = vm.examSubject.examination.examCategoryId;
            }
        }
    }
})();
