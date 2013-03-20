using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;
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
        public HttpRequestHeaders RequestHeaders { get; set; }
        public HeaderValueProvider(HttpRequestHeaders headers)
        {
            this.RequestHeaders = headers;
        }

        public bool ContainsPrefix(string prefix)
        {
            return RequestHeaders.Where(s => s.Key.StartsWith(prefix)).Count() > 0;
        }

        public ValueProviderResult GetValue(string key)
        {
            KeyValuePair<string, IEnumerable<string>> header = RequestHeaders.Where(s => s.Key.StartsWith(key, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            string headerValue = string.Join(",", header.Value);
            return new ValueProviderResult(headerValue, headerValue, System.Globalization.CultureInfo.InvariantCulture);
        }
    }

    public class MyController : ApiController
    {
        //public HttpResponseMessage Post([ValueProvider(typeof(HeaderValueFactory))] String username)
        public HttpResponseMessage Post([FromBody]Post username)
        {

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(username.ToString());
            return response;
        }
    }
}