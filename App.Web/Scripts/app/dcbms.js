var app = angular.module("dcbmsApp", []);

app.controller("addController", function($scope, $http) {
    $scope.testTypes = {};

    $http.get("/api/TestTypesAPI").then(function (response) {
        $scope.testTypes = response.data;
    });

    $scope.submit = function() {
        $http.post("/api/TestTypesAPI", $scope.testType) .then(function(response) {
            if (response.status === 201) {
                $scope.testTypes.push(response.data);
            }
        });
    };
});