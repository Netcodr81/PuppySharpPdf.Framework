﻿using Ardalis.Result;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using PuppySharpPdf.Framework.Common.Mapping;
using PuppySharpPdf.Framework.Renderers.Configurations;
using PuppySharpPdf.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PuppySharpPdf.Framework.Renderers
{
	public class PuppyPdfRenderer
	{
		public RendererOptions RendererOptions { get; }

		public PuppyPdfRenderer()
		{

			RendererOptions = new RendererOptions();
		}
		public PuppyPdfRenderer(Action<RendererOptions> options)
		{

			var rendererOptions = new RendererOptions();
			options?.Invoke(rendererOptions); ;

			RendererOptions = rendererOptions;
		}

		public async Task<Result<byte[]>> GeneratePdfFromUrlAsync(string url)
		{
			if (url is null)
			{
				return Result.Invalid(new List<ValidationError> { new ValidationError { ErrorMessage = "Url can't be empty", ErrorCode = "400" } });
			}

			var urlValidator = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");
			if (!urlValidator.IsMatch(url))
			{
				url = $"https://{url}";
			}

			if (string.IsNullOrEmpty(RendererOptions.ChromeExecutablePath))
			{
				var browserFetcher = new BrowserFetcher();
				await browserFetcher.DownloadAsync();
			}
			await using var browser = await Puppeteer.LaunchAsync(options: PuppyMapper.MapToLaunchOptions(RendererOptions));
			await using var page = await browser.NewPageAsync();

			try
			{
				await page.GoToAsync(url);
				await page.SetJavaScriptEnabledAsync(true);
				await page.WaitForNetworkIdleAsync();

				var result = await page.PdfDataAsync();
				return Result.Success(result);

			}
			catch (Exception ex)
			{
				return Result.Error("An error occurred while generating the pdf");
			}
			finally
			{
				await page.CloseAsync();
			}

		}

		public async Task<Result<byte[]>> GeneratePdfFromUrlAsync(string url, Action<Framework.Renderers.Configurations.PdfOptions> pdfOptions)
		{
			if (url is null)
			{
				return Result.Invalid(new List<ValidationError> { new ValidationError { ErrorMessage = "Url can't be empty", ErrorCode = "400" } });
			}

			var urlValidator = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");
			if (!urlValidator.IsMatch(url))
			{
				url = $"https://{url}";
			}

			var customPdfOptions = new Framework.Renderers.Configurations.PdfOptions();
			pdfOptions?.Invoke(customPdfOptions);

			if (string.IsNullOrEmpty(RendererOptions.ChromeExecutablePath))
			{
				var browserFetcher = new BrowserFetcher();
				await browserFetcher.DownloadAsync();
			}

			await using var browser = await Puppeteer.LaunchAsync(options: PuppyMapper.MapToLaunchOptions(RendererOptions));
			await using var page = await browser.NewPageAsync();

			try
			{

				await page.GoToAsync(url);
				await page.SetJavaScriptEnabledAsync(true);
				await page.WaitForNetworkIdleAsync();

				var result = await page.PdfDataAsync(customPdfOptions.MappedPdfOptions);
				return Result.Success(result);

			}
			catch (Exception ex)
			{
				return Result.Error("An error occurred while generating the pdf");
			}
			finally
			{
				await page.CloseAsync();
			}


		}

		public async Task<Result<byte[]>> GeneratePdfFromUrlAsync(string url, Framework.Renderers.Configurations.PdfOptions pdfOptions)
		{
			if (url is null)
			{
				return Result.Invalid(new List<ValidationError> { new ValidationError { ErrorMessage = "Url can't be empty", ErrorCode = "400" } });
			}

			var urlValidator = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");
			if (!urlValidator.IsMatch(url))
			{
				url = $"https://{url}";
			}

			var optionss = pdfOptions.MappedPdfOptions;

			if (string.IsNullOrEmpty(RendererOptions.ChromeExecutablePath))
			{
				var browserFetcher = new BrowserFetcher();
				await browserFetcher.DownloadAsync();
			}


			await using var browser = await Puppeteer.LaunchAsync(options: PuppyMapper.MapToLaunchOptions(RendererOptions));
			await using var page = await browser.NewPageAsync();

			try
			{
				await page.GoToAsync(url);
				await page.SetJavaScriptEnabledAsync(true);
				await page.WaitForNetworkIdleAsync();
				var result = await page.PdfDataAsync(pdfOptions.MappedPdfOptions);
				return Result.Success(result);
			}
			catch (Exception ex)
			{

				return Result.Error("An error occurred while generating the pdf");
			}
			finally
			{
				await page.CloseAsync();
			}



		}

		public async Task<Result<byte[]>> GeneratePdfFromHtmlAsync(string html)
		{
			if (html is null)
			{
				return Result.Invalid(new List<ValidationError> { new ValidationError { ErrorMessage = "Html string can't be empty", ErrorCode = "400" } });
			}

			if (string.IsNullOrEmpty(RendererOptions.ChromeExecutablePath))
			{
				var browserFetcher = new BrowserFetcher();
				await browserFetcher.DownloadAsync();
			}

			await using var browser = await Puppeteer.LaunchAsync(options: PuppyMapper.MapToLaunchOptions(RendererOptions));

			using var page = await browser.NewPageAsync();
			await page.AddStyleTagAsync(new AddTagOptions { Content = @"#header, #footer { -webkit-print-color-adjust:exact;padding: 0 !important;height: 100% !important;}" });
			await page.EmulateMediaTypeAsync(MediaType.Screen);
			try
			{
				html = HtmlUtils.NormalizeHtmlString(html);

				await page.SetContentAsync(html);

				await page.ImportCssStyles(html);
				await page.SetJavaScriptEnabledAsync(true);
				await page.WaitForNetworkIdleAsync();
				var result = await page.PdfDataAsync(new Framework.Renderers.Configurations.PdfOptions().MappedPdfOptions);
				return Result.Success(result);
			}
			catch (Exception ex)
			{

				return Result.Error("An error occurred while generating the pdf");
			}
			finally
			{
				await page.CloseAsync();
			}

		}

		public async Task<Result<byte[]>> GeneratePdfFromHtmlAsync(string html, Action<Framework.Renderers.Configurations.PdfOptions> options)
		{
			if (html is null)
			{
				return Result.Invalid(new List<ValidationError> { new ValidationError { ErrorMessage = "Html string can't be empty", ErrorCode = "400" } });
			}

			if (string.IsNullOrEmpty(RendererOptions.ChromeExecutablePath))
			{
				var browserFetcher = new BrowserFetcher();
				await browserFetcher.DownloadAsync();
			}

			var pdfOptions = new Framework.Renderers.Configurations.PdfOptions();
			options?.Invoke(pdfOptions);

			await using var browser = await Puppeteer.LaunchAsync(options: PuppyMapper.MapToLaunchOptions(RendererOptions));

			using var page = await browser.NewPageAsync();
			await page.AddStyleTagAsync(new AddTagOptions { Content = @"#header, #footer { -webkit-print-color-adjust:exact;padding: 0 !important;height: 100% !important;}" });
			await page.EmulateMediaTypeAsync(MediaType.Screen);

			try
			{
				html = HtmlUtils.NormalizeHtmlString(html);

				await page.SetContentAsync(html);

				await page.ImportCssStyles(html);
				await page.SetJavaScriptEnabledAsync(true);
				await page.WaitForNetworkIdleAsync();
				var result = await page.PdfDataAsync(pdfOptions.MappedPdfOptions);
				return Result.Success(result);
			}
			catch (Exception ex)
			{

				return Result.Error("An error occurred while generating the pdf");
			}
			finally
			{
				await page.CloseAsync();
			}


		}

		public async Task<Result<byte[]>> GeneratePdfFromHtmlAsync(string html, Framework.Renderers.Configurations.PdfOptions pdfOptions)
		{
			if (html is null)
			{
				return Result.Invalid(new List<ValidationError> { new ValidationError { ErrorMessage = "Html string can't be empty", ErrorCode = "400" } });
			}

			if (string.IsNullOrEmpty(RendererOptions.ChromeExecutablePath))
			{
				var browserFetcher = new BrowserFetcher();
				await browserFetcher.DownloadAsync();
			}


			await using var browser = await Puppeteer.LaunchAsync(options: PuppyMapper.MapToLaunchOptions(RendererOptions));

			using var page = await browser.NewPageAsync();
			await page.AddStyleTagAsync(new AddTagOptions { Content = @"#header, #footer { -webkit-print-color-adjust:exact;padding: 0 !important;height: 100% !important;}" });
			await page.EmulateMediaTypeAsync(MediaType.Screen);

			try
			{
				html = HtmlUtils.NormalizeHtmlString(html);

				await page.SetContentAsync(html);

				await page.ImportCssStyles(html);
				await page.SetJavaScriptEnabledAsync(true);
				await page.WaitForNetworkIdleAsync(new WaitForNetworkIdleOptions() { IdleTime = 1000 });
				var result = await page.PdfDataAsync(pdfOptions.MappedPdfOptions);
				return Result.Success(result);
			}
			catch (Exception ex)
			{

				return Result.Error("An error occurred while generating the pdf");
			}
			finally
			{
				await page.CloseAsync();
			}

		}
	}
}
