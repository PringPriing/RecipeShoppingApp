using AutoMapper;
using RecipeShoppingApp.Entities;
using RecipeShoppingApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeShoppingApp.Web.Mappings
{
    class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {

            Mapper.CreateMap<ShoppingHeader, ShoppingHeaderViewModel>();
            Mapper.CreateMap<Recipe, RecipeViewModel>();
            Mapper.CreateMap<Measurement, MeasurementViewModel>();
            Mapper.CreateMap<Ingredient, IngredientViewModel>()
                .ForMember(vm => vm.MeasurementName, map => map.MapFrom(m => m.measurement.Name))
                .ForMember(vm => vm.RecipeServing, map => map.MapFrom(m => m.recipe.Serving));
        }
    }
}
