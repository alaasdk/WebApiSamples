using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using HelloWebApi.Models;

namespace HelloWebApi.Controllers
{
    public class PostsController : ApiController
    {
        private readonly IPostRepository _repository;

        public PostsController(IPostRepository repository)
        {
            _repository = repository;
        }

        public PostsController(): this(new PostRepository())
        {
        }
        
        public IQueryable<Post> Archive(int year, int month = 0, int day = 0)
        {
            return _repository.Search(year, month, year);
        } 


        public IQueryable<Post> Get()
        {
            return _repository.GetAll();
        }

        public Post Get(int id)
        {
            return _repository.Get(id);
        }

        
        //public HttpResponseMessage Post([ModelBinder(typeof(PostModelBinder))]Post post)
        public HttpResponseMessage Post([ValueProvider(typeof(HeaderValueFactory))]Post post)
        {
            _repository.Create(post);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.StatusCode = HttpStatusCode.Created;
            string uri = Url.Link("DefaultApi", new { id = post.Id });
            response.Headers.Location = new Uri(uri);
               
            return response;
        }

        public HttpResponseMessage Put(int id, Post post)
        {
            post.Id = id;
            _repository.Update(post);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, post);
            string uri = Url.Link("DefaultApi", new { id = post.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            _repository.Delete(id);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NoContent);
            return response;
        }
    }
}