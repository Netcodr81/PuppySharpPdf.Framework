
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PuppySharpPdf.NetFramework.Tests")]
namespace PuppySharpPdf.NetFramework.Common.Interfaces.FileUtils;

internal interface IFileUtils
{
    string RenderCssContentFromFileToString(string pathToCssFile);
}
