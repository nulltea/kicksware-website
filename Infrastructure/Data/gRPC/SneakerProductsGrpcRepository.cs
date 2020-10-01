using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Model.Parameters;
using Core.Repositories;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
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

		public List<SneakerProduct> Get(IEnumerable<string> productNames, RequestParams requestParams = default) =>
			_client.GetProducts(new ProductFilter
			{
				ProductID = {productNames}, RequestParams = requestParams?.FromNative()
			})?.Products.ToList().ToNative();

		public List<SneakerProduct> Get(RequestQuery query, RequestParams requestParams = default) =>
			_client.GetProducts(new ProductFilter
			{
				RequestQuery = query.GetQuery<Struct>(), RequestParams = requestParams?.FromNative()
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

		public int Count(RequestQuery query, RequestParams requestParams = default) =>
			Convert.ToInt32(_client.CountProducts(new ProductFilter
			{
				RequestQuery = query.GetQuery<Struct>(), RequestParams = requestParams?.FromNative()
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
			GetAsync(IEnumerable<string> productNames, RequestParams requestParams = default) =>
			(await _client.GetProductsAsync(new ProductFilter
			{
				ProductID = {productNames}, RequestParams = requestParams?.FromNative()
			})).Products.ToList().ToNative();

		public async Task<List<SneakerProduct>>
			GetAsync(RequestQuery query, RequestParams requestParams = default) =>
			(await _client.GetProductsAsync(new ProductFilter
			{
				RequestQuery = query.GetQuery<Struct>(), RequestParams = requestParams?.FromNative()
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

		public async Task<int> CountAsync(RequestQuery query, RequestParams requestParams = default) =>
			Convert.ToInt32((await _client.CountProductsAsync(new ProductFilter
			{
				RequestQuery = query.GetQuery<Struct>(), RequestParams = requestParams?.FromNative()
			}))?.Count);

		public async Task<int> CountAsync(object queryObject, RequestParams requestParams = default) =>
			Convert.ToInt32((await _client.CountProductsAsync(new ProductFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			}))?.Count);

		public async Task<int> CountAsync() =>
			Convert.ToInt32((await _client.CountProductsAsync(new ProductFilter()))?.Count);

		#endregion

		#region Usecases

		public bool UploadImages(SneakerProduct sneakerProduct, RequestParams requestParams = default) =>
			_client.UploadImages(new UploadImageRequest
			{
				ProductID = sneakerProduct.UniqueID, Images = {sneakerProduct.GetImagesData().FromNative()}
			}) != null;

		public async Task<bool> UploadImagesAsync(SneakerProduct sneakerProduct, RequestParams requestParams = default) =>
			await _client.UploadImagesAsync(new UploadImageRequest
		{
			ProductID = sneakerProduct.UniqueID, Images = {sneakerProduct.GetImagesData().FromNative()}
		}) != null;

		public async Task<decimal> RequestConditionAnalysis(SneakerProduct sneaker) => throw new NotImplementedException();

		public Task<SneakerProduct> RequestSneakerPrediction(List<string> images) => throw new NotImplementedException();

		#endregion

	}
}