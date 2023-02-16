using PuppySharpPdf.NetFramework.Common.Utils;
using Shouldly;
using System.Reflection;
using Xunit;

namespace PuppySharpPdf.NetFramework.Tests.Common.Utils;
public class FileUtilsTests
{
    [Fact]
    public void RenderCssContentFromFile_ValidFilePathProvided_ShouldReturnMatchingCssString()
    {

        // arrange
        var fileUtil = new FileUtils();
        string assemblyPath = Assembly.GetExecutingAssembly().Location;

        // Get the directory path of the assembly
        string directoryPath = Path.GetDirectoryName(assemblyPath);

        // Combine the directory path with the file name of the test file
        string pathToCssFile = Path.Combine(directoryPath, "Resources/css/TestCss.css");


        var expected = @"body
{
    background-color: #ffffff;
    color: #000000;
    font-family: Arial, Helvetica, sans-serif;
    font-size: 12pt;
    margin: 0;
    padding: 0;
}

html {
    margin: 0;
    padding: 0;
}

.page {
    margin: 0;
    padding: 0;
}

.dropdown {
    background-color: blue;
    color: white;
}";

        // act
        var result = fileUtil.RenderCssContentFromFileToString(pathToCssFile);

        // assert
        result.ShouldBe(expected);
    }

    [Fact]
    public void RenderCssContentFromFile_InValidFilePathProvided_ShouldReturnMatchingCssString()
    {

        // arrange
        var fileUtil = new FileUtils();
        string assemblyPath = Assembly.GetExecutingAssembly().Location;

        // Get the directory path of the assembly
        string directoryPath = Path.GetDirectoryName(assemblyPath);

        // Combine the directory path with the file name of the test file
        string pathToCssFile = Path.Combine(directoryPath, "Resources/css/Test.css");


        // assert
        Should.Throw<FileNotFoundException>(() => fileUtil.RenderCssContentFromFileToString(pathToCssFile));
    }
}


