using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Repositories;
using Infrastructure.Gateway.gRPC;
using Proto;
using RequestParams = Core.Gateway.RequestParams;
using SneakerProduct = Core.Entities.Products.SneakerProduct;

namespace Infrastructure.Data
{
	public class SneakerProductsGrpcRepository : ISneakerProductRepository
	{
		private readonly ProductService.ProductServiceClient _client;

		public SneakerProductsGrpcRepository(ProductService.ProductServiceClient client) => _client = client;

		#region Sync

		public SneakerProduct GetUnique(string productId, RequestParams requestParams = default) =>
			_client.GetProducts(new ProductFilter {ProductID = {productId}, RequestParams = requestParams?.FromNative()})
				?.Products.FirstOrDefault()?.ToNative();

		public List<SneakerProduct> Get(RequestParams requestParams = default) =>
			_client.GetProducts(new ProductFilter {RequestParams = requestParams?.FromNative()})?.Products.ToList()
				.ToNative();

		public List<SneakerProduct> Get(IEnumerable<string> productnames, RequestParams requestParams = default) =>
			_client.GetProducts(new ProductFilter
			{
				ProductID = {productnames}, RequestParams = requestParams?.FromNative()
			})?.Products.ToList().ToNative();

		public List<SneakerProduct> Get(Dictionary<string, object> queryMap, RequestParams requestParams = default) =>
			_client.GetProducts(new ProductFilter
			{
				RequestQuery = queryMap.AsStruct(), RequestParams = requestParams?.FromNative()
			})?.Products.ToList().ToNative();

		public List<SneakerProduct> Get(object queryObject, RequestParams requestParams = default) =>
			_client.GetProducts(new ProductFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			})?.Products.ToList().ToNative();

		public SneakerProduct Post(SneakerProduct product, RequestParams requestParams = default) =>
			_client.AddProducts(new ProductInput
			{
				Products = {product?.FromNative()}, RequestParams = requestParams?.FromNative()
			})?.Products.FirstOrDefault()?.ToNative();

		public List<SneakerProduct> Post(List<SneakerProduct> refs, RequestParams requestParams = default) =>
			_client.AddProducts(new ProductInput
			{
				Products = {refs?.FromNative()}, RequestParams = requestParams?.FromNative()
			})?.Products.ToList().ToNative();

		public bool Update(SneakerProduct product, RequestParams requestParams = default) =>
			_client.EditProducts(new ProductInput
			{
				Products = {product?.FromNative()}, RequestParams = requestParams?.FromNative()
			})?.Count > 0;

		public bool Delete(SneakerProduct product, RequestParams requestParams = default) =>
			_client.DeleteProducts(new ProductFilter
			{
				ProductID = {product.UniqueID}, RequestParams = requestParams?.FromNative()
			})?.Count > 0;

		public bool Delete(string productID, RequestParams requestParams = default) =>
			_client.DeleteProducts(new ProductFilter
			{
				ProductID = {productID}, RequestParams = requestParams?.FromNative()
			})?.Count > 0;

		public int Count(Dictionary<string, object> queryMap, RequestParams requestParams = default) =>
			Convert.ToInt32(_client.CountProducts(new ProductFilter
			{
				RequestQuery = queryMap.AsStruct(), RequestParams = requestParams?.FromNative()
			})?.Count);

		public int Count(object queryObject, RequestParams requestParams = default) =>
			Convert.ToInt32(_client.CountProducts(new ProductFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			})?.Count);

		public int Count() => Convert.ToInt32(_client.CountProducts(new ProductFilter())?.Count);

		#endregion

		#region Async

		public async Task<SneakerProduct> GetUniqueAsync(string productId, RequestParams requestParams = default) =>
			(await _client.GetProductsAsync(new ProductFilter
			{
				ProductID = {productId}, RequestParams = requestParams?.FromNative()
			})).Products.FirstOrDefault()?.ToNative();

		public async Task<List<SneakerProduct>> GetAsync(RequestParams requestParams = default) =>
			(await _client.GetProductsAsync(new ProductFilter {RequestParams = requestParams?.FromNative()})).Products
			.ToList().ToNative();

		public async Task<List<SneakerProduct>>
			GetAsync(IEnumerable<string> productnames, RequestParams requestParams = default) =>
			(await _client.GetProductsAsync(new ProductFilter
			{
				ProductID = {productnames}, RequestParams = requestParams?.FromNative()
			})).Products.ToList().ToNative();

		public async Task<List<SneakerProduct>>
			GetAsync(Dictionary<string, object> queryMap, RequestParams requestParams = default) =>
			(await _client.GetProductsAsync(new ProductFilter
			{
				RequestQuery = queryMap.AsStruct(), RequestParams = requestParams?.FromNative()
			})).Products.ToList().ToNative();

		public async Task<List<SneakerProduct>> GetAsync(object queryObject, RequestParams requestParams = default) =>
			(await _client.GetProductsAsync(new ProductFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			})).Products.ToList().ToNative();

		public async Task<List<SneakerProduct>>
			PostAsync(List<SneakerProduct> products, RequestParams requestParams = default) =>
			(await _client.AddProductsAsync(new ProductInput
			{
				Products = {products?.FromNative()}, RequestParams = requestParams?.FromNative()
			}))?.Products.ToList().ToNative();

		public async Task<SneakerProduct> PostAsync(SneakerProduct product, RequestParams requestParams = default) =>
			(await _client.AddProductsAsync(new ProductInput
			{
				Products = {product?.FromNative()}, RequestParams = requestParams?.FromNative()
			}))?.Products.FirstOrDefault()?.ToNative();

		public async Task<bool> UpdateAsync(SneakerProduct product, RequestParams requestParams = default) =>
			(await _client.EditProductsAsync(new ProductInput
			{
				Products = {product?.FromNative()}, RequestParams = requestParams?.FromNative()
			}))?.Count > 0;

		public async Task<bool> DeleteAsync(SneakerProduct product, RequestParams requestParams = default) =>
			(await _client.DeleteProductsAsync(new ProductFilter
			{
				ProductID = {product.UniqueID}, RequestParams = requestParams?.FromNative()
			}))?.Count > 0;

		public async Task<bool> DeleteAsync(string productID, RequestParams requestParams = default) =>
			(await _client.DeleteProductsAsync(new ProductFilter
			{
				ProductID = {productID}, RequestParams = requestParams?.FromNative()
			}))?.Count > 0;

		public async Task<int> CountAsync(Dictionary<string, object> queryMap, RequestParams requestParams = default) =>
			Convert.ToInt32((await _client.CountProductsAsync(new ProductFilter
			{
				RequestQuery = queryMap.AsStruct(), RequestParams = requestParams?.FromNative()
			}))?.Count);

		public async Task<int> CountAsync(object queryObject, RequestParams requestParams = default) =>
			Convert.ToInt32((await _client.CountProductsAsync(new ProductFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			}))?.Count);

		public async Task<int> CountAsync() =>
			Convert.ToInt32((await _client.CountProductsAsync(new ProductFilter()))?.Count);

		#endregion
	}
}