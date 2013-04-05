using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Mvc;

namespace ODataSamples.Controllers
{
    public class PostsController : EntitySetController<Post,int>
    {

        public PostsController() : this(new PostRepository()) {}


        private readonly IPostRepository _repository;
        public PostsController(IPostRepository repository)
        {
            _repository = repository;
        }

        public override IQueryable<Post> Get()
        {
            IEnumerable<Post> posts= _repository.GetPosts();
            return posts.AsQueryable();
        }

    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishingDate { get; set; }
    }

    public interface IPostRepository
    {
        IEnumerable<Post> GetPosts();
    }

    public class PostRepository : IPostRepository
    {
        public IEnumerable<Post> GetPosts()
        {
            IList<Post> posts = new List<Post>();
            posts.Add(new Post { Id = 1, Title = "post1", Author = "ema", PublishingDate = DateTime.Today });
            posts.Add(new Post { Id = 2, Title = "post2", Author = "ale", PublishingDate = DateTime.Today.AddDays(1) });
            posts.Add(new Post { Id = 3, Title = "post3", Author = "vale", PublishingDate = DateTime.Today.AddDays(-2) });
            posts.Add(new Post { Id = 4, Title = "post4", Author = "lory", PublishingDate = DateTime.Today.AddDays(1) });
            posts.Add(new Post { Id = 5, Title = "post5", Author = "tessa", PublishingDate = DateTime.Today });
            return posts;
        }
    }
}
