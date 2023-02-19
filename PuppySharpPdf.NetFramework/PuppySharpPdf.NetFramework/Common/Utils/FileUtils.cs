using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PuppySharpPdf.NetFramework.Tests")]
namespace PuppySharpPdf.NetFramework.Common.Utils;


internal class FileUtils
{
    public string RenderCssContentFromFileToString(string pathToCssFile)
    {
        string assemblyPath = Assembly.GetExecutingAssembly().Location;
        string directoryPath = Path.GetDirectoryName(assemblyPath);


        var css = System.IO.File.ReadAllText(Path.Combine(directoryPath, pathToCssFile.Replace("~", string.Empty)));
        return css;
    }
}
