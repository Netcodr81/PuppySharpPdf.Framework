namespace PuppySharpPdf.NetFramework.Tests.Resources;
public static class DummyHtmlGenerator
{
    public static string GenerateSimpleHtmlString()
    {
        return @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Document</title>
</head>
<body>
    <h1>Hello World</h1>
</body>
</html>";
    }


    public static string GenerateHtmlStringWithThreeImageTags()
    {
        return @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Document</title>
</head>
<body>
    <h1>Hello World</h1>

    <img src=""images/arabian-nights.jpg"" alt=""Girl in a jacket"" width=""500"" height=""600"">
    <img src=""images/deer.jpg"" alt=""Girl in a jacket"" width=""500"" height=""600"">
    <img src=""images/vector-image.jpt"" alt=""Girl in a jacket"" width=""500"" height=""600"">
</body>
</html>";
    }
}
