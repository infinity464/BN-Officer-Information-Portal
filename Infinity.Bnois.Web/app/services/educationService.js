(function () {
    'use strict';
    angular.module('app').service('educationService', ['dataConstants', 'apiHttpService', educationService]);

    function educationService(dataConstants, apiHttpService) {
        var service = {
            getEducations: getEducations,
            getEducation: getEducation,
            saveEducation: saveEducation,
            updateEducation: updateEducation,
            deleteEducation: deleteEducation,
            getExaminationsByExamCategory: getExaminationsByExamCategory,
            getSubjectsByExamination: getSubjectsByExamination,
            getInstituteByBoardOrUniversity: getInstituteByBoardOrUniversity,
            imageUploadUrl: imageUploadUrl
        };

        return service;

        

        function imageUploadUrl(employeeId,educationId) {
            var url = dataConstants.EDUCATION_URL + 'upload-education-certificate?employeeId=' + employeeId + '&educationId=' + educationId;;
            return url;
        }
        function getEducations(employeeId) {
            var url = dataConstants.EDUCATION_URL + 'get-educations?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function getEducation(employeeId, educationId) {
            var url = dataConstants.EDUCATION_URL + 'get-education?employeeId=' + employeeId + '&educationId=' + educationId;
            return apiHttpService.GET(url);
        }

        function saveEducation(employeeId,data) {
            var url = dataConstants.EDUCATION_URL + 'save-education/' + employeeId;
            return apiHttpService.POST(url, data);
        }

        function updateEducation(educationId, data) {
            var url = dataConstants.EDUCATION_URL + 'update-education/' + educationId;
            return apiHttpService.PUT(url, data);
        }

        function getExaminationsByExamCategory(examCategoryId) {
            var url = dataConstants.EDUCATION_URL + 'get-examinations/' + examCategoryId;
            return apiHttpService.GET(url);
        }

        function getSubjectsByExamination(examinationId) {
            var url = dataConstants.EDUCATION_URL + 'get-subjects/' + examinationId;
            return apiHttpService.GET(url);
        }
        function getInstituteByBoardOrUniversity(boardId) {
            var url = dataConstants.EDUCATION_URL + 'get-institutes/' + boardId;
            return apiHttpService.GET(url);
        }
        function deleteEducation(id) {
            var url = dataConstants.EDUCATION_URL + 'delete-education/' + id;
            return apiHttpService.DELETE(url);
        }

        
    }
})();