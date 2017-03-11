(function () {
    'use strict';

    angular
        .module('Search4Self')
        .factory('HelperService', HelperService);


    function HelperService($mdToast) {
        var loadingItems = [];
        var loadingElement = $('#loading');
        var messageDiv = $("#message");


        var service = {
            StartLoading: startLoading,
            StopLoading: stopLoading,
            ShowMessage: showMessage,
        };

        function initialize() {

        }
        //initialize();

        return service;

        /* IMPLEMENTATION */


        function checkIfLoading() {
            if (loadingItems.length > 0) {
                loadingElement.show();
            } else {
                loadingElement.hide();
            }
        }

        function startLoading(item) {
            //if (loadingItems.indexOf(item) < 0) {
            //}
            loadingItems.push(item);
            checkIfLoading();
        }

        function stopLoading(item) {
            var index = loadingItems.indexOf(item);
            if (index >= 0) {
                loadingItems.splice(index, 1);
            }
            checkIfLoading();
        }

        function showMessage(text, isWarning) {
            //messageDiv.addClass(className);
            //messageDiv.html(text);
            //messageDiv.fadeToggle(200);
            //setTimeout(function () {
            //    $("#message").fadeToggle(500);
            //}, 3000);
            //setTimeout(function () {
            //    $("#message").removeClass(className);
            //}, 3600);

            $mdToast.show(
                $mdToast.simple()
                    .textContent(text)
                    .position("bottom right")
                    .theme(isWarning?'warn':'default')
                    .hideDelay(3000)
            );
        }
    }
})();