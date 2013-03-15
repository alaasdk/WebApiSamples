using System.Collections.Generic;
using System.Linq;
using HelloWebApi.Models;

namespace HelloWebApi
{
    public interface IAuthorRepository
    {
        IQueryable<Author> GetAll();
        Author Get(int id);
    }

    class AuthorRepository : IAuthorRepository
    {
        public static List<Author> _authors = new List<Author>();

        public AuthorRepository()
        {
            if (_authors.Count == 0)
            {
                _authors.Add(new Author { Id = 1, Name = "Tolkien", PhotoUrl = "/api/authors/photos/tolkien.png"});
                _authors.Add(new Author { Id = 2, Name = "Dick", PhotoUrl = "/api/authors/photos/dick.png" });
                _authors.Add(new Author { Id = 3, Name = "Clark", PhotoUrl = "/api/authors/photos/clark.png" });
            }
        }
        public IQueryable<Author> GetAll()
        {
            return _authors.AsQueryable();
        }

        public Author Get(int id)
        {
            return _authors.Single(a => a.Id == id);
        }
    }
}