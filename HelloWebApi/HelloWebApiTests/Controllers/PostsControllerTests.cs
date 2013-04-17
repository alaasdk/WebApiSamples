using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using HelloWebApi;
using HelloWebApi.Controllers;
using HelloWebApi.Models;
using Moq;
using Xunit;

namespace HelloWebApiTests.Controllers
{
    public class PostsControllerTests
    {
        [Fact]
        public void GetById_should_return_the_post()
        {
            Mock<IPostRepository> repository = new Mock<IPostRepository>();
            PostsController controller = new PostsController(repository.Object);

            controller.Get(42);
            
            repository.Verify(r => r.Get(42));
        }

        [Fact]
        public void Get_should_return_all_the_posts()
        {
            Mock<IPostRepository> repository = new Mock<IPostRepository>();
            List<Post> valueFunction = new List<Post> {new Post(), new Post()};
            repository.Setup(r => r.GetAll()).Returns(valueFunction.AsQueryable());
            
            PostsController controller = new PostsController(repository.Object);

            IQueryable<Post> posts = controller.Get();

            Assert.Equal(2, posts.Count());
        }

        [Fact]
        public void Post_HttpStatus_should_be_created_and_header_should_contains_the_location()
        {
            Mock<IPostRepository> repository = new Mock<IPostRepository>();
            
            PostsController controller = new PostsController(repository.Object);

            HttpConfiguration config = new HttpConfiguration();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/");
            IHttpRoute route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            HttpRouteData routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "posts" } });
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;

            HttpResponseMessage response = controller.Post(new Post()
                                                               {
                                                                   Title = "test", 
                                                                   Date = DateTime.Today, 
                                                                   Body = "blablabla"
                                                               });

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response.Headers.Location);
            
        }

        [Fact]
        public void Get_with_in_memory_hosting()
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            HttpServer server = new HttpServer(config);
            PostsController controller = new PostsController(null);
            
            HttpClient client = new HttpClient(server);
            HttpResponseMessage response = client.GetAsync("http://localhost/api/posts").Result;

            String content = response.Content.ReadAsStringAsync().Result;
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
        }

        [Fact]
        public void Post_with_in_memory_hosting()
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            HttpServer server = new HttpServer(config);
            PostsController controller = new PostsController(null);

            HttpClient client = new HttpClient(server);
            HashSet<KeyValuePair<string, string>> values = new HashSet<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("Title", "test"));
            values.Add(new KeyValuePair<string, string>("Date", "2010-04-11"));
            values.Add(new KeyValuePair<string, string>("Body", "blablabla"));
            HttpResponseMessage response = client.PostAsync("http://localhost/api/posts", new FormUrlEncodedContent(values)).Result;

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response.Headers.Location);

        }

         
    }
}