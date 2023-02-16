using System;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PuppySharpPdf.NetFramework.Common.Utils;
public static class ImageUtils
{
    public static string RenderImgToBase64(this UrlHelper urlHelper, string url)
    {
        string finalUrl = "";

        if (!url.Contains("http"))
        {
            var path = urlHelper.Content(url);
            finalUrl = new Uri(HttpContext.Current.Request.Url, path).AbsoluteUri;

        }
        else
        {
            finalUrl = url;
        }

        using (var handler = new HttpClientHandler())
        {
            using (var client = new HttpClient(handler))
            {
                var bytes = client.GetByteArrayAsync(finalUrl).Result;
                return $"data:image/{Path.GetExtension(finalUrl)};base64," + Convert.ToBase64String(bytes);
            }
        }
    }
}
