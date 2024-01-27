(function () {

    'use strict';
    var controllerId = 'publicationsController';
    angular.module('app').controller(controllerId, publicationsController);
    publicationsController.$inject = ['$state', 'publicationService', 'notificationService', '$location'];

    function publicationsController($state, publicationService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.publications = [];
        vm.addPublication = addPublication;
        vm.updatePublication = updatePublication;
        vm.deletePublication = deletePublication;
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
            publicationService.getPublications(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.publications = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addPublication() {
            $state.go('publication-create');
        }

        function updatePublication(publication) {
            $state.go('publication-modify', { id: publication.publicationId});
        }

        function deletePublication(publication) {
            publicationService.deletePublication(publication.publicationId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('publications', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
