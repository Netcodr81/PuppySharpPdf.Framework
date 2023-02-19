using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PuppySharpPdf.NetFramework.Common.Utils;
public static class HtmlUtils
{
    public static List<string> FindImageTagSources(string html)
    {
        var mathPattern = @"<img.+?src=[\""'](.+?)[\""'].*?>";

        var matches = Regex.Matches(html, mathPattern, RegexOptions.IgnoreCase)
            .Cast<Match>()
            .Select(x => x.Groups[1].Value)
            .ToList();

        return matches;
    }

    public static string RenderImageToBase64(string imgPath)
    {
        try
        {
            if (Regex.IsMatch(imgPath, @"https?://"))
            {
                using (var handler = new HttpClientHandler())
                {
                    using (var client = new HttpClient(handler))
                    {
                        var bytes = client.GetByteArrayAsync(imgPath).Result;
                        return $"data:image/{Path.GetExtension(imgPath)};base64,{Convert.ToBase64String(bytes)}";
                    }
                }
            }

            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            string directoryPath = Path.GetDirectoryName(assemblyPath);

            var imgFileBytes = File.ReadAllBytes(Path.Combine(directoryPath, imgPath.Replace("~", string.Empty)));
            return $"data:image/{Path.GetExtension(imgPath).Substring(1)};base64,{Convert.ToBase64String(imgFileBytes)}";
        }
        catch (Exception)
        {

            return string.Empty;
        }



    }
}
