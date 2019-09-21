function GeneratorController(
    $scope,
    generatorResource,
    notificationsService) {

    $scope.init = function () {
        generatorResource.getLimitOptions()
        .then(
            function (response)
            {
                return response.data;
            },
            function (error) {
                return error.data;
            }
        )
        .then(
            function (parsedJson) {
                if (parsedJson.Success) {
                    $scope.limitOptions = parsedJson.Data.LimitOptions;
                }
                else {
                    notificationsService.error(
                        "Error",
                        "[Could not retrieve options] Status: " + parsedJson.Message +
                        ", Description: " + parsedJson.Data.Description);
                }
            },
            function (error) {
                notificationsService.error(
                    "Error",
                    "[Could not retrieve options] Message: " + error.message);
            }
        );

        generatorResource.getAllocables()
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
                    $scope.allocables = parsedJson.Data.Allocables;
                    $scope.allocableId = $scope.allocables.map(
                        function (allocable) {
                            return allocable.Id;
                        });
                    $scope.allocableName = $scope.allocables.map(
                        function (allocable) {
                            return allocable.Name;
                        });
                    $scope.allocableNumber = $scope.allocables.map(
                        function (allocable) {
                            return allocable.Number;
                        });
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

    $scope.generate = function () {
        
        if (!$scope.isGenerating) {

            var allocableData = generatorResource.gatherAllocables(
                $scope.allocableId,
                $scope.allocableName,
                $scope.allocableNumber);

            var requestData = {
                StartDate: $scope.model.startdate,
                EndDate: $scope.model.enddate,
                OpenTime: $scope.model.openhour,
                CloseTime: $scope.model.closinghour,
                LimitOption: $scope.model.limitoption,
                LimitNumber: $scope.model.limitnumber,
                Allocable: allocableData
            };

            //$scope.isGenerating = true;

            generatorResource.generateMoments(requestData)
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
                        notificationsService.success(
                            "Success",
                            "Generation status: " + parsedJson.Message +
                            ", Number: " + parsedJson.Data.GeneratedNumber);
                        //$scope.isGenerating = false;
                    }
                    else {
                        notificationsService.error(
                            "Error",
                            "[Generation failed] Status: " + parsedJson.Message +
                            ", Description: " + parsedJson.Data.Description);
                    }
                },
                function (error) {
                    notificationsService.error(
                        "Error",
                        "[Generation failed] Message: " + error.message);
                }
            );
        }
    };

    $scope.isGenerating = false;
    $scope.generateDatabaseLabel = $scope.isGenerating ? 'Please wait...' : 'Generate in database';
    $scope.generateFileLabel = $scope.isGenerating ? 'Please wait...' : 'Generate in file';
    $scope.allocables = [];
    $scope.limitOptions = [];
    $scope.model = {};
    $scope.allocableNumber = [];
    $scope.allocableId = [];
    $scope.allocableName = [];
}
angular.module('umbraco')
       .controller("generator.controller", GeneratorController);