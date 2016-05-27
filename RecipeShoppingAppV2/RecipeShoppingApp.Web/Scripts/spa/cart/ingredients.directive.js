
(function (app) {
    'use strict';

    app.directive('ingredients', ingredients);

    function ingredients() {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/scripts/spa/cart/ingredients.html'
        }
    }

})(angular.module('common.ui'));
