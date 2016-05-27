
using RecipeShoppingApp.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeShoppingApp.Web.Models
{
    public class IngredientViewModel : IValidatableObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Serving { get; set; }
        public int RecipeServing { get; set; }
        public string MeasurementName { get; set; }
        public int MeasurementId { get; set; }
        public int RecipeId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationcontext)
        {
            var validator = new IngredientModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}