function SlotsController(
    $scope,
    slotsResource,
    notificationsService) {

    $scope.slots = [];

    $scope.init = function () {
        slotsResource.getMoments()
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
                    $scope.slots = parsedJson.Data.Moments;
                }
                else {
                    notificationsService.error(
                        "Error",
                        "[Could not retrieve moments] Status: " + parsedJson.Message +
                        ", Description: " + parsedJson.Data.Description);
                }
            },
            function (error) {
                notificationsService.error(
                    "Error",
                    "[Could not retrieve moments] Message: " + error.message);
            }
        );
    };
}

angular.module('umbraco').controller(
    "slot.controller",
    SlotsController);