using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeShoppingApp.Web.Models
{
    public class RecipeViewModelValidator : AbstractValidator<RecipeViewModel>
    {
        public RecipeViewModelValidator()
        {
            RuleFor(r => r.RecipeName).NotEmpty().WithMessage("Invalid Recipe Name");
            RuleFor(r => r.Serving).NotEmpty().WithMessage("Invalid Servings");
        }
    }
}
