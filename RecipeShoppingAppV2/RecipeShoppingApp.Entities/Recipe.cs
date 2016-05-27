using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShoppingApp.Entities
{
   public class Recipe : IEntityBase
    {

        public int ID { get; set; }
        public string RecipeName { get; set; }
        public int Serving { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
       
    }
}
