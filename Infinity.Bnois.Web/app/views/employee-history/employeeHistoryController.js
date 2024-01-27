


(function () {

    'use strict';

    var controllerId = 'employeeHistoryController';

    angular.module('app').controller(controllerId, employeeHistoryController);
    employeeHistoryController.$inject = ['$stateParams', 'codeValue', 'employeeLeaveService', 'employeeService', 'notificationService', 'currentStatusService', '$state'];

    function employeeHistoryController($stateParams, codeValue, employeeLeaveService, employeeService, notificationService, currentStatusService, $state) {
        var vm = this;
        vm.pNo = '';
        vm.officerName = '';
        vm.currentStatus = {};
        vm.permanentTransferHistories = {};
        vm.courseAttendeds = {};
        vm.leaveInfos = [];
        vm.leaveInfo = {};
        vm.publications = [];
        vm.commendations = [];
        vm.appreciations = [];
        vm.oprGradings = [];
        vm.getOprIndexs = [];
        vm.searchText = "";
        vm.searchSubject = "";
        vm.SearchEmployee = SearchEmployee;
        vm.ClearEmployee = ClearEmployee;
        vm.clearSearch = clearSearch;
        vm.options = [
            {
                text: 'Current Status',
                value: '1'
            }, {
                text: 'Appointment History',
                value: '2'
            },{
                text: 'Course History',
                value: '3'
            }, {
                text: 'Details of Leave',
                value: '4'
            }, {
                text: 'Publication',
                value: '5'
            }, {
                text: 'Commendation',
                value: '6'
            }, {
                text: 'OPR',
                value: '7'
            }

        ];
        Init();
        function Init() {
            
        };
        function ClearEmployee() {
            vm.currentStatus = {};
            vm.permanentTransferHistories = {};
            vm.courseAttendeds = {};
            vm.leaveInfos = [];
            vm.leaveInfo = {};
            vm.publications = [];
            vm.commendations = [];
            vm.appreciations = [];
            vm.oprGradings = [];
            vm.getOprIndexs = [];
        };
        function clearSearch() {
            vm.searchText = "";
            vm.searchSubject = "";
        };
        //function basicEmployee() {
        //    employeeService.getEmployeeByPno1(vm.searchText).then(function (data) {
        //        vm.officerName = data.result.fullNameEng;
        //        vm.pNo = data.result.pNo;
        //    },
        //        function (errorMessage) {
        //            notificationService.displayError(errorMessage.message);
        //        });
        //};

        function SearchEmployee() {
            if (vm.searchText === "") {
                notificationService.displayError("Please give me a P No.");
            } else {
                /*Current Status*/
                if (vm.searchSubject === "1") {
                    ClearEmployee();
                    currentStatusService.getCurrentStatus1(vm.searchText).then(function (data) {
                        vm.currentStatus = data.result;
                        if (vm.currentStatus.length === 0) {
                            notificationService.displayError('Data not found.');
                        }
                        if (vm.currentStatus === null) {
                            clearSearch();
                            ClearEmployee();
                            notificationService.displayError('Officer not found.');
                        } else {
                            if (vm.currentStatus.photo != null) {
                                vm.currentStatus.photo = codeValue.API_OFFICER_PICTURE_URL + vm.currentStatus.photo;
                            } else {
                                vm.currentStatus.photo = "../../../Images/officer.png";
                            }
                        }
                    },
                        function (errorMessage) {
                            clearSearch();
                            ClearEmployee();
                            notificationService.displayError(errorMessage.message);
                        });
                }
                else if (vm.searchSubject === "2") {
                    ClearEmployee();
                    currentStatusService.getTransferHistory(vm.searchText).then(function (data) {
                        vm.permanentTransferHistories = data.result;
                        if (vm.permanentTransferHistories.length === 0) {
                            notificationService.displayError('Data not found.');
                        }
                        if (vm.permanentTransferHistories === null) {
                            ClearEmployee();
                            clearSearch();
                            notificationService.displayError('Officer not found.');
                        }
                        //else {
                        //    basicEmployee();
                        //}
                    },
                        function (errorMessage) {
                            clearSearch();
                            ClearEmployee();
                            notificationService.displayError(errorMessage.message);
                        });
                }
                else if (vm.searchSubject === "3") {
                    ClearEmployee();
                    currentStatusService.getCourseAttended(vm.searchText).then(function (data) {
                        vm.courseAttendeds = data.result;
                        if (vm.courseAttendeds.length === 0) {
                            notificationService.displayError('Data not found.');
                        }
                        if (vm.courseAttendeds === null) {
                            ClearEmployee();
                            clearSearch();
                            notificationService.displayError('Officer not found.');
                        }
                        //else {
                        //    basicEmployee();
                        //}
                    },
                        function (errorMessage) {
                            clearSearch();
                            ClearEmployee();
                            notificationService.displayError(errorMessage.message);
                        });
                }
                else if (vm.searchSubject === "4") {
                    ClearEmployee();
                    currentStatusService.getLeaveInfo(vm.searchText).then(function (data) {
                        vm.leaveInfo = data.result;
                        vm.plAvailable = Math.abs(vm.leaveInfo.plAvailable);
                        if (vm.leaveInfo === null) {
                            ClearEmployee();
                            clearSearch();
                            notificationService.displayError('Officer not found.');
                        }
                        //else {
                        //    basicEmployee();
                        //}
                    },
                        function (errorMessage) {
                            clearSearch();
                            ClearEmployee();
                            notificationService.displayError(errorMessage.message);
                        });

                    currentStatusService.getEmployeeLeaveAndEmployeeInfo(vm.searchText).then(function (data) {
                        vm.leaveInfos = data.result.leaveDetails;
                        if (vm.leaveInfos === null) {
                            ClearEmployee();
                            clearSearch();
                            notificationService.displayError('Officer not found.');
                        }
                        //else {
                        //    basicEmployee();
                        //}
                    },
                        function (errorMessage) {
                            clearSearch();
                            ClearEmployee();
                            notificationService.displayError(errorMessage.message);
                        });

                    if (vm.leaveInfos.length === 0 && vm.leaveInfo.length === 0) {
                        notificationService.displayError('Data not found.');
                    }
                }
                else if (vm.searchSubject === "5") {
                    ClearEmployee();
                    currentStatusService.getPublication(vm.searchText).then(function (data) {
                        vm.publications = data.result;
                        if (vm.publications.length === 0) {
                            notificationService.displayError('Data not found.');
                        }

                        if (vm.publications === null) {
                            ClearEmployee();
                            clearSearch();
                            notificationService.displayError('Officer not found.');
                        }
                        //else {
                        //    basicEmployee();
                        //}
                    },
                        function (errorMessage) {
                            clearSearch();
                            ClearEmployee();
                            notificationService.displayError(errorMessage.message);
                        });
                }
                else if (vm.searchSubject === "6") {
                    ClearEmployee();
                    currentStatusService.getCommendationAppreciation(vm.searchText, 1).then(function (data) {
                        vm.commendations = data.result;
                        
                        if (vm.commendations === null) {
                            ClearEmployee();
                            clearSearch();
                            notificationService.displayError('Officer not found.');
                        }
                        //else {
                        //    basicEmployee();
                        //}
                    },
                        function (errorMessage) {
                            clearSearch();
                            ClearEmployee();
                            notificationService.displayError(errorMessage.message);
                        });

                    currentStatusService.getCommendationAppreciation(vm.searchText, 2).then(function (data) {
                        vm.appreciations = data.result;
                        if (vm.appreciations.length === 0 && vm.commendations.length === 0) {
                            notificationService.displayError('Data not found.');
                        }
                        if (vm.appreciations === null) {
                            ClearEmployee();
                            clearSearch();
                            notificationService.displayError('Officer not found.');
                        }
                        //else {
                        //    basicEmployee();
                        //}
                    },
                        function (errorMessage) {
                            clearSearch();
                            ClearEmployee();
                            notificationService.displayError(errorMessage.message);
                        });
                }
                else if (vm.searchSubject === "7") {
                    ClearEmployee();
                    currentStatusService.getOprGrading(vm.searchText).then(function (data) {
                        vm.oprGradings = data.result;
                        if (vm.oprGradings === null) {
                            ClearEmployee();
                            clearSearch();
                            notificationService.displayError('Officer not found.');
                        }
                        //else {
                        //    basicEmployee();
                        //}
                    },
                        function (errorMessage) {
                            clearSearch();
                            ClearEmployee();
                            notificationService.displayError(errorMessage.message);
                        });

                    currentStatusService.getOprIndex(vm.searchText).then(function (data) {
                        vm.getOprIndexs = data.result;
                        if (vm.getOprIndexs === null) {
                            ClearEmployee();
                            clearSearch();
                            notificationService.displayError('Officer not found.');
                        }
                        //else {
                        //    basicEmployee();
                        //}
                    },
                        function (errorMessage) {
                            clearSearch();
                            ClearEmployee();
                            notificationService.displayError(errorMessage.message);
                        });


                }
               

                else {
                    notificationService.displayError("Please select a category.");
                }
            };
        }
    }
})();
