using RecipeShoppingApp.Entities;
using RecipeShoppingApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeShoppingApp.Web.Infrastructure.Extensions
{
    public static class EntitiesExtensions
    {
        public static void UpdateRicepe(this Recipe recipe, RecipeViewModel recipeVM)
        {
            recipe.RecipeName = recipeVM.RecipeName;
            recipe.Serving = recipeVM.Serving;
            recipe.Description = recipeVM.Description;
            recipe.DateCreated = recipeVM.DateCreated;
            recipe.Rating = recipeVM.Rating;
        }

        public static void UpdateMeasurement(this Measurement measurement, MeasurementViewModel measurementVM)
        {
            measurement.ID = measurementVM.ID;
            measurement.Name = measurementVM.Name;
        }

        public static void UpdateIngredient(this Ingredient ingredient, IngredientViewModel ingredientsVM)
        {
            ingredient.Name = ingredientsVM.Name;
            ingredient.ID = ingredientsVM.ID;
            ingredient.MeasurementID = ingredientsVM.MeasurementId;
            ingredient.Serving = ingredientsVM.Serving;
            ingredient.RecipeID = ingredientsVM.RecipeId;
            
           
        }

        public static void UpdateShoppingHeader(this ShoppingHeader header, ShoppingHeaderViewModel headerVM)
        {
            header.ID = headerVM.ID;
            header.Date = headerVM.Date;
            header.Sun = headerVM.Sun;
            header.Mon = headerVM.Mon;
            header.Tue = headerVM.Tue;
            header.Wed = headerVM.Wed;
            header.Thu = headerVM.Thu;
            header.Fri = headerVM.Fri;
            header.Sat = headerVM.Sat;

        }
    }
}