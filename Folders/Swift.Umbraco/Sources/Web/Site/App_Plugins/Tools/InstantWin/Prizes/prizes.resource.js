function PrizeResource($http) {
    return {
        getPrizes: function () {
            return $http(
                {
                    method: 'GET',
                    url: "/umbraco/backoffice/tools/instantwin/getprizes",
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                .then(
                    function (response) {
                        return response;
                    },
                    function (error) {
                        return error;
                    }
                );
        }
    };
}

angular.module("umbraco.resources").factory(
    "prizeResource",
    PrizeResource);