(function () {
    'use strict';
    angular.module('app').service('courseCategoryService', ['dataConstants', 'apiHttpService', courseCategoryService]);

    function courseCategoryService(dataConstants, apiHttpService) {
        var service = {
            getCourseCategories: getCourseCategories,
            getCourseCategory: getCourseCategory,
            saveCourseCategory: saveCourseCategory,
            updateCourseCategory: updateCourseCategory,
            deleteCourseCategory: deleteCourseCategory
        };

        return service;
        function getCourseCategories(pageSize, pageNumber, searchText) {
            var url = dataConstants.COURSE_CATEGORY_URL + 'get-course-categories?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getCourseCategory(id) {
            var url = dataConstants.COURSE_CATEGORY_URL + 'get-course-category?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveCourseCategory(data) {
            var url = dataConstants.COURSE_CATEGORY_URL + 'save-course-category';
            return apiHttpService.POST(url, data);
        }

        function updateCourseCategory(id, data) {
            var url = dataConstants.COURSE_CATEGORY_URL + 'update-course-category/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteCourseCategory(id) {
            var url = dataConstants.COURSE_CATEGORY_URL + 'delete-course-category/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();