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
					new SelectListItem(sort.Caption, sort.RenderValue){Selected = sort.RenderValue == model.ChosenSorting.RenderValue})
				.ToList() ?? new List<SelectListItem>();
		}
	}
}