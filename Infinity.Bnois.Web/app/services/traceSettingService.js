(function () {
    'use strict';
    angular.module('app').service('traceSettingService', ['dataConstants', 'apiHttpService', traceSettingService]);

    function traceSettingService(dataConstants, apiHttpService) {
        var service = {
            getTraceSettings: getTraceSettings,
            getTraceSetting: getTraceSetting,
            saveTraceSetting: saveTraceSetting,
            updateTraceSetting: updateTraceSetting,
            deleteTraceSetting: deleteTraceSetting,

            //-------------------Course Point Service------------------
            getCoursePoints: getCoursePoints,
            getCoursePoint: getCoursePoint,
            saveCoursePoint: saveCoursePoint,
            updateCoursePoint: updateCoursePoint,
            deleteCoursePoint: deleteCoursePoint,

            //-----------------Poor Course Result--------------
            getPoorCourseResults: getPoorCourseResults,
            getPoorCourseResult: getPoorCourseResult,
            savePoorCourseResult: savePoorCourseResult,
            updatePoorCourseResult: updatePoorCourseResult,
            deletePoorCourseResult: deletePoorCourseResult,

            //-----------------Branch & Country  Wise Course Point--------------
            getBraCtryCoursePoints: getBraCtryCoursePoints,
            getBraCtryCoursePoint: getBraCtryCoursePoint,
            saveBraCtryCoursePoint: saveBraCtryCoursePoint,
            updateBraCtryCoursePoint: updateBraCtryCoursePoint,
            deleteBraCtryCoursePoint: deleteBraCtryCoursePoint,

            //---------------------Point Deduction for Punishments----------
            getPtDeductPunishments: getPtDeductPunishments,
            getPtDeductPunishment: getPtDeductPunishment,
            savePtDeductPunishment: savePtDeductPunishment,
            updatePtDeductPunishment: updatePtDeductPunishment,
            deletePtDeductPunishment: deletePtDeductPunishment,


            //---------------------Bonus Point for Medal----------
            getBonusPtMedals: getBonusPtMedals,
            getBonusPtMedal: getBonusPtMedal,
            saveBonusPtMedal: saveBonusPtMedal,
            updateBonusPtMedal: updateBonusPtMedal,
            deleteBonusPtMedal: deleteBonusPtMedal,

            //---------------------Bonus Point for Award----------
            getBonusPtAwards: getBonusPtAwards,
            getBonusPtAward: getBonusPtAward,
            saveBonusPtAward: saveBonusPtAward,
            updateBonusPtAward: updateBonusPtAward,
            deleteBonusPtAward: deleteBonusPtAward,


            //---------------------Bonus Point for Publication----------
            getBonusPtPublics: getBonusPtPublics,
            getBonusPtPublic: getBonusPtPublic,
            saveBonusPtPublic: saveBonusPtPublic,
            updateBonusPtPublic: updateBonusPtPublic,
            deleteBonusPtPublic: deleteBonusPtPublic,

            //---------------------Bonus Point for Commendation and Appreciation----------
            getBonusPtComApps: getBonusPtComApps,
            getBonusPtComApp: getBonusPtComApp,
            saveBonusPtComApp: saveBonusPtComApp,
            updateBonusPtComApp: updateBonusPtComApp,
            deleteBonusPtComApp: deleteBonusPtComApp,

        };

        return service;
        function getTraceSettings(pageSize, pageNumber, searchText) {
            var url = dataConstants.TRACE_SETTING_URL + 'get-trace-settings?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getTraceSetting(id) {
            var url = dataConstants.TRACE_SETTING_URL + 'get-trace-setting?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveTraceSetting(data) {
            var url = dataConstants.TRACE_SETTING_URL + 'save-trace-setting';
            return apiHttpService.POST(url, data);
        }

        function updateTraceSetting(id, data) {
            var url = dataConstants.TRACE_SETTING_URL + 'update-trace-setting/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteTraceSetting(id) {
            var url = dataConstants.TRACE_SETTING_URL + 'delete-trace-setting/' + id;
            return apiHttpService.DELETE(url);
        }
        //-------------------Course Point Service------------------

        function getCoursePoints(id) {
            var url = dataConstants.COURSE_POINT_URL + 'get-course-points?id=' + id;
            return apiHttpService.GET(url);
        }

        function getCoursePoint(traceSettingId, coursePointId) {
            var url = dataConstants.COURSE_POINT_URL + 'get-course-point?traceSettingId=' + traceSettingId + '&coursePointId=' + coursePointId;
            return apiHttpService.GET(url);
        }

        function saveCoursePoint(traceSettingId, data) {
            var url = dataConstants.COURSE_POINT_URL + 'save-course-point/' + traceSettingId;
            return apiHttpService.POST(url, data);
        }

        function updateCoursePoint(id, data) {
            var url = dataConstants.COURSE_POINT_URL + 'update-course-point/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteCoursePoint(id) {
            var url = dataConstants.COURSE_POINT_URL + 'delete-course-point/' + id;
            return apiHttpService.DELETE(url);
        }
        //-------------------Poor Course Result Service----------------------------------

        function getPoorCourseResults(id) {
            var url = dataConstants.POOR_COURSE_RESULT + 'get-poor-course-results?id=' + id;
            return apiHttpService.GET(url);
        }

        function getPoorCourseResult(traceSettingId, poorCourseResultId) {
            var url = dataConstants.POOR_COURSE_RESULT + 'get-poor-course-result?traceSettingId=' + traceSettingId + '&poorCourseResultId=' + poorCourseResultId;
            return apiHttpService.GET(url);
        }

        function savePoorCourseResult(traceSettingId, data) {
            var url = dataConstants.POOR_COURSE_RESULT + 'save-poor-course-result/' + traceSettingId;
            return apiHttpService.POST(url, data);
        }

        function updatePoorCourseResult(id, data) {
            var url = dataConstants.POOR_COURSE_RESULT + 'update-poor-course-result/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deletePoorCourseResult(id) {
            var url = dataConstants.POOR_COURSE_RESULT + 'delete-poor-course-result/' + id;
            return apiHttpService.DELETE(url);
        }

        //-----------------Branch & Country  Wise Course Point--------------
        function getBraCtryCoursePoints(id) {
            var url = dataConstants.BRANCH_COUNTRY_WISE_COURSE_POINT + 'get-bra-ctry-course-points?id=' + id;
            return apiHttpService.GET(url);
        }

        function getBraCtryCoursePoint(traceSettingId, braCtryCoursePointId) {
            var url = dataConstants.BRANCH_COUNTRY_WISE_COURSE_POINT + 'get-bra-ctry-course-point?traceSettingId=' + traceSettingId + '&braCtryCoursePointId=' + braCtryCoursePointId;
            return apiHttpService.GET(url);
        }

        function saveBraCtryCoursePoint(traceSettingId, data) {
            var url = dataConstants.BRANCH_COUNTRY_WISE_COURSE_POINT + 'save-bra-ctry-course-point/' + traceSettingId;
            return apiHttpService.POST(url, data);
        }

        function updateBraCtryCoursePoint(id, data) {
            var url = dataConstants.BRANCH_COUNTRY_WISE_COURSE_POINT + 'update-bra-ctry-course-point/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteBraCtryCoursePoint(id) {
            var url = dataConstants.BRANCH_COUNTRY_WISE_COURSE_POINT + 'delete-bra-ctry-course-point/' + id;
            return apiHttpService.DELETE(url);
        }
        //-------------------Point Deduction for Punishments------------------

        function getPtDeductPunishments(id) {
            var url = dataConstants.POINT_DEDUCTION_PUNISHMENT_URL + 'get-point-deduction-for-punishments?id=' + id;
            return apiHttpService.GET(url);
        }

        function getPtDeductPunishment(traceSettingId, ptDeductPunishmentId) {
            var url = dataConstants.POINT_DEDUCTION_PUNISHMENT_URL + 'get-point-deduction-for-punishment?traceSettingId=' + traceSettingId + '&ptDeductPunishmentId=' + ptDeductPunishmentId;
            return apiHttpService.GET(url);
        }

        function savePtDeductPunishment(traceSettingId, data) {
            var url = dataConstants.POINT_DEDUCTION_PUNISHMENT_URL + 'save-point-deduction-for-punishment/' + traceSettingId;
            return apiHttpService.POST(url, data);
        }

        function updatePtDeductPunishment(id, data) {
            var url = dataConstants.POINT_DEDUCTION_PUNISHMENT_URL + 'update-point-deduction-for-punishment/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deletePtDeductPunishment(id) {
            var url = dataConstants.POINT_DEDUCTION_PUNISHMENT_URL + 'delete-point-deduction-for-punishment/' + id;
            return apiHttpService.DELETE(url);
        }

        //---------------------Bonus Point for Medal--------------------------------
        function getBonusPtMedals(id) {
            var url = dataConstants.BONUS_POINT_MEDAL_URL + 'get-bonus-point-medals?id=' + id;
            return apiHttpService.GET(url);
        }

        function getBonusPtMedal(traceSettingId, bonusPtMedalId) {
            var url = dataConstants.BONUS_POINT_MEDAL_URL + 'get-bonus-point-medal?traceSettingId=' + traceSettingId + '&bonusPtMedalId=' + bonusPtMedalId;
            return apiHttpService.GET(url);
        }

        function saveBonusPtMedal(traceSettingId, data) {
            var url = dataConstants.BONUS_POINT_MEDAL_URL + 'save-bonus-point-medal/' + traceSettingId;
            return apiHttpService.POST(url, data);
        }

        function updateBonusPtMedal(id, data) {
            var url = dataConstants.BONUS_POINT_MEDAL_URL + 'update-bonus-point-medal/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteBonusPtMedal(id) {
            var url = dataConstants.BONUS_POINT_MEDAL_URL + 'delete-bonus-point-medal/' + id;
            return apiHttpService.DELETE(url);
        }

        //---------------------Bonus Point for Award--------------------------------
        function getBonusPtAwards(id) {
            var url = dataConstants.BONUS_POINT_AWARD_URL + 'get-bonus-point-awards?id=' + id;
            return apiHttpService.GET(url);
        }

        function getBonusPtAward(traceSettingId, bonusPtAwardId) {
            var url = dataConstants.BONUS_POINT_AWARD_URL + 'get-bonus-point-award?traceSettingId=' + traceSettingId + '&bonusPtAwardId=' + bonusPtAwardId;
            return apiHttpService.GET(url);
        }

        function saveBonusPtAward(traceSettingId, data) {
            var url = dataConstants.BONUS_POINT_AWARD_URL + 'save-bonus-point-award/' + traceSettingId;
            return apiHttpService.POST(url, data);
        }

        function updateBonusPtAward(id, data) {
            var url = dataConstants.BONUS_POINT_AWARD_URL + 'update-bonus-point-award/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteBonusPtAward(id) {
            var url = dataConstants.BONUS_POINT_AWARD_URL + 'delete-bonus-point-award/' + id;
            return apiHttpService.DELETE(url);
        }

        //---------------------Bonus Point for Publication--------------------------------
        function getBonusPtPublics(id) {
            var url = dataConstants.BONUS_POINT_PUBLICATION_URL + 'get-bonus-point-publics?id=' + id;
            return apiHttpService.GET(url);
        }

        function getBonusPtPublic(traceSettingId, bonusPtPublicId) {
            var url = dataConstants.BONUS_POINT_PUBLICATION_URL + 'get-bonus-point-public?traceSettingId=' + traceSettingId + '&bonusPtPublicId=' + bonusPtPublicId;
            return apiHttpService.GET(url);
        }

        function saveBonusPtPublic(traceSettingId, data) {
            var url = dataConstants.BONUS_POINT_PUBLICATION_URL + 'save-bonus-point-public/' + traceSettingId;
            return apiHttpService.POST(url, data);
        }

        function updateBonusPtPublic(id, data) {
            var url = dataConstants.BONUS_POINT_PUBLICATION_URL + 'update-bonus-point-public/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteBonusPtPublic(id) {
            var url = dataConstants.BONUS_POINT_PUBLICATION_URL + 'delete-bonus-point-public/' + id;
            return apiHttpService.DELETE(url);
        }

        //---------------------Bonus Point for Publication--------------------------------
        function getBonusPtComApps(id) {
            var url = dataConstants.BONUS_POINT_COMMENDATION_APPRECIATION_URL + 'get-bonus-point-com-apps?id=' + id;
            return apiHttpService.GET(url);
        }

        function getBonusPtComApp(traceSettingId, bonusPtComAppId) {
            var url = dataConstants.BONUS_POINT_COMMENDATION_APPRECIATION_URL + 'get-bonus-point-com-app?traceSettingId=' + traceSettingId + '&bonusPtComAppId=' + bonusPtComAppId;
            return apiHttpService.GET(url);
        }

        function saveBonusPtComApp(traceSettingId, data) {
            var url = dataConstants.BONUS_POINT_COMMENDATION_APPRECIATION_URL + 'save-bonus-point-com-app/' + traceSettingId;
            return apiHttpService.POST(url, data);
        }

        function updateBonusPtComApp(id, data) {
            var url = dataConstants.BONUS_POINT_COMMENDATION_APPRECIATION_URL + 'update-bonus-point-com-app/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteBonusPtComApp(id) {
            var url = dataConstants.BONUS_POINT_COMMENDATION_APPRECIATION_URL + 'delete-bonus-point-com-app/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();