using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using System.Web.Providers.Entities;
using HelloWebApi.Models;

namespace HelloWebApi
{
    public class HeaderValueFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(HttpActionContext actionContext)
        {
            return new HeaderValueProvider(actionContext.Request.Headers);
        }
    }

public class HeaderValueProvider : IValueProvider
{
    public HttpRequestHeaders Headers { get; set; }
    public HeaderValueProvider(HttpRequestHeaders headers)
    {
        Headers = headers;
    }

    public bool ContainsPrefix(string prefix)
    {
        return Headers.Any(s => s.Key.StartsWith(prefix));
    }

    public ValueProviderResult GetValue(string key)
    {
        KeyValuePair<string, IEnumerable<string>> header = Headers.FirstOrDefault(s => s.Key.StartsWith(key));
        string headerValue = string.Join(",", header.Value);
        return new ValueProviderResult(headerValue, headerValue, CultureInfo.InvariantCulture);
    }
}

    public class MyController : ApiController
    {
        //public HttpResponseMessage Post([ModelBinder(typeof(UserModelBinder))] User username)
        //public HttpResponseMessage Post([FromBody] String username)
        //public HttpResponseMessage Post([ValueProvider(typeof(HeaderValueFactory))] String username)
        public HttpResponseMessage Post(Post post, [FromUri]User user)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent("aaa");
            return response;
            //...
        }


    }
}