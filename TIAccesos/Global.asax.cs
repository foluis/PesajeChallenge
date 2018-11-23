using System.Web.Mvc;
using System.Web.Routing;

namespace TIAccesos
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Establecer el tipo de Claim que es utilizado para identificar al usuario de forma única.
            System.Web.Helpers.AntiForgeryConfig.UniqueClaimTypeIdentifier = System.Security.Claims.ClaimTypes.NameIdentifier;
        }
    }
}
