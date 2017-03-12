(function () {
    'use strict';

    angular
        .module('Search4Self')
        .factory('API', API);

    API.$inject = ['$resource', '$rootScope'];

    function API($resource, $rootScope) {
        var baseUrl = '/api/';
        $rootScope.baseUrl = baseUrl;

        var res = $resource('/', {}, {
            // Music
            getMusicGenres: {
                url: baseUrl + 'music/genres',
                method: 'GET',
                isArray: false
            },
            getMusicWords: {
                url: baseUrl + 'music/words',
                method: 'GET',
                isArray: true
            },
            getGoogleSearches: {
                url: baseUrl + 'google/searches',
                method: 'GET',
                isArray: true
            },

            getMe: {
                url: baseUrl + 'account/whoAmI',
                method: 'GET',
                isArray: false
            },


        });

        return res;
    }
})();