using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Extension;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Environment = Core.Environment.Environment;

namespace Web.Utils.Helpers
{
	public static partial class CustomHelpers
	{
		public static IHtmlContent VectorIconRenderFromPath(this IHtmlHelper helper, string iconPath)
		{
			if (!File.Exists(iconPath)) return null;
			var iconNode = HtmlAgilityPack.HtmlNode.CreateNode(File.ReadAllText(iconPath));
			return new HtmlString(iconNode.OuterHtml);
		}

		public static IHtmlContent VectorIconRenderFromPath(this IHtmlHelper helper, string iconPath, params string[] classes)
		{
			if (!File.Exists(iconPath)) return null;
			var iconNode = HtmlAgilityPack.HtmlNode.CreateNode(File.ReadAllText(iconPath));
			classes.ToList().ForEach(iconNode.AddClass);
			return new HtmlString(iconNode.OuterHtml);
		}

		public static IHtmlContent VectorIconRender(this IHtmlHelper helper, string icon)
		{
			return helper.VectorIconRenderFromPath(Path.Combine(Environment.IconsPath, icon));
		}

		public static IHtmlContent VectorIconRender(this IHtmlHelper helper, string icon, params string[] classes)
		{
			return helper.VectorIconRenderFromPath(Path.Combine(Environment.IconsPath, icon), classes);
		}

		public static IHtmlContent VectorLogoRender(this IHtmlHelper helper, string logo, object attrs = default)
		{
			var logoPath = Path.Combine(Environment.LogosPath, logo);
			if (!File.Exists(logoPath)) return null;
			var iconNode = HtmlAgilityPack.HtmlNode.CreateNode(File.ReadAllText(logoPath));
			attrs?.ToMap().ToList().
				ForEach(kvp => iconNode.Attributes.Add(kvp.Key, Convert.ToString(kvp.Value)));
			return new HtmlString(iconNode.OuterHtml);
		}
	}
}