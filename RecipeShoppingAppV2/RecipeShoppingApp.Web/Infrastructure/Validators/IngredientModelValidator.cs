using FluentValidation;
using RecipeShoppingApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeShoppingApp.Web.Infrastructure.Validators
{
    public class IngredientModelValidator : AbstractValidator<IngredientViewModel>
    {
        public IngredientModelValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name required");
            RuleFor(i => i.Serving).NotEmpty().WithMessage("Serving required");
        }
    }
}