var app = angular.module("myApp", ["ngRoute"]);
app.config(['$locationProvider', '$routeProvider', function ($locationProvider, $routeProvider) {
    $routeProvider.when('/', { //Routing for show list of employee
        templateUrl: '/UserList.html',
        controller: 'UserController'
    }).when('/AddUser', { //Routing for add employee
        templateUrl: '/AddUser.html',
        controller: 'UserController'
        }).when('/UserList', { //Routing for add employee
            templateUrl: '/UserList.html',
            controller: 'UserController'
        })
        .when('/EditUser/:empId', { //Routing for geting single employee details
            templateUrl: '/UpdateUser.html',
            controller: 'UserController'
        })
        .when('/DeleteUser/:empId', { //Routing for delete employee
            templateUrl: '/DeleteUser.html',
            controller: 'UserController'
        })
        .otherwise({ //Default Routing
            controller: 'UserController'
        });
}]);
