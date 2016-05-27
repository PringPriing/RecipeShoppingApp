using RecipeShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShoppingApp.Data.Configurations
{
   public class MeasurementConfiguration: EntityBaseConfiguration<Measurement>
    {
       public MeasurementConfiguration()
       {
           Property(m => m.Name).IsRequired();
       }
    }
}
 