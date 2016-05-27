using FluentValidation;
using RecipeShoppingApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeShoppingApp.Web.Infrastructure.Validators
{
    public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationViewModelValidator()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress()
                .WithMessage("Invalid email address");

            RuleFor(r => r.UserName).NotEmpty()
                .WithMessage("Invalid Username");

            RuleFor(r => r.Password).NotEmpty()
                .WithMessage("Invalid Password");
        }
    }
}