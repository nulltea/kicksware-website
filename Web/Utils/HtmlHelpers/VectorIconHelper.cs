using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Environment;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
			return helper.VectorIconRenderFromPath(Path.Combine(Environment.ImagesPath, icon));
		}

		public static IHtmlContent VectorIconRender(this IHtmlHelper helper, string icon, params string[] classes)
		{
			return helper.VectorIconRenderFromPath(Path.Combine(Environment.ImagesPath, icon), classes);
		}
	}
}