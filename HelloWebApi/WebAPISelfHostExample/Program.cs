using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using HelloWebApi.Controllers;

namespace WebAPISelfHostExample
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    //ValuesController controller = new ValuesController();
        //    var config = new HttpSelfHostConfiguration("http://localhost:3000");
        //    config.Routes.MapHttpRoute("default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        //    var server = new HttpSelfHostServer(config);
        //    server.OpenAsync().Wait();
        //    Console.ReadLine();
        //}

        public static void Main()
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        
            HttpServer server = new HttpServer(config);
            HttpClient client = new HttpClient(server);
            HttpResponseMessage response = client.GetAsync("http://localhost/api/posts").Result;
            String content = response.Content.ReadAsStringAsync().Result;
            
        }
    }
}
