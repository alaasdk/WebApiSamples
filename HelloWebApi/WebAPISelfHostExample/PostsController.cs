using System.Web.Http;

namespace WebAPISelfHostExample
{
    public class PostsController : ApiController
    {
        public string[] Get()
        {
            return new[]{"Wow", "this", "is", "a", "real", "web", "application"};
        } 

    }
}