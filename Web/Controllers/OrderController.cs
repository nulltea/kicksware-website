using System;
using System.Threading.Tasks;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Entities.Products;
using Core.Entities.References;
using Microsoft.Extensions.Logging;
using Web.Utils.Extensions;

namespace Web.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderService _service;

		private readonly ILogger _logger;

		public OrderController(IOrderService service, ILogger<OrderController> logger)
		{
			_service = service;
			_logger = logger;
		}

		public async Task<IActionResult> CommitOrder(string referenceID, string productID = default)
		{
			Order order = null;
			try
			{
				order = await _service.StoreAsync(referenceID, productID);
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"error occured while {nameof(CommitOrder)} executing");
			}
			return Json(new
			{
				success = true,
				content = await this.RenderViewAsync("_WhyRedirectPartial", true),
				rdirectURL = order?.SourceURL
			});
		}

		public Task<IActionResult> CommitOrderFor(SneakerReference reference)
		{
			if (reference is null) throw new ArgumentException(nameof(reference));
			return CommitOrder(reference.UniqueID);
		}

		public Task<IActionResult> CommitOrderFor(SneakerProduct product)
		{
			if (product is null) throw new ArgumentException(nameof(product));
			return CommitOrder(product.ReferenceID, product.UniqueID);
		}
	}
}