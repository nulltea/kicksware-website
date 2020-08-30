using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Services;
using Infrastructure.Gateway.gRPC;
using Proto;
using RequestParams = Core.Gateway.RequestParams;
using SneakerReference = Core.Entities.References.SneakerReference;

namespace Infrastructure.Usecase
{
	public class ReferenceSearchServiceGRPC : IReferenceSearchService
	{
		private SearchReferencesService.SearchReferencesServiceClient _client;

		public ReferenceSearchServiceGRPC(SearchReferencesService.SearchReferencesServiceClient client) =>
			_client = client;

		public List<SneakerReference> Search(string query, RequestParams requestParams = default) =>
			_client.Search(new SearchTag {Tag = query, RequestParams = requestParams?.FromNative()})?.References
				.ToList().ToNative();

		public List<SneakerReference> SearchBy(string field, object query, RequestParams requestParams = default) =>
			_client.SearchBy(new SearchFilter {Field = field, Value = query.ToString(), RequestParams = requestParams?.FromNative()})?.References
				.ToList().ToNative();

		public async Task<List<SneakerReference>> SearchAsync(string query, RequestParams requestParams = default) =>
			(await _client.SearchAsync(new SearchTag {Tag = query, RequestParams = requestParams?.FromNative()}))?.References
				.ToList().ToNative();

		public async Task<List<SneakerReference>> SearchAsyncBy(string field, object query, RequestParams requestParams = default) =>
			(await _client.SearchByAsync(new SearchFilter {Field = field, Value = query.ToString(), RequestParams = requestParams?.FromNative()}))?.References
				.ToList().ToNative();

		public List<SneakerReference> SearchSKU(string skuQuery, RequestParams requestParams = default) =>
			_client.SearchSKU(new SearchFilter {Value = skuQuery, RequestParams = requestParams?.FromNative()})?.References
				.ToList().ToNative();

		public List<SneakerReference> SearchBrand(string brandQuery, RequestParams requestParams = default) =>
			_client.SearchBrand(new SearchFilter {Value = brandQuery, RequestParams = requestParams?.FromNative()})?.References
				.ToList().ToNative();

		public List<SneakerReference> SearchModel(string modelQuery, RequestParams requestParams = default) =>
			_client.SearchModel(new SearchFilter {Value = modelQuery, RequestParams = requestParams?.FromNative()})?.References
				.ToList().ToNative();

		public async Task<List<SneakerReference>> SearchAsyncSKU(string skuQuery, RequestParams requestParams = default) =>
			(await _client.SearchSKUAsync(new SearchFilter {Value = skuQuery, RequestParams = requestParams?.FromNative()}))?.References
				.ToList().ToNative();

		public async Task<List<SneakerReference>> SearchAsyncBrand(string brandQuery, RequestParams requestParams = default) =>
			(await _client.SearchBrandAsync(new SearchFilter {Value = brandQuery, RequestParams = requestParams?.FromNative()}))?.References
			.ToList().ToNative();

		public async Task<List<SneakerReference>> SearchAsyncModel(string modelQuery, RequestParams requestParams = default) =>
			(await _client.SearchModelAsync(new SearchFilter {Value = modelQuery, RequestParams = requestParams?.FromNative()}))?.References
			.ToList().ToNative();
	}
}