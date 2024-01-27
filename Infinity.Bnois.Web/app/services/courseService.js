(function () {
    'use strict';
    angular.module('app').service('courseService', ['dataConstants', 'apiHttpService', courseService]);

    function courseService(dataConstants, apiHttpService) {
        var service = {
            getCourses: getCourses,
            getCourse: getCourse,
            getCourseSubCategories: getCourseSubCategories,
            getCourseBySubCategory: getCourseBySubCategory,
            getCourseByCategory: getCourseByCategory,
            saveCourse: saveCourse,
            updateCourse: updateCourse,
            deleteCourse: deleteCourse
        };

        return service;
        function getCourses(pageSize, pageNumber,searchText) {
            var url = dataConstants.COURSE_URL + 'get-courses?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getCourse(courseId) {
            var url = dataConstants.COURSE_URL + 'get-course?id=' + courseId;
            return apiHttpService.GET(url);
        }

        function getCourseSubCategories(categoryId) {
            var url = dataConstants.COURSE_URL + 'get-course-sub-categories-by-course?id=' + categoryId;
            return apiHttpService.GET(url);
        }

        function getCourseBySubCategory(courseSubCatId) {
            var url = dataConstants.COURSE_URL + 'get-course-by-sub-category?id=' + courseSubCatId;
            return apiHttpService.GET(url);
        }

         function getCourseByCategory(courseCatId) {
            var url = dataConstants.COURSE_URL + 'get-course-by-category?id=' + courseCatId;
            return apiHttpService.GET(url);
        }


        function saveCourse(data) {
            var url = dataConstants.COURSE_URL + 'save-course';
            return apiHttpService.POST(url, data);
        }

        function updateCourse(courseId, data) {
            var url = dataConstants.COURSE_URL + 'update-course/' + courseId;
            return apiHttpService.PUT(url, data);
        }

        function deleteCourse(courseId) {
            var url = dataConstants.COURSE_URL + 'delete-course/' + courseId;
            return apiHttpService.DELETE(url);
        }
        
    }
})();