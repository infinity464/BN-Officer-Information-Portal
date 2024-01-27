
(function () {

    'use strict';
    var controllerId = 'graphicalReportsController';
    angular.module('app').controller(controllerId, graphicalReportsController);
    graphicalReportsController.$inject = ['graphicalReportService','courseService', 'downloadService','reportService','notificationService', '$location'];

    function graphicalReportsController(graphicalReportService, courseService, downloadService, reportService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.graphicalReports = [];
        vm.deleteGraphicalReport = deleteGraphicalReport;
        vm.deleteAllGraphicalReport = deleteAllGraphicalReport;
        vm.downloadGraphicalOprListReport = downloadGraphicalOprListReport;
        vm.downloadGraphicalOprYearlyReport = downloadGraphicalOprYearlyReport;
        vm.downloadGraphicalTraceReport = downloadGraphicalTraceReport,
            vm.downloadGraphicalSeaServiceReport = downloadGraphicalSeaServiceReport,
            vm.downloadGraphicalSeaCommandServiceReport = downloadGraphicalSeaCommandServiceReport;

        //Graph
        vm.getLastOPRChart = getLastOPRChart;
        vm.getOPRYearlyChart = getOPRYearlyChart;
        vm.getSeaServiceChart = getSeaServiceChart;
        vm.getSeaCommandServiceChart = getSeaCommandServiceChart;
        vm.getCourseResultChart = getCourseResultChart;
        vm.getTraceChart = getTraceChart;



        vm.getCourseSubCategory = getCourseSubCategory;
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.venue = 1;

        vm.passingYears = [];
        vm.courseCategories = [];

        init();
        function init() {
            deleteAllGraphicalReport();
            graphicalReportService.getGraphicalReports().then(function (data) {
                //vm.graphicalReports = data.result.employeeReports;
                vm.passingYears = data.result.passingYears;
                vm.courseCategories = data.result.courseCategories;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function save() {
            if (vm.graphicalReport.employee.employeeId > 0) {
               
                vm.graphicalReport.employeeId = vm.graphicalReport.employee.employeeId;
                insertGraphicalReport();
            } else {
                notificationService.displayError("Please Search Valid Officer by PNo!");
            }
           
        }

        function insertGraphicalReport() {
            graphicalReportService.saveGraphicalReport(vm.graphicalReport).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function close() {
            vm.graphicalReport.employee = null;
            graphicalReportService.getGraphicalReports().then(function (data) {
                    vm.graphicalReports = data.result.employeeReports;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function deleteGraphicalReport(graphicalReport) {
            graphicalReportService.deleteGraphicalReport(graphicalReport.id).then(function (data) {

                //init();

                graphicalReportService.getGraphicalReports().then(function (data) {
                    vm.graphicalReports = data.result.employeeReports;
                    vm.passingYears = data.result.passingYears;
                    vm.courseCategories = data.result.courseCategories;
                },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });

            });
        }

        function deleteAllGraphicalReport() {
            graphicalReportService.deleteAllGraphicalReport().then(function (data) {
                vm.graphicalReports = [];
            });
        }
        function getCourseSubCategory(categoryId) {
            courseService.getCourseSubCategories(categoryId).then(function (data) {
                vm.courseSubCategories = data.result.courseSubCategories;
            });
        }


        function downloadGraphicalOprListReport(lastOprNo) {
            var url = reportService.downloadGraphicalOprListReport(lastOprNo);
            downloadService.downloadReport(url);
        }


      


        function downloadGraphicalOprYearlyReport(fromYear, toYear) {
            saveGraphReport(fromYear, toYear);
            var url = reportService.downloadGraphicalOprYearlyReport(fromYear, toYear);
            downloadService.downloadReport(url);
        }


    


        function downloadGraphicalTraceReport(fromYear, toYear) {
            var url = reportService.downloadGraphicalTraceReport(fromYear, toYear);
            downloadService.downloadReport(url);
        }

        function downloadGraphicalSeaServiceReport() {
            var url = reportService.downloadGraphicalSeaServiceReport();
            downloadService.downloadReport(url);
        }

        function downloadGraphicalSeaCommandServiceReport() {
            var url = reportService.downloadGraphicalSeaCommandServiceReport();
            downloadService.downloadReport(url);
        }

        function getLastOPRChart(lastOprNo) {
            graphicalReportService.getLastOPRChart(lastOprNo).then(function (data) {
                    var l = document.getElementById("line-chart");
                     var ctx = l.getContext("2d");
                    var lineChart = new Chart(ctx, {
                        type: 'line',
                        data: data.result,
                        options: {
                            responsive: true,
                            legend: {
                                display: true, position: 'bottom', fontColor: "black",
                                fontSize: 12 },
                            title: {
                                display: true,
                                text: 'OPR of Last ' + lastOprNo + " Year(s) Comparative Chart",
                                fontColor: '#2196F3',
                                fontStyle: 'bold',
                                fontSize: '16',
                                lineHeight: 5
                                    },
                            scales: {
                                yAxes: [{

                                    scaleLabel: {
                                        display: true,
                                        labelString: 'Point',
                                        fontColor: "black",
                                        fontSize: 14
                                    },
                                    ticks: {
                                        fontColor: "black",
                                        fontSize: 12
                                        
                                    },
                                  
                                }],
                                xAxes: [{
                                    scaleLabel: {
                                        display: true,
                                        labelString: 'Year',
                                        fontColor: "black",
                                        fontSize: 14
                                    },
                                    ticks: {
                                        fontColor: "black",
                                        fontSize: 12,
                                      
                                    },
                                
                                }]
                            }
                        }
                    });

                  
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function getOPRYearlyChart(fromYear, toYear) {
            if (fromYear <= toYear) {

                graphicalReportService.saveGraphReport(fromYear, toYear).then(function (data) {
                    graphicalReportService.getOPRYearlyChart(fromYear, toYear).then(function (data) {
                        var l = document.getElementById("line-chart");
                        var ctx = l.getContext("2d");
                        var lineChart = new Chart(ctx, {
                            type: 'line',
                            data: data.result,
                            options: {
                                responsive: true,
                                legend: {
                                    display: true, position: 'bottom', fontColor: "black",
                                    fontSize: 12
                                },
                                title: {
                                    display: true,
                                    text: 'OPR From ' + fromYear + " To " + toYear + " Comparative Chart",
                                    fontColor: '#2196F3',
                                    fontStyle: 'bold',
                                    fontSize: '16'
                                },
                                scales: {
                                    yAxes: [{

                                        scaleLabel: {
                                            display: true,
                                            labelString: 'Point',
                                            fontColor: "black",
                                            fontSize: 14
                                        },
                                        ticks: {
                                            fontColor: "black",
                                            fontSize: 12

                                        },

                                    }],
                                    xAxes: [{
                                        scaleLabel: {
                                            display: true,
                                            labelString: 'Year',
                                            fontColor: "black",
                                            fontSize: 14
                                        },
                                        ticks: {
                                            fontColor: "black",
                                            fontSize: 12,

                                        },

                                    }]
                                }
                            }
                        });


                    },
                        function (errorMessage) {
                            notificationService.displayError(errorMessage.message);
                        });
                });

 
            }
           
        }

        function getSeaServiceChart() {
           
            graphicalReportService.getSeaServiceChart().then(function (data) {
               
                    var l = document.getElementById("bar-chart-grouped");
                    var ctx = l.getContext("2d");
                    var barChart = new Chart(ctx, {
                        type: 'bar',
                        data: data.result,
                        options: {
                            "hover": {
                                "animationDuration": 0
                            },
                            "animation": {
                                "duration": 1,
                                "onComplete": function () {
                                    var chartInstance = this.chart,
                                        ctx = chartInstance.ctx;

                                    ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontSize, Chart.defaults.global.defaultFontStyle, Chart.defaults.global.defaultFontFamily);
                                    ctx.textAlign = 'center';
                                    ctx.textBaseline = 'bottom';

                                    this.data.datasets.forEach(function (dataset, i) {
                                        var meta = chartInstance.controller.getDatasetMeta(i);
                                        meta.data.forEach(function (bar, index) {
                                            var data = dataset.data[index];
                                            ctx.fillText(data, bar._model.x, bar._model.y - 5);
                                        });
                                    });
                                }
                            },
                            responsive: true,
                           
                            title: {
                                display: true,
                                text: "Sea Service Comparative Chart",
                                fontColor: '#2196F3',
                                fontStyle: 'bold',
                                fontSize: '16'
                            },
                            scales: {
                                yAxes: [{

                                    scaleLabel: {
                                        display: true,
                                        labelString: 'Days',
                                        fontColor: "black",
                                        fontSize: 14
                                    },
                                    ticks: {
                                        fontColor: "black",
                                        fontSize: 12

                                    },

                                }],
                                xAxes: [{
                                    scaleLabel: {
                                        display: true,
                                        labelString: 'PNo',
                                        fontColor: "black",
                                        fontSize: 14
                                    },
                                    ticks: {
                                        fontColor: "black",
                                        fontSize: 12,

                                    },

                                }]
                            }
                        }
                    });
                },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
            
           

        }

        function getSeaCommandServiceChart() {

            graphicalReportService.getSeaCommandServiceChart().then(function (data) {

                var l = document.getElementById("multi-bar-chart-grouped");
                var ctx = l.getContext("2d");
                var barChart = new Chart(ctx, {
                    type: 'bar',
                    data: data.result,
                    options: {
                        "hover": {
                            "animationDuration": 0
                        },
                        "animation": {
                            "duration": 1,
                            "onComplete": function () {
                                var chartInstance = this.chart,
                                    ctx = chartInstance.ctx;

                                ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontSize, Chart.defaults.global.defaultFontStyle, Chart.defaults.global.defaultFontFamily);
                                ctx.textAlign = 'center';
                                ctx.textBaseline = 'bottom';

                                this.data.datasets.forEach(function (dataset, i) {
                                    var meta = chartInstance.controller.getDatasetMeta(i);
                                    meta.data.forEach(function (bar, index) {
                                        var data = dataset.data[index];
                                        ctx.fillText(data, bar._model.x, bar._model.y - 5);
                                    });
                                });
                            }
                        },
                        responsive: true,

                        title: {
                            display: true,
                            text: "Sea Service Comparative Chart",
                            fontColor: '#2196F3',
                            fontStyle: 'bold',
                            fontSize: '16',
                            lineHeight: 5
                        },
                        scales: {
                            yAxes: [{

                                scaleLabel: {
                                    display: true,
                                    labelString: 'Days',
                                    fontColor: "black",
                                    fontSize: 14
                                },
                                ticks: {
                                    fontColor: "black",
                                    fontSize: 12

                                },

                            }],
                            xAxes: [{
                                scaleLabel: {
                                    display: true,
                                    labelString: 'PNo',
                                    fontColor: "black",
                                    fontSize: 14
                                },
                                ticks: {
                                    fontColor: "black",
                                    fontSize: 12,

                                },

                            }]
                        }
                    }
                });


            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });



        }

        function getTraceChart() {
            
                graphicalReportService.getTraceChart().then(function(data) {

                        var l = document.getElementById("multi-bar-chart-grouped");
                        var ctx = l.getContext("2d");
                        var barChart = new Chart(ctx,
                            {
                                type: 'bar',
                                data: data.result,
                                options: {
                                    "hover": {
                                        "animationDuration": 0
                                    },
                                    "animation": {
                                        "duration": 1,
                                        "onComplete": function () {
                                            var chartInstance = this.chart,
                                                ctx = chartInstance.ctx;

                                            ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontSize, Chart.defaults.global.defaultFontStyle, Chart.defaults.global.defaultFontFamily);
                                            ctx.textAlign = 'center';
                                            ctx.textBaseline = 'bottom';

                                            this.data.datasets.forEach(function (dataset, i) {
                                                var meta = chartInstance.controller.getDatasetMeta(i);
                                                meta.data.forEach(function (bar, index) {
                                                    var data = dataset.data[index];
                                                    ctx.fillText(data, bar._model.x, bar._model.y - 5);
                                                });
                                            });
                                        }
                                    },
                                    responsive: true,

                                    title: {
                                        display: true,
                                        text: "Trace Comparative Chart",
                                        fontColor: '#2196F3',
                                        fontStyle: 'bold',
                                        fontSize: '16'
                                    },
                                    scales: {
                                        yAxes: [
                                            {
                                                scaleLabel: {
                                                    display: true,
                                                    labelString: 'Marks',
                                                    fontColor: "black",
                                                    fontSize: 14
                                                },
                                                ticks: {
                                                    fontColor: "black",
                                                    fontSize: 12

                                                },

                                            }
                                        ],
                                        xAxes: [
                                            {
                                                scaleLabel: {
                                                    display: true,
                                                    labelString: 'PNo',
                                                    fontColor: "black",
                                                    fontSize: 14
                                                },
                                                ticks: {
                                                    fontColor: "black",
                                                    fontSize: 12,

                                                },

                                            }
                                        ]
                                    }
                                }
                            });


                    },
                    function(errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });

            

        }

        function getCourseResultChart(categoryId,subCategoryId,venue) {
           
            graphicalReportService.getCourseResultChart(categoryId, subCategoryId,venue).then(function (data) {

                var l = document.getElementById("multi-bar-chart-grouped");
                var ctx = l.getContext("2d");
                var barChart = new Chart(ctx, {
                    type: 'bar',
                    data: data.result,
                    options: {
                        "hover": {
                            "animationDuration": 0
                        },
                        "animation": {
                            "duration": 1,
                            "onComplete": function () {
                                var chartInstance = this.chart,
                                    ctx = chartInstance.ctx;

                                ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontSize, Chart.defaults.global.defaultFontStyle, Chart.defaults.global.defaultFontFamily);
                                ctx.textAlign = 'center';
                                ctx.textBaseline = 'bottom';

                                this.data.datasets.forEach(function (dataset, i) {
                                    var meta = chartInstance.controller.getDatasetMeta(i);
                                    meta.data.forEach(function (bar, index) {
                                        var data = dataset.data[index];
                                        ctx.fillText(data, bar._model.x, bar._model.y - 5);
                                    });
                                });
                            }
                        },
                        responsive: true,
                        title: {
                            display: true,
                            text: "Course Result Wise Comparative Chart",
                            fontColor: '#2196F3',
                            fontStyle: 'bold',
                            fontSize: '16',
                            lineHeight: 5
                        },
                        scales: {
                            yAxes: [{

                                scaleLabel: {
                                    display: true,
                                    labelString: 'Percentage',
                                    fontColor: "black",
                                    fontSize: 14
                                },
                                ticks: {
                                    fontColor: "black",
                                    fontSize: 12

                                },

                            }],
                            xAxes: [{
                                scaleLabel: {
                                    display: true,
                                    labelString: 'PNo',
                                    fontColor: "black",
                                    fontSize: 14
                                },
                                ticks: {
                                    fontColor: "black",
                                    fontSize: 12,

                                },

                            }]
                        }
                    }
                });


            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });



        }
    }


    

})();
