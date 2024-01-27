(function () {
    'use strict';
    angular.module('app').service('genderService', ['dataConstants', 'apiHttpService', genderService]);

    function genderService(dataConstants, apiHttpService) {
        var service = {
            getGenders: getGenders,
            getGender: getGender,
            getOfficerGenderSelectModels: getOfficerGenderSelectModels,
            saveGender: saveGender,
            updateGender: updateGender,
            deleteGender: deleteGender
        };

        return service;
        function getGenders(pageSize, pageNumber,searchText) {
            var url = dataConstants.GENDER_URL + 'get-genders?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getGender(genderId) {
            var url = dataConstants.GENDER_URL + 'get-gender?genderId=' + genderId;
            return apiHttpService.GET(url);
        }

        function getOfficerGenderSelectModels() {
            var url = dataConstants.GENDER_URL + 'get-gender-select-models';
            return apiHttpService.GET(url);
        }

        function saveGender(data) {
            var url = dataConstants.GENDER_URL + 'save-gender';
            return apiHttpService.POST(url, data);
        }

        function updateGender(genderId, data) {
            var url = dataConstants.GENDER_URL + 'update-gender/' + genderId;
            return apiHttpService.PUT(url, data);
        }

        function deleteGender(genderId) {
            var url = dataConstants.GENDER_URL + 'delete-gender/' + genderId;
            return apiHttpService.DELETE(url);
        }


    }
})();