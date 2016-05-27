using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShoppingApp.Entities
{
    public class Measurement : IEntityBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
