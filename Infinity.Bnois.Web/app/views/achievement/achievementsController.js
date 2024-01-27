

(function () {

    'use strict';
    var controllerId = 'achievementsController';
    angular.module('app').controller(controllerId, achievementsController);
    achievementsController.$inject = ['$state', 'achievementService', 'notificationService', '$location'];

    function achievementsController($state, achievementService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.achievements = [];
        vm.addAchievement = addAchievement;
        vm.updateAchievement = updateAchievement;
        vm.deleteAchievement = deleteAchievement;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;

        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
            vm.searchText = location.search().q;
        }
        init();
        function init() {
            achievementService.getAchievements(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.achievements = data.result;
                vm.total = data.total; vm.permission = data.permission; 
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addAchievement() {
            $state.go('achievement-create');
        }

        function updateAchievement(achievement) {
            $state.go('achievement-modify', { id: achievement.achievementId });
        }

        function deleteAchievement(achievement) {
            achievementService.deleteAchievement(achievement.achievementId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('achievements', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }

    }

})();

