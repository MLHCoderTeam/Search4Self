angular
    .module('Search4Self')
    .controller('IndexController', function (API, HelperService, $timeout, $filter, $scope, Upload, $location) {
        $scope.zipFile = null;
        $scope.progress = 0;

        function init() {

        }

        init();

        $scope.upload = function (file) {
            $scope.zipFile = file;

            if (file == null) return;
            HelperService.StartLoading('getMusicGenres');
            Upload.upload({
                url: '/api/file/upload',
                data: { file: file, 'username': $scope.username }
            }).then(function (resp) {
                console.log('Success ' + resp.config.data.file.name + 'uploaded. Response: ' + resp.data);

                $scope.zipFile = null;
                HelperService.ShowMessage("File uploaded successfully!");
                HelperService.StopLoading('getMusicGenres');
            }, function (resp) {
                HelperService.StopLoading('getMusicGenres');
                console.log('Error status: ' + resp.status);
            }, function (evt) {
                $scope.progress = parseInt(100.0 * evt.loaded / evt.total);
                //console.log('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
            });
        }

    });