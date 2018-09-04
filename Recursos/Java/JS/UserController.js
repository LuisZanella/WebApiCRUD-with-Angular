var config = {
    headers: {
        'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
    }
};
app.controller("UserController", ['$scope', '$http', '$location', '$routeParams', function ($scope, $http, $location, $routeParams)
{
    $scope.ListOfUsers;
    $scope.Status;
    //Get all employee and bind with html table
    $http({
        method: 'GET',
        url: 'api/OtherUser'
    }).then(function (response) {
        console.warn(response);
        $scope.ListOfUsers = response.data;
    }, function (error) {
        $scope.Status = "data not found";
        alert("Error");
    });
    $scope.Close = function () {
        $location.path('/UserList');
    };


    //Add new employee
    $scope.Add = function () {
        var UserData = {
            Name: $scope.Name,
            FLastName: $scope.FLastName,
            SLastName: $scope.SLastName,
            Nick: $scope.Nick,
            Password: $scope.Password,
            BirthDate: $scope.BirthDate
            // DepartmentID: $scope.DepartmentID
        };
        $http({
            method: 'Post',
            url: 'api/OtherUser',
            data: UserData,
            config
        }).then(function (response) {
            $location.path('/UserList');
            }, function (error) {
                console.log(error.data);
            $scope.error = "Something wrong when adding new employee " + data.ExceptionMessage;
        });
    };

    //Fill the employee records for update

    if ($routeParams.empId) {
        $scope.Id = $routeParams.empId;
        $http({
            method: 'GET',
            url: 'api/OtherUser/' + $scope.Id
        }).then(function (response) {
            $scope.Id = response.data.Id;
            $scope.Name = response.data.Name;
            $scope.FLastName = response.data.FLastName;
            $scope.SLastName = response.data.SLastName;
            $scope.Nick = response.data.Nick;
            $scope.Password = response.data.Password;
            $scope.BirthDate = response.data.BirthDate;
        }, function (error) {
            $scope.Status = "data not found";
            alert("Error");
        });
    }

    //Update the employee records
    $scope.Update = function () {
        var UserData = {
            Id: $scope.Id,
            Name: $scope.Name,
            FLastName: $scope.FLastName,
            SLastName: $scope.SLastName,
            Nick: $scope.Nick,
            Password: $scope.Password,
            BirthDate: $scope.BirthDate
            //DepartmentID: $scope.DepartmentID
        };
        UserData = JSON.stringify(UserData);
        if ($scope.Id > 0) {
            $http({
                method: 'Put',
                url: 'api/OtherUser/' + $scope.Id,
                data: UserData,
                config
            }).then(function (response) {
                $location.path('/UserList');
            }, function (error) {
                console.log(error.data);
                $scope.error = "Something wrong when adding updating employee " + data.ExceptionMessage;
            });
        }
    };


    //Delete the selected employee from the list
    $scope.Delete = function () {
        if ($scope.Id > 0) {
            $http({
                method: 'Delete',
                url: 'api/OtherUser/' + $scope.Id
            }).then(function (response) {
                $location.path('/UserList');
            }, function (error) {
                console.log(error.data);
                $scope.error = "Something wrong when adding Deleting employee " + data.ExceptionMessage;
            });
        }

    };
}]);