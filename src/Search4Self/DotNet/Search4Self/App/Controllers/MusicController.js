angular
    .module('Search4Self')
    .controller('MusicController', function (API, HelperService, $timeout, $filter, $scope) {

        var ctrl = this;

        ctrl.genres = [];
        ctrl.genresData = [];

        function init() {
            //$timeout(function () {
            //    ctrl.genres = ['test', 'kaka'];
            //    ctrl.genresData = [{
            //        "date": "2014-03-01",
            //        "test": 18,
            //        "kaka": 15
            //    },
            //    {
            //        "date": "2014-03-02",
            //        "test": 8,
            //        "kaka": 5
            //        }];
            //    console.log('acum');
            //}, 1000);

            //HelperService.ShowMessage("Un mesaj!", true);

            $scope.getMusicGenres();
        }

        $scope.getMusicGenres = function () {
            HelperService.StartLoading('getMusicGenres');
            API.getMusicGenres(function (success) {

                if (success.genres == null || success.genres.length == 0) {
                    HelperService.ShowMessage("There is no data, please upload on the main page!");
                }

                ctrl.genres = success.genres;

                success.data.forEach(function (el) {

                });

                HelperService.StopLoading('getMusicGenres');
            }, function (error) {
                HelperService.StopLoading('getMusicGenres');
                HelperService.ShowMessage("Check Internet connection and reload!");
            });
        }

      
        init();
    });