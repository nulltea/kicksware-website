using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Handlers.Sort
{
	public static class SortRenderer
	{
		public static List<SelectListItem> FormSortSelect(this ISortedModel model)
		{
			return model.SortParameters?.Select(sort =>
					new SelectListItem(sort.Caption, sort.RenderValue))
				.ToList() ?? new List<SelectListItem>();
		}
	}
}