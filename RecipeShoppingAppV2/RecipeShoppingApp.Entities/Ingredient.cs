using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShoppingApp.Entities
{
    public class Ingredient : IEntityBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Serving { get; set; }
        public int MeasurementID { get; set; }
        public int RecipeID { get; set; }
        public virtual Measurement measurement { get; set; }
        public virtual Recipe recipe { get; set; }
    
    }
}
