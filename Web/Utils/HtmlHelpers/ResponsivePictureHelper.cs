using System;
using System.Linq;
using Core.Extension;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Utils.UrlHelpers;

namespace Web.Utils.Helpers
{
	public static partial class CustomHelpers
	{
		public static IHtmlContent ResponsivePictureRender(this IHtmlHelper helper, string from, string image, object attr = default, object imgAttr = default)
		{
			var picture = new TagBuilder("picture");
			attr?.ToMap().ToList().
				ForEach(kvp => picture.Attributes.Add(kvp.Key, Convert.ToString(kvp.Value)));
			var widths = new[] {1440, 1000, 720, 480, 225};
			foreach (var width in widths)
			{
				var source = new TagBuilder("source");
				source.Attributes.Add("srcset", ContentDelivery.ResizedImageLink(from, image, width));
				source.Attributes.Add("media", $"(min-width: {width}px)");
				picture.InnerHtml.AppendHtml(source);
			}
			var img = new TagBuilder("img");
			img.Attributes.Add("src", ContentDelivery.OriginalImageLink(from, image));
			imgAttr?.ToMap().ToList().
				ForEach(kvp => img.Attributes.Add(kvp.Key, Convert.ToString(kvp.Value)));
			picture.InnerHtml.AppendHtml(img);
			return picture;
		}
	}
}