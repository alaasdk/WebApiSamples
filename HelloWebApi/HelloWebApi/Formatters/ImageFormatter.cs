using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HelloWebApi.Models;

namespace HelloWebApi.Formatters
{
    public class ImageFormatter : MediaTypeFormatter
    {
        public ImageFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("image/png")); 
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof(Author);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            return Task.Factory.StartNew(() => WriteToStream(type, value, writeStream, content));
        }

        public void WriteToStream(Type type, object value, Stream stream, HttpContent content)
        {
            Author author = (Author)value;
            Image image = Image.FromFile(@"D:\dev\CodicePlastico\WebApiBook\HelloWebApi\HelloWebApi\Content\Photos\" + author.Name + ".png");
            image.Save(stream, ImageFormat.Png);
            image.Dispose();
        }
    }
}