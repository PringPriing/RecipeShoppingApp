using FluentValidation;
using RecipeShoppingApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeShoppingApp.Web.Infrastructure.Validators
{
    public class MeasurementViewModelValidator : AbstractValidator<MeasurementViewModel>
    {
        public MeasurementViewModelValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Name Required");
        }
    }
}