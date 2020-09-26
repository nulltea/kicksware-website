using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Products;
using Core.Gateway;

namespace Core.Repositories
{
	public interface ISneakerProductRepository : IAsyncRepository<SneakerProduct>, IRepository<SneakerProduct>
	{
		bool UploadImages(SneakerProduct sneakerProduct, RequestParams requestParams = default);

		Task<bool> UploadImagesAsync(SneakerProduct sneakerProduct, RequestParams requestParams = default);

		Task<decimal> RequestConditionAnalysis(SneakerProduct sneaker);

		Task<SneakerProduct> RequestSneakerPrediction(List<string> images);
	}
}