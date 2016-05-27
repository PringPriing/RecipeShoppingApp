(function (app) {
    'use strict';

    app.controller('recipeEditCtrl', recipeEditCtrl);

    recipeEditCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService', ];

    function recipeEditCtrl($scope, $location, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-recipes';
        $scope.recipe = {Rating:1};
        $scope.loadingRecipe = true;
        $scope.isReadOnly = false;
        $scope.UpdateRecipe = UpdateRecipe;
        $scope.DeleteRecipe = DeleteRecipe;
        $scope.changeNumberOfStocks = changeNumberOfStocks;

        function loadRecipe() {

            $scope.loadingRecipe = true;

            apiService.get('/api/recipe/details/' + $routeParams.id, null,
            recipeLoadCompleted,
            recipeLoadFailed);
        }

        function recipeLoadCompleted(result) {
            console.log(result.data);
            $scope.recipe = result.data;
            $scope.loadingRecipe = false;
        }

        function recipeLoadFailed(response) {
            notificationService.displayError(response.data);
        }


        function UpdateRecipe() {
            UpdateRecipeModel();
            console.log($scope.recipe);
        }

        function DeleteRecipe() {
            DeleteRecipeModel();
        }

        function DeleteRecipeModel() {
            apiService.post('/api/recipe/delete/' + $scope.recipe.ID,
                null,
                DeleteRecipeSucceded,
                DeleteRecipeFailed)
        }

        function DeleteRecipeSucceded(){
            notificationService.displaySuccess('Recipe Deleted');
            redirectToCart();
        }
        function DeleteRecipeFailed(response) {
            notificationService.displayError(response.statustext);
        }

        function UpdateRecipeModel() {
            apiService.post('/api/recipe/update', $scope.recipe,
            updateRecipeSucceded,
            updateRecipeFailed);
        }


        function updateRecipeSucceded(response) {
            console.log(response);
            notificationService.displaySuccess($scope.recipe.RecipeName + ' has been updated');
            $scope.recipe = response.data;
        }

        function updateRecipeFailed(response) {
            notificationService.displayError(response);
        }

        function changeNumberOfStocks($vent) {
            var btn = $('#btnSetStocks'),
            oldValue = $('#inputStocks').val().trim(),
            newVal = 0;

            if (btn.attr('data-dir') == 'up') {
                newVal = parseInt(oldValue) + 1;
            } else {
                if (oldValue > 1) {
                    newVal = parseInt(oldValue) - 1;
                } else {
                    newVal = 1;
                }
            }
            $('#inputStocks').val(newVal);
            $scope.recipe.Serving = newVal;
            console.log($scope.recipe);
        }

        function redirectToCart() {
            $location.url('/cart');
        }


        loadRecipe();
    }

})(angular.module('RecipeApp'));