function SlotsResource($http) {
    return {
        getMoments: function () {
            return $http(
                {
                    method: 'GET',
                    url: "/umbraco/backoffice/tools/instantwin/getmoments",
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
    "slotsResource",
    SlotsResource);