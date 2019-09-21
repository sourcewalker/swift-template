function PrizeController(
    $scope,
    prizeResource,
    notificationsService) {

    $scope.prizes = [];

    $scope.init = function () {
        prizeResource.getPrizes()
        .then(
            function (response) {
                return response.data;
            },
            function (error) {
                return error.data;
            }
        )
        .then(
            function (parsedJson) {
                if (parsedJson.Success) {
                    $scope.prizes = parsedJson.Data.Prizes;
                }
                else {
                    notificationsService.error(
                        "Error",
                        "[Could not retrieve prizes] Status: " + parsedJson.Message +
                                                  ", Description: " + parsedJson.Data.Description);
                }
            },
            function (error) {
                notificationsService.error(
                    "Error",
                    "[Could not retrieve prizes] Message: " + error.message);
            }
        );
    };
}

angular.module('umbraco')
    .controller('prize.controller', PrizeController);