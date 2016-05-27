(function (app) {
    'use strict';

    app.controller('recipeAddCtrl', recipeAddCtrl);

    recipeAddCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService', 'fileUploadService'];

    function recipeAddCtrl($scope, $location, $routeParams, apiService, notificationService, fileUploadService) {

        $scope.pageClass = 'page-recipes';
        $scope.recipe = {Rating:1, Serving : 1, DateCreated: new Date() };

        $scope.isReadOnly = false;
        $scope.prepareFiles = prepareFiles;
        $scope.AddRecipe = AddRecipe;
        $scope.changeNumberOfStocks = changeNumberOfStocks;
        var Image = null;

        $scope.myPopover = {
            isOpen: false,
            container: 'body',
            templateUrl: '/scripts/spa/cart/addRecipe.html',
            open: function open() {
                $scope.myPopover.isOpen = true;
            },
            close: function close() {
                $scope.myPopover.isOpen = false;
            }
        };

        function prepareFiles($files){
            Image = $files
        }


        function AddRecipe() {
            AddRecipeModel();
        }

        function AddRecipeModel() {
            apiService.post('/api/recipe/add', $scope.recipe,
            addRecipeSucceded,
            addRecipeFailed);
        }


        function addRecipeSucceded(response) {
            notificationService.displaySuccess($scope.recipe.RecipeName + ' has been submitted');
            $scope.recipe = response.data;
            if (Image) {
                fileUploadService.uploadImage(Image, $scope.recipe.ID, redirectToEdit);
            }
            else
                redirectToEdit();
        }

        function addRecipeFailed(response) {
            console.log(respnse.data);
            console.log(response);
            notificationService.displayError(response.statusText);
        }

        function redirectToEdit() {
            $location.url('cart/');
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
            console.log($scope.recepi);
        }

    }

})(angular.module('RecipeApp'));