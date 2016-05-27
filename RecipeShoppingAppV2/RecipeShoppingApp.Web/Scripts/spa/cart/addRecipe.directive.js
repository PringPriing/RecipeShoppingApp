(function (app) {
	'use strict';

	app.directive('addrecipe', addrecipe);
	function addrecipe() {
		return {
		    restrict: 'E',
		    transclude: true,
			replace: true,
			scope: {},
            controller: ['$scope', '$location', '$routeParams', 'apiService', 'notificationService', 'fileUploadService',
                        function ($scope, $location, $routeParams, apiService, notificationService, fileUploadService) {

                            $scope.pageClass = 'page-recipes';
                            $scope.recipe = {Rating: 3, Serving: 1, DateCreated: new Date() };

                            $scope.isReadOnly = false;
                            $scope.prepareFiles = prepareFiles;
                            $scope.AddRecipe = AddRecipe;
                            $scope.changeNumberOfStocks = changeNumberOfStocks;
                            var Image = null;

                            function prepareFiles($files) {
                                Image = $files
                            }
                            function AddRecipe() {
                                //AddRecipeModel();
                                notificationService.displaySuccess('test'); 3
                                alert('x');
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
                                $location.url('recipe/edit/' + $scope.recipe.ID);
                            }

                            function changeNumberOfStocks($vent) {
                                var btn = $('#btnSetServing'),
                                oldValue = $('#inputServing').val().trim(),
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
                                $('#inputServing').val(newVal);
                                $scope.recipe.Serving = newVal;
                                console.log($scope.recepi);

                                

                            }
                        }],
            templateUrl: '/scripts/spa/cart/addRecipe.html'

		}
	}

})(angular.module('RecipeApp'));