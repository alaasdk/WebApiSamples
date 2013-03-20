using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Routing;
using HelloWebApi.Controllers;
using HelloWebApi.Formatters;

namespace HelloWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Add(new ImageFormatter()); 

            //config.DependencyResolver = new CustomControllerResolver();
            config.Routes.MapHttpRoute(
                name: "Archive",
                routeTemplate: "api/posts/archive/{year}/{month}/{day}",
                defaults: new { controller = "Posts", month = RouteParameter.Optional, day = RouteParameter.Optional },
                constraints: new { month = @"\d{0,2}", day = @"\d{0,2}"}
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }

    //public class CustomControllerResolver : IDependencyResolver
    //{
    //    public void Dispose()
    //    {
            
    //    }

    //    public object GetService(Type serviceType)
    //    {
    //        if (serviceType == typeof(PostsController))
    //            return new PostsController(new PostRepository());
    //    }

    //    public IEnumerable<object> GetServices(Type serviceType)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IDependencyScope BeginScope()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
