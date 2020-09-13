using System.Collections.Generic;
using System.Linq;
using Core.Extension;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Utils.Helpers;

namespace Web.Handlers.Menu
{
	/// <li class="expandable" id="shop-menu">
	/// 	<a asp-controller="Shop" asp-action="References">SHOP</a>
	/// 	<div class="sub-menu">
	/// 		<ul>
	/// 			...
	/// 		</ul>
	/// 	</div>
	/// </li>
	public class MobileShopMenuContent : MenuListContent<MobileBrandSubmenuContent>
	{
		public override IHtmlContent Render(IHtmlHelper html)
		{
			FillMissingAttributes();

			var subPanel = new TagBuilder("div");
			subPanel.AddCssClass("sub-menu");

			var brandsMenu = new TagBuilder("ul");
			brandsMenu.AddCssClass("slider-menu-nav");

			foreach (var brand in InnerContent)
			{
				brandsMenu.InnerHtml.AppendHtml(brand.Render(html));
			}
			subPanel.InnerHtml.AppendHtml(RenderSubmenuHeader(html));
			subPanel.InnerHtml.AppendHtml(brandsMenu);

			return subPanel;
		}

		private IHtmlContent RenderSubmenuHeader(IHtmlHelper html)
		{
			var header = new TagBuilder("div");
			header.AddCssClass("submenu-header");

			var button = new TagBuilder("button");
			button.AddCssClass("go_back-button");
			var span = new TagBuilder("span");
			span.InnerHtml.Append("BACK TO MENU");
			button.InnerHtml.AppendLine(span);

			var link = html.ActionLink($"SHOW ALL KICKS", Action, Controller, new {brandID = RouteValues});

			header.InnerHtml.AppendHtml(button);
			header.InnerHtml.AppendHtml(link);
			return header;
		}
	}

	/// <li class="expandable" id="brand-models">
	/// 	<a asp-controller="Action" asp-action="Contoller">Caption</a>
	/// 	<div class="sub-menu">
	/// 		...
	/// 	</div>
	/// </li>
	public class MobileBrandSubmenuContent : MenuListContent<MobileBrandSubgroupContent>
	{
		public override IHtmlContent Render(IHtmlHelper html)
		{
			FillMissingAttributes();

			var expandableItem = new TagBuilder("li");
			if (InnerContent?.Any() ?? false) expandableItem.AddCssClass("expandable");
			expandableItem.AddCssClass("slider-menu-item");
			expandableItem.Attributes["id"] = $"{Caption.ToLower()}-models";

			var link = html.ActionLink(Caption.ToUpper(), Action, Controller, new {brandID = RouteValues});
			expandableItem.InnerHtml.AppendHtml(link);

			if (InnerContent is null) return expandableItem;

			var subPanel = new TagBuilder("div");
			subPanel.AddCssClass("sub-menu");

			var submodelsMenu = new TagBuilder("ul");
			submodelsMenu.AddCssClass("slider-menu-nav");

			if (InnerContent.Count == 1)
			{
				InnerContent.First().FillMissingAttributes();
				foreach (var model in InnerContent.First().InnerContent)
				{
					submodelsMenu.InnerHtml.AppendHtml(model.Render(html));
				}
			}
			else
			{
				var innerContent = new List<MobileBrandSubgroupContent>();
				for (var i = InnerContent.Count - 1; i >= 0; i--)
				{
					if (string.IsNullOrEmpty(InnerContent[i].Caption.Trim()))
					{
						InnerContent[i - 1].InnerContent.AddRange(InnerContent[i].InnerContent);
						continue;
					}
					innerContent.Add(InnerContent[i]);
				}

				innerContent.Reverse();
				foreach (var group in innerContent)
				{
					submodelsMenu.InnerHtml.AppendHtml(group.Render(html));
				}
			}

			subPanel.InnerHtml.AppendHtml(RenderSubmenuHeader(html));
			subPanel.InnerHtml.AppendHtml(submodelsMenu);
			expandableItem.InnerHtml.AppendHtml(subPanel);

			return expandableItem;
		}

		public override void FillMissingAttributes()
		{
			if (string.IsNullOrEmpty(RouteValues) && !string.IsNullOrEmpty(Caption)) RouteValues = Caption.ToFormattedID(" "); // TODO
			base.FillMissingAttributes();
		}

