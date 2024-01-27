(function () {
    'use strict';
    angular.module('app').service('medicalCategoryService', ['dataConstants', 'apiHttpService', medicalCategoryService]);

    function medicalCategoryService(dataConstants, apiHttpService) {
        var service = {
            getMedicalCategories: getMedicalCategories,
            getMedicalCategory: getMedicalCategory,
            saveMedicalCategory: saveMedicalCategory,
            updateMedicalCategory: updateMedicalCategory,
            deleteMedicalCategory: deleteMedicalCategory
        };

        return service;
        function getMedicalCategories(pageSize, pageNumber, searchText) {
            var url = dataConstants.MEDICAL_CATEGORY_URL + 'get-medical-categories?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getMedicalCategory(medicalCategoryId) {
            var url = dataConstants.MEDICAL_CATEGORY_URL + 'get-medical-category?medicalCategoryId=' + medicalCategoryId;
            return apiHttpService.GET(url);
        }

        function saveMedicalCategory(data) {
            var url = dataConstants.MEDICAL_CATEGORY_URL + 'save-medical-category';
            return apiHttpService.POST(url, data);
        }

        function updateMedicalCategory(medicalCategoryId, data) {
            var url = dataConstants.MEDICAL_CATEGORY_URL + 'update-medical-category/' + medicalCategoryId;
            return apiHttpService.PUT(url, data);
        }

        function deleteMedicalCategory(medicalCategoryId) {
            var url = dataConstants.MEDICAL_CATEGORY_URL + 'delete-medical-category/' + medicalCategoryId;
            return apiHttpService.DELETE(url);
        }


    }
})();