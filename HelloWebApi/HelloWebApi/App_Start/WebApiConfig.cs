using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace HelloWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
              name: "PostsCustomAction",
              routeTemplate: "api/{controller}/{action}/{id}",
              defaults: new { id = RouteParameter.Optional }
          );

            config.Routes.MapHttpRoute(
                name: "PostByDate",
                routeTemplate: "api/{controller}/{year}/{month}/{day}",
                defaults: new { month = RouteParameter.Optional, day = RouteParameter.Optional },
                constraints: new { month = @"\d{0,2}", day = @"\d{0,2}"}
            );

          


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
