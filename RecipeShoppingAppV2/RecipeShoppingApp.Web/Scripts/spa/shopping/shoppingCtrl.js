(function (app) {
    'use strict';

    app.controller('shoppingCtrl', shoppingCtrl);

    shoppingCtrl.$inject = ['$scope', '$modalInstance', '$location', 'apiService', 'notificationService'];

    function shoppingCtrl($scope, $modalInstance, $location, apiService, notificationService) {
        $scope.close = close;
        $scope.ShoppingList = [];
        $scope.Export = Export;

        function LoadShoppingList(){

            $.each($scope.IngredientIDs, function (key,value) {
                GetIngredientsData(value);
            });

        }

        function GetIngredientsData(recipeID) {
            apiService.get('/api/ingredient/ingredients/' + recipeID,
                                 null,
                                 GetIngredientsDataSucceded,
                                 LoadIngredientsFailed);
        }

        function GetIngredientsDataSucceded(result) {
            Array.prototype.push.apply($scope.ShoppingList, result.data);
        }

        function LoadIngredientsFailed(response) {
            notificationService.displayerror(response.statustext);
        }

        function Export() {
            apiService.post('/api/shoppingHeader/export/',
                            $scope.ShoppingList,
                            ExportSucceded,
                            ExportFailed)

           
        }
        function ExportSucceded() {
            notificationService.displaysuccess('Exported');
        }

        function ExportFailed(response) {
            notificationService.displayerror(response.statustext);
        }

        function close() {
            $modalInstance.dismiss();
        }

        LoadShoppingList();
    }

})(angular.module('RecipeApp'));