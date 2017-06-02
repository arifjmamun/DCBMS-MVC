var app = angular.module("dcbmsApp", []);

app.run(function ($rootScope, $http) {
    $rootScope.tests = {};
    $http.get("/api/TestsAPI").then(function (response) {
        $rootScope.testTypes = response.data;
    });
});

app.controller("testController", function ($rootScope, $scope, $http, $filter) {

    $scope.submit = function () {
        if ($scope.addTestForm.$valid) {
            $http.post("/api/TestsAPI", $scope.test) .then(function(response) {
                toastr.options = {
                    "closeButton": true,
                    "debug": false,
                    "newestOnTop": true,
                    "progressBar": true,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": true,
                    "showDuration": "300",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                };
                if (response.status === 201) {
                    $rootScope.tests.push(response.data);

                    $scope.addTestForm.$setPristine();
                    $scope.addTestForm.$setUntouched();
                    $scope.test = {};
                    //notification

                    toastr.success('Success!', 'Test added!');
                    return;
                }
                toastr.error('Error!', 'Internal Problem! Try Again!');
                return;
            });
        }
    };

    $scope.editTest = function (e) {
        $scope.test = {};
        if (e.test !== null) {
            $rootScope.$broadcast("test", e.test);
        }
    };

    $scope.confirmDelete = function (e) {
        swal({
            title: 'Confirmation',
            text: "Are you sure want to delete the data?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }, function (isConfirmed) {
            if (isConfirmed) {
                $http.delete("/api/TestsAPI/" + e.test.TestId).then(function (response) {
                    if (response.status === 200) {
                        $rootScope.tests = $filter("filter")($rootScope.tests, function (item) {
                            return item.TestId !== e.test.TestId;
                        });
                    }
                });
            }
        });
    };
});

app.controller("modalController", function ($rootScope, $scope, $http) {
    $scope.test = {};
    $scope.$on("test", function (e, data) {
        $scope.test.TestId = data.TestId;
        $scope.test.TestName = data.TestName;
        $scope.test.Fee = data.Fee;
        $scope.test.TestTypeId = data.TestTypeId;
    });

    $scope.saveChangesTest = function () {
        $http.put("/api/TestsAPI/" + $scope.test.TestId, $scope.test).then(function (response) {
            if (response.status === 204) {
                $("#editTestModal").modal("hide"); //not appropriate use of angular (Jquery Used)

                angular.forEach($rootScope.tests, function (item) {
                    if (item.TestId === $scope.test.TestId) {
                        item.TestName = $scope.test.TestName;
                        item.Fee = $scope.test.Fee;
                        item.TestTypeId = $scope.test.TestTypeId;
                    }
                });
            }
        });
    };
});