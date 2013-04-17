using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using HelloWebApi.Controllers;

namespace HelloWebApi
{
    public class SimpleResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(PostsController))
            {
                return new PostsController(new PostRepository());
            }
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public void Dispose()
        {
            
        }
        
        public IDependencyScope BeginScope()
        {
            return this;
        }
    }
}