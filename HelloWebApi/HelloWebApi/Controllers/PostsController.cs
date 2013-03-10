using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HelloWebApi.Controllers
{
    public class PostsController : ApiController
    {
        public IQueryable<Post> Get(int year, int month = 0, int day = 0)
        {
            Post p = new Post {Title = "First post:"  + year + month + day};
            return new List<Post>(){p}.AsQueryable();

            // other code omitted ...
        }

        [HttpGet]
        public string Category(int id)
        {
            return "Category" + id;
        }
    }

    public class Post
    {
        public string Title { get; set; }
    }
}