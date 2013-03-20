using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using HelloWebApi.Models;

namespace HelloWebApi
{
    public class PostModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            Post model = new Post();
            string key = bindingContext.ModelName;
            ValueProviderResult val = bindingContext.ValueProvider.GetValue(key);
            
            bindingContext.Model = model;

            return true;
        }
    }
}