﻿using System.Web;
using System.Web.Mvc;

namespace Xave.CdaGenerator.SVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
