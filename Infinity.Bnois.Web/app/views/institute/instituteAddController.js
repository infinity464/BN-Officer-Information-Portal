

(function () {

	'use strict';

    var controllerId = 'instituteAddController';

    angular.module('app').controller(controllerId, instituteAddController);
    instituteAddController.$inject = ['$stateParams', 'instituteService', 'notificationService', '$state'];

    function instituteAddController($stateParams, instituteService, notificationService, $state) {
		var vm = this;
        vm.instituteId = 0;
        vm.title = 'ADD MODE';
        vm.institute = {};	
        vm.boardTypes = [];
        vm.boards = [];
		vm.saveButtonText = 'Save';
		vm.save = save;
		vm.close = close;
        vm.instituteForm = {};
        vm.getBoardsByBoardType = getBoardsByBoardType

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.instituteId = $stateParams.id;
			vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
		}

		Init();
        function Init() {
            instituteService.getInstitute(vm.instituteId).then(function (data) {
                vm.institute = data.result.institute;	
                vm.boardTypes = data.result.boardTypes;	
                vm.boards = data.result.boards;	
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		};

		function save() {

            if (vm.instituteId !== 0) {
                updateInstitute();
			} else {
                insertInstitute();
			}
		}

        function insertInstitute() {
            instituteService.saveInstitute(vm.institute).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

        function updateInstitute() {
            instituteService.updateInstitute(vm.instituteId, vm.institute).then(function (data) {
				close();
			},
				function (errorMessage) {
					notificationService.displayError(errorMessage.message);
				});
		}

		function close() {	
            $state.go('institutes');
        }

        function getBoardsByBoardType(boardType) {
            instituteService.getBoardsByBoardType(boardType).then(function (data) {
                vm.boards = data.result.boards;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

	}
})();
