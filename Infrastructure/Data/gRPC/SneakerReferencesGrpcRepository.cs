using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Gateway;
using Core.Repositories;
using Infrastructure.Gateway.gRPC;
using Infrastructure.Gateway.REST;
using Infrastructure.Gateway.REST.References.Sneakers;
using Core.Entities.References;
using Core.Model.Parameters;
using Google.Protobuf.WellKnownTypes;
using Proto;
using RequestParams = Core.Gateway.RequestParams;
using SneakerReference = Core.Entities.References.SneakerReference;

namespace Infrastructure.Data
{
	public class SneakerReferencesGrpcRepository : ISneakerReferenceRepository
	{
		private readonly ReferenceService.ReferenceServiceClient _client;

		public SneakerReferencesGrpcRepository(ReferenceService.ReferenceServiceClient client) => _client = client;

		#region Sync

		public SneakerReference GetUnique(string referenceId, RequestParams requestParams = default) =>
			_client.GetReferences(new ReferenceFilter
			{
				ReferenceID = {referenceId}, RequestParams = requestParams?.FromNative()
			})?.References.FirstOrDefault()?.ToNative();

		public List<SneakerReference> Get(RequestParams requestParams = default) =>
			_client.GetReferences(new ReferenceFilter {RequestParams = requestParams?.FromNative()})?.References.ToList()
				.ToNative();

		public List<SneakerReference> Get(IEnumerable<string> referenceNames, RequestParams requestParams = default) =>
			_client.GetReferences(new ReferenceFilter
			{
				ReferenceID = {referenceNames}, RequestParams = requestParams?.FromNative()
			})?.References.ToList().ToNative();

		public List<SneakerReference> Get(RequestQuery query, RequestParams requestParams = default) =>
			_client.GetReferences(new ReferenceFilter
			{
				RequestQuery = query.GetQuery<Struct>(), RequestParams = requestParams?.FromNative()
			})?.References.ToList().ToNative();

		public List<SneakerReference> Get(object queryObject, RequestParams requestParams = default) =>
			_client.GetReferences(new ReferenceFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			})?.References.ToList().ToNative();

		public SneakerReference Post(SneakerReference reference, RequestParams requestParams = default) =>
			_client.AddReferences(new ReferenceInput
			{
				References = {reference?.FromNative()}, RequestParams = requestParams?.FromNative()
			})?.References.FirstOrDefault()?.ToNative();

		public List<SneakerReference> Post(List<SneakerReference> refs, RequestParams requestParams = default) =>
			_client.AddReferences(new ReferenceInput
			{
				References = {refs?.FromNative()}, RequestParams = requestParams?.FromNative()
			})?.References.ToList().ToNative();

		public bool Update(SneakerReference reference, RequestParams requestParams = default) =>
			_client.EditReferences(new ReferenceInput
			{
				References = {reference?.FromNative()}, RequestParams = requestParams?.FromNative()
			})?.Count > 0;

		public bool Delete(SneakerReference reference, RequestParams requestParams = default) =>
			_client.DeleteReferences(new ReferenceFilter
			{
				ReferenceID = {reference.UniqueID}, RequestParams = requestParams?.FromNative()
			})?.Count > 0;

		public bool Delete(string referenceID, RequestParams requestParams = default) =>
			_client.DeleteReferences(new ReferenceFilter
			{
				ReferenceID = {referenceID}, RequestParams = requestParams?.FromNative()
			})?.Count > 0;

		public int Count(RequestQuery query, RequestParams requestParams = default) =>
			Convert.ToInt32(_client.CountReferences(new ReferenceFilter
			{
				RequestQuery = query.GetQuery<Struct>(), RequestParams = requestParams?.FromNative()
			})?.Count);

		public int Count(object queryObject, RequestParams requestParams = default) =>
			Convert.ToInt32(_client.CountReferences(new ReferenceFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			})?.Count);

		public int Count() => Convert.ToInt32(_client.CountReferences(new ReferenceFilter())?.Count);

		#endregion

		#region Async

		public async Task<SneakerReference> GetUniqueAsync(string referenceId, RequestParams requestParams = default) =>
			(await _client.GetReferencesAsync(new ReferenceFilter
			{
				ReferenceID = {referenceId}, RequestParams = requestParams?.FromNative()
			})).References.FirstOrDefault()?.ToNative();

		public async Task<List<SneakerReference>> GetAsync(RequestParams requestParams = default) =>
			(await _client.GetReferencesAsync(new ReferenceFilter {RequestParams = requestParams?.FromNative()}))
			.References.ToList().ToNative();

		public async Task<List<SneakerReference>>
			GetAsync(IEnumerable<string> referenceNames, RequestParams requestParams = default) =>
			(await _client.GetReferencesAsync(new ReferenceFilter
			{
				ReferenceID = {referenceNames}, RequestParams = requestParams?.FromNative()
			})).References.ToList().ToNative();

		public async Task<List<SneakerReference>>
			GetAsync(RequestQuery query, RequestParams requestParams = default) =>
			(await _client.GetReferencesAsync(new ReferenceFilter
			{
				RequestQuery = query.GetQuery<Struct>(), RequestParams = requestParams?.FromNative()
			})).References.ToList().ToNative();

		public async Task<List<SneakerReference>> GetAsync(object queryObject, RequestParams requestParams = default) =>
			(await _client.GetReferencesAsync(new ReferenceFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			})).References.ToList().ToNative();

		public async Task<List<SneakerReference>>
			PostAsync(List<SneakerReference> references, RequestParams requestParams = default) =>
			(await _client.AddReferencesAsync(new ReferenceInput
			{
				References = {references?.FromNative()}, RequestParams = requestParams?.FromNative()
			}))?.References.ToList().ToNative();

		public async Task<SneakerReference>
			PostAsync(SneakerReference reference, RequestParams requestParams = default) =>
			(await _client.AddReferencesAsync(new ReferenceInput
			{
				References = {reference?.FromNative()}, RequestParams = requestParams?.FromNative()
			}))?.References.FirstOrDefault()?.ToNative();

		public async Task<bool> UpdateAsync(SneakerReference reference, RequestParams requestParams = default) =>
			(await _client.EditReferencesAsync(new ReferenceInput
			{
				References = {reference?.FromNative()}, RequestParams = requestParams?.FromNative()
			}))?.Count > 0;

		public async Task<bool> DeleteAsync(SneakerReference reference, RequestParams requestParams = default) =>
			(await _client.DeleteReferencesAsync(new ReferenceFilter
			{
				ReferenceID = {reference.UniqueID}, RequestParams = requestParams?.FromNative()
			}))?.Count > 0;

		public async Task<bool> DeleteAsync(string referenceID, RequestParams requestParams = default) =>
			(await _client.DeleteReferencesAsync(new ReferenceFilter
			{
				ReferenceID = {referenceID}, RequestParams = requestParams?.FromNative()
			}))?.Count > 0;

		public async Task<int> CountAsync(RequestQuery query, RequestParams requestParams = default) =>
			Convert.ToInt32((await _client.CountReferencesAsync(new ReferenceFilter
			{
				RequestQuery = query.GetQuery<Struct>(), RequestParams = requestParams?.FromNative()
			}))?.Count);

		public async Task<int> CountAsync(object queryObject, RequestParams requestParams = default) =>
			Convert.ToInt32((await _client.CountReferencesAsync(new ReferenceFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			}))?.Count);

		public async Task<int> CountAsync() =>
			Convert.ToInt32((await _client.CountReferencesAsync(new ReferenceFilter()))?.Count);

		#endregion
	}
}