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
            //Users
            updateUserProblems: {
                url: baseUrl + 'admin/sourceProviders',
                method: 'POST',
                isArray: false
            },
            getTest: {
                url: baseUrl + 'test/test/:userName',
                method: 'GET',
                params: { userName: '@userName' },
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