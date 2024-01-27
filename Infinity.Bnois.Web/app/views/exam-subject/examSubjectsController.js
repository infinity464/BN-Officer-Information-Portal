(function () {
    'use strict';
    var controllerId = 'examSubjectsController';
    angular.module('app').controller(controllerId, examSubjectsController);
    examSubjectsController.$inject = ['$state', 'examSubjectService', 'notificationService', '$location'];

    function examSubjectsController($state, examSubjectService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.examSubjects = [];
        vm.addExamSubject = addExamSubject;
        vm.updateExamSubject = updateExamSubject;
        vm.deleteExamSubject = deleteExamSubject;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;

        if (location.search().ps !== undefined && location.search().ps != null && location.search().ps != '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn != null && location.search().pn != '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q != null && location.search().q != '') {
            vm.searchText = location.search().q;
        }
        init();
        function init() {
            examSubjectService.getExamSubjects(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.examSubjects = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addExamSubject() {
            $state.go('exam-subject-create');
        }

        function updateExamSubject(examSubject) {
            $state.go('exam-subject-modify', { id:examSubject.examSubjectId});
        }

        function deleteExamSubject(examSubject) {
            examSubjectService.deleteExamSubject(examSubject.examSubjectId).then(function (data) {
                init();
            });
        }

        function pageChanged() {
            $state.go('exam-subjects', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }

    }

})();
