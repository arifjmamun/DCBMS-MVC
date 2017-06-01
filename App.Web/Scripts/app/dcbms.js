var app = angular.module("dcbmsApp", ["ui.bootstrap.modal"]);

app.run(function($rootScope, $http) {
    $rootScope.testTypes = {};
    $http.get("/api/TestTypesAPI").then(function (response) {
        $rootScope.testTypes = response.data;
    });
});

app.controller("testTypeController", function ($rootScope, $scope, $http, $filter) {

    $scope.submit = function () {
        $http.post("/api/TestTypesAPI", $scope.testType) .then(function(response) {
            if (response.status === 201) {
                $rootScope.testTypes.push(response.data);

                $scope.addTestTypeForm.$setPristine();
                $scope.addTestTypeForm.$setUntouched();
                $scope.testType = {};
                $scope.isAdded = true;
                return;
            }
            $scope.isAdded = false;
            return;
        });
    };

    $scope.editTestType = function(e) {
        $scope.testType = {};
        if (e.testType !== null) {
            $rootScope.$broadcast("testType", e.testType);
        }
    };

    $scope.confirmDelete = function(e) {
        swal({
            title: 'Confirmation',
            text: "Are you sure want to delete the data?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }, function(isConfirmed) {
            if (isConfirmed) {
                $http.delete("/api/TestTypesAPI/" + e.testType.TestTypeId).then(function (response) {
                    if (response.status === 200) {
                        $rootScope.testTypes = $filter("filter")($rootScope.testTypes, function (item) {
                            return item.TestTypeId !== e.testType.TestTypeId;
                        });
                    }
                });
            }
        });
    };
});

app.controller("modalController", function ($rootScope, $scope, $http) {
    $scope.testType = {};
    $scope.$on("testType", function(e, data) {
        $scope.testType.TestTypeId = data.TestTypeId;
        $scope.testType.TestTypeName = data.TestTypeName;
    });

    $scope.saveChangesTestType = function () {
        $http.put("/api/TestTypesAPI/" + $scope.testType.TestTypeId, $scope.testType).then(function (response) {
            if (response.status === 204) {
                $("#editTestTypeModal").modal("hide"); //not appropriate use of angular (Jquery Used)

                angular.forEach($rootScope.testTypes, function(item) {
                    if (item.TestTypeId === $scope.testType.TestTypeId) {
                        item.TestTypeName = $scope.testType.TestTypeName;
                    }
                });
            }
        });
    };
});