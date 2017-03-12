(function () {
    'use strict';

    angular
        .module('Search4Self')
        .directive('barChart',
        function () {
            return {
                restrict: 'E',
                //replace: true,
                scope: {
                    data: '=data',
                    title: '=title'
                },
                template: '<div id="chartdiv-bar-chart" style="min-width: 310px; height: 500px; margin: 0 auto"></div>',
                link: function (scope, element, attrs) {

                    scope.$watch('data', function (newValue, oldValue) {
                        initChart();
                    });


                    var chart = false;

                    var initChart = function () {
                        if (chart) chart.destroy();


                        var data = [
                            //{
                            //    "word": "killer",
                            //    "count": 18,
                            //},
                            //{
                            //    "word": "cars",
                            //    "count": 8,
                            //},
                        ];
                        if (scope.data && scope.data.length > 0)
                            data = scope.data;

                        var config = {
                            "type": "serial",
                            "theme": "light",
                            "dataProvider": [],
                            "valueAxes": [{
                                "gridColor": "#FFFFFF",
                                "gridAlpha": 0.2,
                                "dashLength": 0
                            }],
                            "gridAboveGraphs": true,
                            "startDuration": 1,
                            "graphs": [{
                                "balloonText": "[[category]]: <b>[[value]]</b>",
                                "fillAlphas": 0.8,
                                "lineAlpha": 0.2,
                                "type": "column",
                                "valueField": "count"
                            }],
                            "chartCursor": {
                                "categoryBalloonEnabled": false,
                                "cursorAlpha": 0,
                                "zoomable": false
                            },
                            "categoryField": "word",
                            "categoryAxis": {
                                "gridPosition": "start",
                                "gridAlpha": 0,
                                "tickPosition": "start",
                                "tickLength": 20,
                                "labelRotation": 45
                            }
                        };

                        config.dataProvider = data;

                        if (scope.title) {
                            config.titles = [{
                                "id": "Title-1",
                                "size": 15,
                                "text": scope.title
                            }];
                        }

                        chart = AmCharts.makeChart("chartdiv-bar-chart", config);

                    };
                    initChart();


                }//end watch           
            }
        });



})();