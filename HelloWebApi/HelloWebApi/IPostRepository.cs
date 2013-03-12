using System;
using System.Collections.Generic;
using System.Linq;
using HelloWebApi.Models;

namespace HelloWebApi
{
    public interface IPostRepository
    {
        IQueryable<Post> GetAll();
        Post Get(int id);
        void Create(Post post);
        void Update(Post post);
        void Delete(int id);
        IQueryable<Post> Search(int year, int month, int day);
    }

    public class PostRepository : IPostRepository
    {
        public static List<Post> _posts = new List<Post>();

        public PostRepository()
        {
            if (_posts.Count == 0)
            {
                _posts.Add(new Post { Id = 1, Date = new DateTime(2010, 04, 09), Title = "An introduction to the web api", Body = "..."});
                _posts.Add(new Post { Id = 2, Date = new DateTime(2010, 04, 11), Title = "REST is BEST", Body = "..." });
                _posts.Add(new Post { Id = 3, Date = new DateTime(2010, 11, 28), Title = "Build your web app", Body = "..." });
            }
        }

        public IQueryable<Post> GetAll()
        {
            return _posts.AsQueryable();
        }

        public Post Get(int id)
        {
            return _posts.Single(p => p.Id == id);
        }

        public void Create(Post post)
        {
            post.Id = _posts.Count() + 1;
            _posts.Add(post);
        }

        public void Update(Post post)
        {
            Post oldPost = _posts.Single(p => p.Id == post.Id);
            oldPost.Title = post.Title;
            oldPost.Date = post.Date;
            oldPost.Body = post.Body;
        }

        public void Delete(int id)
        {
            Post post = _posts.Single(p => p.Id == id);
            _posts.Remove(post);
        }

        public IQueryable<Post> Search(int year, int month, int day)
        {
            return _posts.Where(p => (p.Date.Year == year) && (month == 0 || p.Date.Month == month) && (day == 0 || p.Date.Day == day))
                    .AsQueryable();
        }
    }
}