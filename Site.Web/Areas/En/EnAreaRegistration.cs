using System.Web.Mvc;

namespace Site.Web.Areas.En
{
    public class EnAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "En";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "En_default",
                "En/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Site.Web.Areas.En.Controllers" }
            );
        }
    }
}
