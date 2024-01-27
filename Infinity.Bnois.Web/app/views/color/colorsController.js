/// <reference path="../../services/colorService.js" />

(function () {

    'use strict';
    var controllerId = 'colorsController';
    angular.module('app').controller(controllerId, colorsController);
    colorsController.$inject = ['$state', 'colorService', 'notificationService', '$location'];

    function colorsController($state, colorService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.color = [];
        vm.addColor = addColor;
        vm.updateColor = updateColor;
        vm.deleteColor = deleteColor;
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
            colorService.getColors(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.colors = data.result;
                console.log(data.result);
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addColor() {
            $state.go('color-create');
        }

        function updateColor(color) {
            $state.go('color-modify', { id: color.colorId });
        }

        function deleteColor(color) {
            colorService.deleteColor(color.colorId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('colors', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
