using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeShoppingApp.Web.Models
{
    public class ShoppingHeaderViewModel 
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Sun { get; set; }
        public string Mon { get; set; }
        public string Tue { get; set; }
        public string Wed { get; set; }
        public string Thu { get; set; }
        public string Fri { get; set; }
        public string Sat { get; set; }
    }
}