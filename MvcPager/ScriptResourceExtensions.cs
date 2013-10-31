using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;

namespace MvcPager
{
    public static class ScriptResourceExtensions
    {
        public static void RegisterMvcPagerScriptResource(this HtmlHelper html)
        {
            var page = html.ViewContext.HttpContext.CurrentHandler as Page;
            var scriptUrl = (page ?? new Page()).ClientScript.GetWebResourceUrl(typeof(PagerHelper), "MvcPager.MvcPager.min.js");
            //string url = @"/Content/js/MvcPager.js";
            html.ViewContext.Writer.Write("<script type=\"text/javascript\" src=\"" + scriptUrl + "\"></script>");
        }
    }
}
