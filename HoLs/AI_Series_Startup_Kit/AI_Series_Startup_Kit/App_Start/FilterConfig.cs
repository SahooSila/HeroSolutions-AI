﻿using System.Web;
using System.Web.Mvc;

namespace AI_Series_Startup_Kit
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
