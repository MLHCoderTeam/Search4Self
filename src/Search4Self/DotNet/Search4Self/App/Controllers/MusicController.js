angular
    .module('Search4Self')
    .controller('MusicController', function (API, HelperService, $timeout, $filter, $scope) {
        
        var ctrl = this;

        ctrl.genres = ['rock'];
        ctrl.genresData = [];

        function init() {
            //HelperService.StartLoading('loadUnacceptedProblems');
            //API.getAllUnacceptedProblems({}, function (success) {
            //    $scope.unacceptedProblems = success;
            //    HelperService.StopLoading('loadUnacceptedProblems');
            //}, function (error) {
            //    HelperService.StopLoading('loadUnacceptedProblems');
            //    HelperService.ShowMessage('alert-danger', "Verificați conexiunea la internet și reîncărcați pagina!");
            //});

            $timeout(function () {
                ctrl.genres = ['test', 'kaka'];
                ctrl.genresData = [{
                    "date": "2014-03-01",
                    "test": 18,
                    "kaka": 15
                },
                {
                    "date": "2014-03-02",
                    "test": 8,
                    "kaka": 5
                    }];
                console.log('acum');
            }, 1000);

            HelperService.ShowMessage("Un mesaj!", true);
        }

        $scope.acceptProblem = function (problem) {
            HelperService.StartLoading('acceptProblem');
            API.acceptProblem({ id: problem.id }, function (success) {

                var index = $scope.unacceptedProblems.indexOf(problem);
                $scope.unacceptedProblems.splice(index, 1);

                HelperService.StopLoading('acceptProblem');
            }, function (error) {
                HelperService.StopLoading('acceptProblem');
                HelperService.ShowMessage('alert-danger', "Verificați conexiunea la internet și reîncărcați pagina!");
            });
        }

        $scope.deleteProblem = function (problem) {
            HelperService.StartLoading('deleteProblem');
            API.deleteProblem({ id: problem.id }, function (success) {

                var index = $scope.unacceptedProblems.indexOf(problem);
                $scope.unacceptedProblems.splice(index, 1);

                HelperService.StopLoading('deleteProblem');
            }, function (error) {
                HelperService.StopLoading('deleteProblem');
                HelperService.ShowMessage('alert-danger', "Verificați conexiunea la internet și reîncărcați pagina!");
            });
        }

        init();
    });