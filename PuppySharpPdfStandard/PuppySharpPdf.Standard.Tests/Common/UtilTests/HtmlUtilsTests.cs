using PuppySharpPdf.Standard.Tests.Resources;
using PuppySharpPdf.Standard.Utils;
using Shouldly;

namespace PuppySharpPdf.Standard.Tests.Common.UtilTests;
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

    [Fact]
    public void NormalizeHtmlString_ConvertsAllImageTagsToBase64()
    {

        // arrange
        var html = DummyHtmlGenerator.GenerateHtmlStringWithThreeImageTags();
        var expected = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Resources\\html\\NormalizedImageTags.html");

        // act
        var results = HtmlUtils.NormalizeHtmlString(html);

        // assert
        results.ShouldBe(expected);
    }

    [Fact]
    public void FindCssTagSources_ShouldFindThreeTagSources()
    {

        // arrange
        var html = DummyHtmlGenerator.GenerateHtmlStringWithThreeCssTags();

        // act
        var results = HtmlUtils.FindCssTagSources(html);

        // assert
        results.Count().ShouldBe(3);


    }

}
