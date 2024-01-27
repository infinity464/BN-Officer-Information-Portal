(function () {
    'use strict';
    angular.module('app').service('courseSubCategoryService', ['dataConstants', 'apiHttpService', courseSubCategoryService]);

    function courseSubCategoryService(dataConstants, apiHttpService) {
        var service = {
            getCourseSubCategories: getCourseSubCategories,
            getCourseSubCategory: getCourseSubCategory,
            saveCourseSubCategory: saveCourseSubCategory,
            updateCourseSubCategory: updateCourseSubCategory,
            deleteCourseSubCategory: deleteCourseSubCategory
        };

        return service;
        function getCourseSubCategories(pageSize, pageNumber, searchText) {
            var url = dataConstants.COURSE_SUB_CATEGORY_URL + 'get-course-sub-categories?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getCourseSubCategory(id) {
            var url = dataConstants.COURSE_SUB_CATEGORY_URL + 'get-course-sub-category?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveCourseSubCategory(data) {
            var url = dataConstants.COURSE_SUB_CATEGORY_URL + 'save-course-sub-category';
            return apiHttpService.POST(url, data);
        }

        function updateCourseSubCategory(id, data) {
            var url = dataConstants.COURSE_SUB_CATEGORY_URL + 'update-course-sub-category/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteCourseSubCategory(id) {
            var url = dataConstants.COURSE_SUB_CATEGORY_URL + 'delete-course-sub-category/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();