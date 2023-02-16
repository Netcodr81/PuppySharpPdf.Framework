using PuppySharpPdf.NetFramework.Common.Interfaces.FileUtils;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PuppySharpPdf.NetFramework.Tests")]
namespace PuppySharpPdf.NetFramework.Common.Utils;


internal class FileUtils : IFileUtils
{
    public string RenderCssContentFromFileToString(string pathToCssFile)
    {
        string assemblyPath = Assembly.GetExecutingAssembly().Location;
        string directoryPath = Path.GetDirectoryName(assemblyPath);


        var css = System.IO.File.ReadAllText(pathToCssFile);
        return css;
    }
}
