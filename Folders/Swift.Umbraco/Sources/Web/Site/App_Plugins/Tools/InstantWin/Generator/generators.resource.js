function GeneratorResource($http) {
    return {
        getAllocables: function () {
            return $http(
                {
                    method: 'GET',
                    url: "/umbraco/backoffice/tools/instantwin/getallocables",
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
        },
        getLimitOptions: function () {
            return $http(
                {
                    method: 'GET',
                    url: "/umbraco/backoffice/tools/instantwin/getlimitoptions",
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
        },
        generateMoments: function (requestData) {
            return $http(
                {
                    method: 'POST',
                    url: "/umbraco/backoffice/tools/instantwin/generate",
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    data: requestData
                })
                .then(
                    function (response) {
                        return response;
                    },
                    function (error) {
                        return error;
                    }
                );
        },
        gatherAllocables: function (ids, names, numbers)
        {
            var newAllocables = [];
            numbers.forEach(setNumberValue);

            function setNumberValue(item, index, arr) {
                var objectItem = {
                    Id: ids[index],
                    Name: names[index],
                    Number: numbers[index]
                };
                newAllocables.push(objectItem);
            }

            return newAllocables;
        }
    };
}

angular.module("umbraco.resources").factory(
    "generatorResource",
    GeneratorResource);