angular
    .module('Search4Self')
    .controller('GoogleController', function (API, HelperService, $timeout, $filter, $scope) {

        var ctrl = this;

        ctrl.searches = [];

        function init() {

            $scope.getGoogleSearches();
        }

        $scope.getGoogleSearches = function () {
            HelperService.StartLoading('getGoogleSearches');
            API.getGoogleSearches(function (success) {

                if (success == null || success.length == 0) {
                    HelperService.ShowMessage("There is no data, please upload on the main page!");
                }

                ctrl.searches = success;


                HelperService.StopLoading('getGoogleSearches');
            }, function (error) {
                HelperService.StopLoading('getGoogleSearches');
                HelperService.ShowMessage("Check Internet connection and reload!");
            });
        }


        init();
    });