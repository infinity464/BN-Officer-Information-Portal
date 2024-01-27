(function () {
    'use strict';
    var controllerId = 'LeftSideBarNavigationController';
    angular
        .module('app')
        .controller(controllerId, ['$scope', '$window', 'feedBackService', 'employeeService', 'OidcManager', 'notificationService', 'moduleService', '$sce', '$state', leftSideBarNavigation]);

    function leftSideBarNavigation($scope, $window, feedBackService, employeeService, OidcManager, notificationService, moduleService, $sce, $state) {
        var vm = this;
        vm.model = [];
        vm.isCurrent = isCurrent;
        vm.isChild = isChild;
        vm.menuChange = menuChange;
        vm.manager = OidcManager.OidcTokenManager();
        vm.userName = vm.manager.profile.preferred_username;
        vm.role = vm.manager.profile.role
        vm.homeButton = homeButton;
        vm.upArrow = true;
        vm.transfer = transfer;
        vm.course = course;
        vm.opr = opr;
        vm.award = award;
        vm.commendation = commendation;
        vm.leave = leave;
        vm.logout = logout;
        vm.password = password;
        vm.notificationUser = 0;
        vm.refresh = refresh;
        vm.print = print;
        vm.migration = migration;
        vm.empHistory = empHistory;
        vm.download = download;
        vm.feedBack = feedBack;
        vm.userCreate = userCreate;
        vm.user = user;
        vm.userLog = userLog;
        vm.flashNotice = flashNotice;
        vm.noticeBoard = noticeBoard;
        init();

        function init() {
            moduleService.getModuleFeaturs().then(function (data) {
                vm.model = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            feedBackService.getFeedbackForUserNotification().then(function (data) {
                vm.notificationUser = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
            angular.element(function () {
                if (window.location.pathname === '/current-status-transfer-history') {
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('transfer');
                    element.classList.add('active');
                } else if (window.location.pathname === '/current-status-course-attended') {
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('course');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/current-status-opr-grading') {
                    //$state.go('current-status-opr-grading');
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('opr');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/current-status-medal-award-publication') {
                    //$state.go('current-status-medal-award-publication');
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('award');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/current-status-commendation-appreciation') {
                    //$state.go('current-status-commendation-appreciation');
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('commendation');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/current-status-leave-information') {
                    //$state.go('current-status-leave-information');
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('leave');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/log-out') {
                    //$state.go('log-out');
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('logout');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/change-password') {
                    //$state.go('change-password');
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('password');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/print-section') {
                    //$state.go('print-section');
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('print');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/migration') {
                    //$state.go('migration');
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('migration');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/employee-history') {
                    //$state.go('employee-history');
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('empHistory');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/download-list') {
                    //$state.go('employee-history');
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('download');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/users') {
                    //$state.go('users');
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('user');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/users-create') {
                    //$state.go('users-create');
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('userCreate');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/feed-back') {
                    //$state.go('feed-back');
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('feedBack');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/employee-current-status') {
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('homeButton');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/admin-dashboard') {
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('homeButton');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/user-log') {
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('userLog');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/flash-notice') {
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('flashNotice');
                    element.classList.add('active');
                }
                else if (window.location.pathname === '/flash-notice-board') {
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('noticeBoard');
                    element.classList.add('active');
                }
                else {
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                }
            });
        }
        function homeButton() {
            employeeService.GetLoginEmployeePno().then(function (data) {
                vm.pno = data.result;
                if (vm.pno !== null) {
                    $state.go('employee-current-status', { pno: vm.pno });
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('homeButton');
                    element.classList.add('active');
                }
                else {
                    $state.go('admin-dashboard');
                    $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
                    var element = document.getElementById('homeButton');
                    element.classList.add('active');
                }

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        var a = 'something';

        function isCurrent(route) {
            if (!route.title || !$state.current || !$state.current.title) {
                return '';
            }

            var menu = route.title;
            return $state.current.title.substr(0, menu.length) === menu ? 'active' : '';
        }

        function isChild(route) {
            if (route.isChild) {
                return 'child';
            }

            return '';
        }

        vm.toggole = function () {
            alert('hi');
        };


        function menuChange() {
            $('.user').parent().parent().parent().toggleClass("collaspe");
            $('.navbar-header').toggleClass("collL");
            $('.col-md-offset-2').toggleClass("bodyLeft");

        }

        // Sidebar height
        var screen_height = window.innerHeight;
        var height = screen_height - 74;
        var kk = $("#sidebar-wrapper").height(height);
        function transfer() {
            $state.go('current-status-transfer-history');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('transfer');
            element.classList.add('active');
        }
        function course() {
            $state.go('current-status-course-attended');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('course');
            element.classList.add('active');
        }
        function opr() {
            $state.go('current-status-opr-grading');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('opr');
            element.classList.add('active');
        }
        function noticeBoard() {
            $state.go('flash-notice-board');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('noticeBoard');
            element.classList.add('active');
        }
        function award() {
            $state.go('current-status-medal-award-publication');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('award');
            element.classList.add('active');
        }
        function commendation() {
            $state.go('current-status-commendation-appreciation');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('commendation');
            element.classList.add('active');
        }
        function leave() {
            $state.go('current-status-leave-information');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('leave');
            element.classList.add('active');
        }
        function logout() {
            $state.go('log-out');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('logout');
            element.classList.add('active');
        }
        function password() {
            $state.go('change-password');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('password');
            element.classList.add('active');
        }
        function refresh() {
            $window.location.reload();
        }
        function print() {
            $state.go('print-section');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('print');
            element.classList.add('active');
        }
        function migration() {
            $state.go('migration');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > ul > li > a').removeClass("active");
            var element = document.getElementById('migration');
            element.classList.add('active');
        }
        function empHistory() {
            $state.go('employee-history');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('empHistory');
            element.classList.add('active');
        }
        function download() {
            $state.go('download-list');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('download');
            element.classList.add('active');
        }
        function user() {
            $state.go('users');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('user');
            element.classList.add('active');
        }
        function userCreate() {
            $state.go('users-create');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('userCreate');
            element.classList.add('active');
        }
        function feedBack() {
            $state.go('feed-back');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('feedBack');
            element.classList.add('active');
        }
        function userLog() {
            $state.go('user-log');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('userLog');
            element.classList.add('active');
        }
        function flashNotice() {
            $state.go('flash-notice');
            $('#myDiv > div > div > section > div.ng-scope > div > nav > div > ul > li > a').removeClass("active");
            var element = document.getElementById('flashNotice');
            element.classList.add('active');
        }

    }

})();