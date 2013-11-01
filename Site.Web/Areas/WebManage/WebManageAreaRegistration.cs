using System.Web.Mvc;

namespace Site.Web.Areas.WebManage
{
    public class WebManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "WebManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "WebManage_default",
                "WebManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Site.Web.Areas.WebManage.Controllers" }
            );
        }
    }
}
