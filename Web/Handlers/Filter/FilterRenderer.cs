using System;
using Core.Extension;
using Core.Model.Parameters;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Handlers.Filter
{
	public static class FilterRenderer
	{
		public static IHtmlContent RenderCheckbox(this FilterParameter param, object attributes = default)
		{
			var content = new TagBuilder("div");
			content.AddCssClass("filter-checkbox");

			var checkbox = new TagBuilder("input");
			checkbox.Attributes["type"] = "checkbox";
			checkbox.Attributes["id"] = param.RenderId;
			checkbox.Attributes["value"] = Convert.ToString(param.Value);
			if (attributes != default) checkbox.MergeAttributes(attributes.ToMap());
			if (param.Checked) checkbox.Attributes["checked"] = "true";
			content.InnerHtml.AppendHtml(checkbox);

			var label = new TagBuilder("label");
			label.Attributes["for"] = param.RenderId;
			label.InnerHtml.Append(param.Caption);
			content.InnerHtml.AppendHtml(label);

			return content;
		}

		public static IHtmlContent RenderInput(this FilterParameter param, object attributes = default)
		{
			var input = new TagBuilder("input");
			input.Attributes["type"] = "text";
			input.Attributes["id"] = param.RenderId;
			input.Attributes["value"] = Convert.ToString(param.Value);
			input.MergeAttributes(attributes.ToMap(), true);
			return input;
		}

		public static IHtmlContent RenderHidden(this FilterParameter param)
		{
			var hidden = new TagBuilder("input");
			hidden.Attributes["type"] = "hidden";
			hidden.Attributes["id"] = param.RenderId;
			hidden.Attributes["value"] = Convert.ToString(param.Value);
			return hidden;
		}
	}
}