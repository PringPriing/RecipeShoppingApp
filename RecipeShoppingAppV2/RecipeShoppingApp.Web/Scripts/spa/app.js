(function () {
    'use strict';

    angular.module('RecipeApp', ['common.core', 'common.ui'])
        .config(config)
        .run(run);

    config.$inject = ['$routeProvider'];
    function config($routeProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "scripts/spa/home/index.html",
                //controller: "indexCtrl"
            })
            .when("/error401", {
                 templateUrl: "scripts/spa/UnAuthorized/401.html",
                 //controller: "indexCtrl"
             })
            .when("/login", {
                templateUrl: "scripts/spa/account/login.html",
                controller: "loginCtrl"
            })
            .when("/addRecipe", {
                templateUrl: "scripts/spa/recipe/add.html",
                controller: "recipeAddCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/cart", {
                  templateUrl: "scripts/spa/cart/cart.html",
                  controller: "cartCtrl",
                  resolve: { isAuthenticated: isAuthenticated }
            }).when("/measurement", {
                templateUrl: "scripts/spa/measurement/measurement.html",
                controller: "measurementCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/recipe/edit/:id", {
                 templateUrl: "scripts/spa/recipe/update.html",
                 controller: "recipeEditCtrl",
                 resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/register", {
                templateUrl: "scripts/spa/account/register.html",
                controller: "registerCtrl"
            }).otherwise({ redirectTo: "#/cart" });
    }

    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];

    function run($rootScope, $location, $cookieStore, $http) {
        // handle page refreshes
        $rootScope.repository = $cookieStore.get('repository') || {};
        if ($rootScope.repository.loggedUser) {
            $http.defaults.headers.common['Authorization'] = $rootScope.repository.loggedUser.authdata;
        }

        $(document).ready(function () {
            $(".fancybox").fancybox({
                openEffect: 'none',
                closeEffect: 'none'
            });

            $('.fancybox-media').fancybox({
                openEffect: 'none',
                closeEffect: 'none',
                helpers: {
                    media: {}
                }
            });

            $('[data-toggle=offcanvas]').click(function () {
                $('.row-offcanvas').toggleClass('active');
            });
        });
    }

    isAuthenticated.$inject = ['membershipService', '$rootScope', '$location'];

    function isAuthenticated(membershipService, $rootScope, $location) {
        if (!membershipService.isUserLoggedIn()) {
            $rootScope.previousState = $location.path();
            $location.path('/login');
        }
    }

})();