using System.Linq;
using System.Web.Http;
using HelloWebApi.Models;

namespace HelloWebApi.Controllers
{
    public class AuthorsController :  ApiController
    {
        private readonly IAuthorRepository _repository;

        public AuthorsController(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public AuthorsController()
            : this(new AuthorRepository())
        {
        }
        // ...
        
      
        public IQueryable<Author> Get()
        {
            return _repository.GetAll();
        }

        public Author Get(int id)
        {
            return _repository.Get(id);
        }
    
    }
}