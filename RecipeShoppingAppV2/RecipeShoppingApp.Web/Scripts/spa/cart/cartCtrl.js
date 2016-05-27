(function (app) {
    'use strict';

    app.controller('cartCtrl', cartCtrl);

    cartCtrl.$inject = ['$scope', '$filter','$modal', 'apiService', 'notificationService'];

    function cartCtrl($scope,$filter,$modal,apiService, notificationService) {
        $scope.pageClass = 'page-recipes';
        $scope.loadingRecipe = false;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Name = '';
        $scope.Date = new Date('2016-05-01');
        $scope.ID = '';
        $scope.ingredientID = '';
        $scope.ingredients = [];
        $scope.IngredientIDs = new Array();
        $scope.recipes = [];
        $scope.PlotDetails = {};
        $scope.RecipeName = '';
        $scope.Sun = [];
        $scope.Mon = [];
        $scope.Tue = [];
        $scope.Wed = [];
        $scope.Thu = [];
        $scope.Fri = [];
        $scope.Sat = [];
        $scope.recipe = { Rating: 3, Serving: 1, DateCreated: new Date() };
        $scope.Ingredient = { Serving: 1 };
        $scope.ShoppingHeader = {Date:new Date()};
        $scope.measurements = [];
        $scope.LoadIngredientDetails = LoadIngredientDetails;
        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.clearSearchIngredient = clearSearchIngredient;
        $scope.AddIngredient = AddIngredient;
        $scope.changeNumberOfStocks = changeNumberOfStocks;
        $scope.canSave = true;
        $scope.AddShoppingHeader = AddShoppingHeader;
        $scope.openShoppingDialog = openShoppingDialog;
        $scope.openRecipeDetailsModal = openRecipeDetailsModal;
        $scope.openDatePicker = openDatePicker;
        $scope.changeDateEvent = changeDateEvent;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 0

        };
        $scope.datepicker = {};
        $scope.clearElements = clearElements;
        $scope.disabled = function (date, mode) {
            return (mode === 'day' && (date.getDay() != 0));
        };
        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.datepicker.opened = true;
        };


        function generateShoppingList()
        {
            var sunElement = getElements('sundayPanel');
            var monElement = getElements('mondayPanel');
            var tueElement = getElements('tuesdayPanel');
            var wedElement = getElements('wednesdayPanel');
            var thuElement = getElements('thursdayPanel');
            var friElement = getElements('fridayPanel');
            var satElement = getElements('saturdayPanel');
            $scope.ShoppingHeader = {
                                        Date: $scope.Date,
                                        Sun: sunElement,
                                        Mon: monElement,
                                        Tue: tueElement,
                                        Wed: wedElement,
                                        Thu: thuElement,
                                        Fri: friElement,
                                        Sat:satElement
            };

            var id = [sunElement, monElement, tueElement, wedElement, thuElement, friElement, satElement];
            var arr = id.join().split(',');
           

            $scope.IngredientIDs = [];

            for(var i =0 ; i<arr.length; i++){
                if (arr[i] != '') {
                    $scope.IngredientIDs.push(arr[i]);
                }
                
            }
        }
        function clearDraggedElements(parentPanel)
        {
            var count = $("#" + parentPanel).children().length;
            var i = 0;
            for (i = 0; i < count; i++) {
                var top = document.getElementById(parentPanel);
                var nested = document.getElementById(parentPanel).querySelector(".recipe");
                if (nested != null)
                {
                    top.removeChild(nested);
                }
                    
               
            }
        }
        function clearElements(day)
        {
            switch(day)
            {
                case 'sunday':
                    $scope.Sun = [];
                    clearDraggedElements("sundayPanel");
                    break;
                case 'monday':
                    $scope.Mon = [];
                    clearDraggedElements("mondayPanel");
                    break;
                case 'tuesday':
                    $scope.Tue = [];
                    clearDraggedElements("tuesdayPanel");
                    break;
                case 'wednesday':
                    $scope.Wed = [];
                    clearDraggedElements("wednesdayPanel");
                    break;
                case 'thursday':
                    $scope.Thu = [];
                    clearDraggedElements("thursdayPanel");
                    break;
                case 'friday':
                    $scope.Fri = [];
                    clearDraggedElements("fridayPanel");
                    break;
                case 'saturday':
                    $scope.Sat = [];
                    clearDraggedElements("saturdayPanel");
                    break;
            }
           
        }

        function clearAllElements()
        {
           
            clearDraggedElements("sundayPanel");
            clearDraggedElements("mondayPanel");
            clearDraggedElements("tuesdayPanel");
            clearDraggedElements("wednesdayPanel");
            clearDraggedElements("thursdayPanel");
            clearDraggedElements("fridayPanel");
            clearDraggedElements("saturdayPanel");
        }

        function getElements(id)
        {
            var values;
            var children = [].slice.call(document.getElementById(id).getElementsByTagName('*'), 0);

            var elements = new Array(children.length);
            var arrayLength = children.length;
            for (var i = 0; i < arrayLength; i++) {
                var name = children[i].getAttribute("data-value") || children[i].getAttribute("class");
                elements[i] = name;
            }

            return values = elements.join(',');
        }

        function changeDateEvent() {
            $scope.Sun = [];
            $scope.Mon = [];
            $scope.Tue = [];
            $scope.Wed = [];
            $scope.Thu = [];
            $scope.Fri = [];
            $scope.Sat = [];
            LoadShoppingList();
            clearAllElements();
        }

        function LoadShoppingList() {
            console.log($filter('date')($scope.Date, 'yyyy-MM-dd'));
            apiService.get('/api/shoppingHeader/shoppingList/' + $filter('date')($scope.Date, 'yyyy-MM-dd'),
                            null,
                            LoadShoppingListSucceded,
                            LoadShoppingListFailed)
        }

        function LoadShoppingListSucceded(result) {
            if (result.data == null) {
                notificationService.displayInfo("No recipe plotted for this week");
            }
            else {
                $scope.PlotDetails = result.data;
                console.log('shopping list ' + $scope.PlotDetails.ID);
                LoadPlottedRecipe();
                notificationService.displaySuccess("Recipes  plotted for " + $scope.Date);

                }
        }

   
        function LoadPlottedRecipe() {
            var SunArr = $scope.PlotDetails.Sun.split(',');
            var MonArr = $scope.PlotDetails.Mon.split(',');
            var TueArr = $scope.PlotDetails.Tue.split(',');
            var WedArr = $scope.PlotDetails.Wed.split(',');
            var ThuArr = $scope.PlotDetails.Thu.split(',');
            var FriArr = $scope.PlotDetails.Fri.split(',');
            var SatArr = $scope.PlotDetails.Sat.split(',');

            GetRecipeValues(SunArr,'sunday');
            GetRecipeValues(MonArr, 'monday');
            GetRecipeValues(TueArr, 'tuesday');
            GetRecipeValues(WedArr, 'wednesday');
            GetRecipeValues(ThuArr, 'thursday');
            GetRecipeValues(FriArr, 'friday');
            GetRecipeValues(SatArr, 'saturday');
            
        }

        function GetRecipeValues(array,day)
        {
             angular.forEach(array, function (value) {
                        GetRecipeName(value,day);
                    });
       
        }
   
        //=== GET RECIPE     === GET RECIPE     === GET RECIPE 
        function GetRecipeName(id, day) {
            switch (day) {
                case 'sunday':
                    apiService.get('/api/recipe/details/' + id, null,
                               getRecipeSuccededSun,
                               getRecipeFailed);
                    break;
                case 'monday':
                    apiService.get('/api/recipe/details/' + id, null,
                               getRecipeSuccededMon,
                               getRecipeFailed);
                    break;
                case 'tuesday':
                    apiService.get('/api/recipe/details/' + id, null,
                               getRecipeSuccededTue,
                               getRecipeFailed);
                    break;
                case 'wednesday':
                    apiService.get('/api/recipe/details/' + id, null,
                               getRecipeSuccededWed,
                               getRecipeFailed);
                    break;
                case 'thursday':
                    apiService.get('/api/recipe/details/' + id, null,
                               getRecipeSuccededThu,
                               getRecipeFailed);
                    break;
                case 'friday':
                    apiService.get('/api/recipe/details/' + id, null,
                               getRecipeSuccededFri,
                               getRecipeFailed);
                    break;
                case 'saturday':
                    apiService.get('/api/recipe/details/' + id, null,
                               getRecipeSuccededSat,
                               getRecipeFailed);
                    break;

            }
           
        }
        function getRecipeSuccededSun(result) {
            $scope.Sun.push({ id: result.data.ID, recipename: result.data.RecipeName });
            $scope.recipe = result.data;
        }
        function getRecipeSuccededMon(result) {
            $scope.Mon.push({ id: result.data.ID, recipename: result.data.RecipeName });
            $scope.recipe = result.data;
        }
        function getRecipeSuccededTue(result) {
            $scope.Tue.push({ id: result.data.ID, recipename: result.data.RecipeName });
            $scope.recipe = result.data;
        }
        function getRecipeSuccededWed(result) {
            $scope.Wed.push({ id: result.data.ID, recipename: result.data.RecipeName });
            $scope.recipe = result.data;
        }
        function getRecipeSuccededThu(result) {
            $scope.Thu.push({ id: result.data.ID, recipename: result.data.RecipeName });
            $scope.recipe = result.data;
        }
        function getRecipeSuccededFri(result) {
            $scope.Fri.push({ id: result.data.ID, recipename: result.data.RecipeName });
            $scope.recipe = result.data;
        }
        function getRecipeSuccededSat(result) {
            $scope.Sat.push({ id: result.data.ID, recipename: result.data.RecipeName });
            $scope.recipe = result.data;
        }
        function getRecipeFailed(response){
            //notificationService.displayError(response.statusText);
        }


        function LoadShoppingListFailed(response) {
            notificationService.displayError(response.statusText);
        }

        //ShoppingListModal
        function openShoppingDialog() {
            $modal.open({
                templateUrl: 'scripts/spa/shopping/shoppingListModal.html',
                controller : 'shoppingCtrl',
                scope: $scope
            }).result.then(function ($scope) {
               
            }, function () {
            });
        }
        //ShoppingListModal

        function openRecipeDetailsModal() {
            $modal.open({
                templateUrl: 'scripts/spa/cart/recipeDetailsModal.html',
                controller: 'recipeModalCtrl',
                scope: $scope
            }).result.then(function ($scope) {

            }, function () {
            });
        }
        //shoppingHeader
        function DeleteShoppingHeader() {
            var date = $filter('date')($scope.Date, 'yyyy-MM-dd');
            apiService.post('/api/shoppingHeader/delete/' + date,
                            null,
                            DeleteShoppingHeaderSucceded,
                            DeleteShoppingHeaderFailed)
        }

        function DeleteShoppingHeaderSucceded() {
            notificationService.displayInfo("Header updated");
        }
        function DeleteShoppingHeaderFailed(response){
            notificationService.displayError(response.statusText);
        }

        function AddShoppingHeader() {
            generateShoppingList();
            if (   $scope.ShoppingHeader.Sun == ""
                && $scope.ShoppingHeader.Mon == ""
                && $scope.ShoppingHeader.Tue == ""
                && $scope.ShoppingHeader.Wed == ""
                && $scope.ShoppingHeader.Thu == ""
                && $scope.ShoppingHeader.Fri == ""
                && $scope.ShoppingHeader.Sat == ""
                ) {
                notificationService.displayError("Nothing to generate");
            }
            else {
                openShoppingDialog();
                AddShoppingHeaderModel();
            }


        }
        function AddShoppingHeaderModel() {
            apiService.post('/api/shoppingHeader/add', $scope.ShoppingHeader,
                               AddShoppingSucceded,
                               AddShoppingFailed)
        }

        function AddShoppingSucceded(response) {
            $scope.ShoppingHeader = response.data;
            notificationService.displaySuccess("shoppingList has been submitted");
        }
        function AddShoppingFailed(response) {
            notificationService.displayError(response.statusText);
        }
        //shoppingHeader
        function LoadIngredients() {
            apiService.get('/api/ingredient/ingredients/' + $scope.Ingredient.RecipeID,
                                 null,
                                 LoadIngredientsCompleted,
                                 LoadIngredientsFailed);
        }

        function LoadIngredientsCompleted(response) {
            $scope.ingredients = response.data;
        }

        function LoadIngredientsFailed(response) {
            console.log(response.data);
            notificationService.displayError(response.statusText);
        }

        function AddIngredient() {
            AddIngredientModel();
        }

        function AddIngredientModel()
        {
            apiService.post('/api/ingredient/add', $scope.Ingredient,
                AddIngredientSucceded,
                AddIngredientFailed);
        }

        function AddIngredientSucceded()
        {
            LoadIngredients();
            notificationService.displaySuccess("ingredient has been submitted");
        }

        function AddIngredientFailed()
        {
            notificationService.displayError("Select recipe first");
        }

        function LoadIngredientDetails(name,id)
        {
            $scope.ID= id;
            $scope.Name = name;
            $scope.Ingredient = { RecipeID: id, Serving: 1 };
            $scope.canSave = false;
            LoadIngredients();

        }
        function LoadMeasurements()
        {
            apiService.get('/api/measurement/measurements',
                                null,
                                measurementsLoadCompleted,
                                measurementsLoadFailed);
        }

       

        function measurementsLoadCompleted(response)
        {
            $scope.measurements = response.data;
        }

        function measurementsLoadFailed(response)
        {
            notificationService.displayError(response.statusText);
        }

        function search(page) {
            page = page || 0;

            $scope.loadingRecipe = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 3,
                    filter: $scope.filterRecipes
                }
            };

            apiService.get('/api/recipe/', config,
            recipesLoadCompleted,
            recipesLoadFailed);
        }

        function recipesLoadCompleted(result) {
            $scope.recipes = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingrecipes = false;

            if ($scope.filterRecipes && $scope.filterRecipes.length) {
                notificationService.displayInfo(result.data.Items.length + ' recipe found');
            }

        }

        function recipesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterRecipes = '';
            $scope.filterIngredients = '';
            search();
        }

        function clearSearchIngredient() {
            $scope.filterIngredients = '';
            search();
        }

        function redirectToEdit() {
            $location.url('recipe/edit/98');
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
            $scope.Ingredient.Serving = newVal;
            console.log($scope.recepi);
        }

        $scope.search();
        LoadMeasurements();
       
    }

})(angular.module('RecipeApp'));