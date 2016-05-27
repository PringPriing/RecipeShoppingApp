﻿using RecipeShoppingApp.Web.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RecipeShoppingApp.Web.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
            AutoMapperConfiguration.Configure();
        }
        
    }
}