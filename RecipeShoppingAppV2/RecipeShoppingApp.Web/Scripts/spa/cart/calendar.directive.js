(function (app) {
    'use strict';

    app.directive('calendar', calendar);

    function calendar() {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/scripts/spa/cart/calendar.html'
        }
    }

})(angular.module('common.ui'));