		private IHtmlContent RenderSubmenuHeader(IHtmlHelper html)
		{
			var header = new TagBuilder("div");
			header.AddCssClass("submenu-header");

			var button = new TagBuilder("button");
			button.AddCssClass("go_back-button");
			var span = new TagBuilder("span");
			span.InnerHtml.Append("BACK TO MENU");
			button.InnerHtml.AppendLine(span);

			var link = html.ActionLink($"SHOW ALL FROM {Caption.ToUpper()}", Action, Controller, new {brandID = RouteValues});

			header.InnerHtml.AppendHtml(button);
			header.InnerHtml.AppendHtml(link);
			return header;
		}
	}

	/// <div class="sub-group">
	/// 	<h3>Caption</h3>
	/// 	...
	/// </div>
	public class MobileBrandSubgroupContent : MenuListContent<MobileModelSubmenuContent>
	{
		public override IHtmlContent Render(IHtmlHelper html)
		{
			FillMissingAttributes();

			var expandableItem = new TagBuilder("li");
			if (InnerContent?.Any() ?? false) expandableItem.AddCssClass("expandable");
			expandableItem.AddCssClass("slider-menu-item");
			expandableItem.Attributes["id"] = $"{Caption.ToLower().Replace(" ", "_")}-submodels";

			var link = html.ActionLink(Caption.ToUpper(), Action, Controller, new {brandID = RouteValues});


			var subGroup = new TagBuilder("div");
			subGroup.AddCssClass("sub-group");

			subGroup.InnerHtml.AppendHtml(RenderSubmenuHeader(html));

			foreach (var model in InnerContent)
			{
				subGroup.InnerHtml.AppendHtml(model.Render(html));
			}

			expandableItem.InnerHtml.AppendHtml(link);
			expandableItem.InnerHtml.AppendHtml(subGroup);
			return expandableItem;
		}

		public override void FillMissingAttributes()
		{
			foreach (var sub in InnerContent)
			{
				if (string.IsNullOrEmpty(sub.Controller)) sub.Controller = Controller;
				if (string.IsNullOrEmpty(sub.Action)) sub.Action = Action;
				sub.ParentContent = ParentContent ?? this;
			}
		}

		private IHtmlContent RenderSubmenuHeader(IHtmlHelper html)
		{
			var header = new TagBuilder("div");
			header.AddCssClass("submenu-header");

			var button = new TagBuilder("button");
			button.AddCssClass("go_back-button");
			var span = new TagBuilder("span");
			span.InnerHtml.Append("BACK TO MENU");
			button.InnerHtml.AppendLine(span);

			var link = html.ActionLink($"SHOW ALL {Caption.ToUpper()}", Action, Controller, new {brandID = RouteValues});

			header.InnerHtml.AppendHtml(button);
			header.InnerHtml.AppendHtml(link);
			return header;
		}
	}

	/// <div class="sub-item">
	/// 	<a asp-controller="controller" asp-action="action">Caption</a>
	/// </div>
	public class MobileModelSubmenuContent : MenuContent
	{

		public override IHtmlContent Render(IHtmlHelper html)
		{
			FillMissingAttributes();

			var subItem = new TagBuilder("div");
			subItem.AddCssClass("sub-item");
			subItem.AddCssClass("slider-menu-item");

			object routeValues = new {modelID = RouteValues};

			if (Action == "Brand")
			{
				routeValues = new {brandID = RouteValues};
			}

			var link = html.ActionLink(Caption, Action, Controller, routeValues);
			subItem.InnerHtml.AppendHtml(link);

			return subItem;
		}

		public override void FillMissingAttributes()
		{
			if (string.IsNullOrEmpty(RouteValues) && !string.IsNullOrEmpty(Caption))
			{
				if (!string.IsNullOrEmpty(ParentContent?.RouteValues) && !new[]{"More"}.Contains(ParentContent?.RouteValues))
				{
					RouteValues = string.Join("_", ParentContent?.RouteValues, Caption.ToFormattedID());
					return;
				}

				RouteValues = Caption.ToFormattedID(Action == "Brand" ? " " : "-");
			}
		}
	}
}