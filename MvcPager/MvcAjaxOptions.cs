﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcPager
{
    public class MvcAjaxOptions:System.Web.Mvc.Ajax.AjaxOptions
    {
        public bool EnablePartialLoading { get; set; }
        public string DataFormId { get; set; }
    }
}
