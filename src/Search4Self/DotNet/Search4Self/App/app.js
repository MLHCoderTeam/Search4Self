/**
     * You must include the dependency on 'ngMaterial'
     */
angular.module('Search4Self', ['ngMaterial', 'ngRoute', 'ngResource', 'ngFileUpload'])
    //.controller('LeftCtrl', function ($scope, $timeout, $mdSidenav, $log) {
    //    $scope.close = function () {
    //        // Component lookup should always be available since we are not using `ng-if`
    //        $mdSidenav('left').close()
    //            .then(function () {
    //                $log.debug("close LEFT is done");
    //            });

    //    };

    .config(function ($routeProvider, $locationProvider) {

        $routeProvider
            .when('/', {
                templateUrl: '/Views/Index.html',
                controller: 'IndexController'
            })
            //.when('/cartier/:districtId/:districtName', {
            //    templateUrl: '/Views/District.html',
            //    controller: 'DistrictController'
            //})
            .when('/youtube', {
                templateUrl: '/Views/Music.html',
                controller: 'MusicController',
                controllerAs:'ctrl'
            })
            .when('/google', {
                templateUrl: '/Views/Google.html',
                controller: 'GoogleController',
                controllerAs: 'ctrl'
            })

            .otherwise({
                templateUrl: '/Views/Inexistent.html',
            });
        //.when('/contact', {
        //    templateUrl : 'partials/contact.html',
        //    controller : mainController
        //});


        // use the HTML5 History API
        $locationProvider.html5Mode(true);
    });