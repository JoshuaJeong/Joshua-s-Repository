﻿using System.Web;
using System.Web.Mvc;

namespace xave.web.code.svc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
