using PuppySharpPdf.NetFramework.Common.Utils;
using PuppySharpPdf.NetFramework.Tests.Resources;
using Shouldly;

namespace PuppySharpPdf.NetFramework.Tests.Common.Utils;
public class HtmlUtilsTests
{

    [Fact]
    public void FindImgageTagSources_ShouldReturnThreeSources()
    {

        // arrange
        var html = DummyHtmlGenerator.GenerateHtmlStringWithThreeImageTags();

        // act
        var result = HtmlUtils.FindImageTagSources(html);

        // assert
        result.Count().ShouldBe(3);
    }


    [Fact]
    public void RenderImageToBase64_ValidFilePath_ShouldReturnBase64EncodedString()
    {

        // arrange
        var imagePath = "Resources/images/deer.jpg";

        // act

        var result = HtmlUtils.RenderImageToBase64(imagePath);

        // assert

        result.ShouldNotBeEmpty();
        result.ShouldBeOfType<string>();

    }

    [Fact]
    public void RenderImageToBase64_ValidUrlPath_ShouldReturnBase64EncodedString()
    {

        // arrange
        var imagePath = "https://cdn.pixabay.com/photo/2014/09/20/23/44/website-454460_960_720.jpg";

        // act

        var result = HtmlUtils.RenderImageToBase64(imagePath);

        // assert

        result.ShouldNotBeEmpty();
        result.ShouldBeOfType<string>();

    }
}
