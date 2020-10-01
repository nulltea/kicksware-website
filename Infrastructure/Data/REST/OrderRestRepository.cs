using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Gateway;
using Core.Model.Parameters;
using Core.Repositories;
using Infrastructure.Gateway.REST;

namespace Infrastructure.Data
{
	/// <summary>
	/// TODO \ REST, sorry not today...
	/// </summary>
	public class OrderRestRepository : IOrderRepository
	{
		private IGatewayClient<IGatewayRestRequest> _client;

		public OrderRestRepository(IGatewayClient<IGatewayRestRequest> client) => _client = client;

		public Task<Order> GetUniqueAsync(string uniqueId, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public Task<List<Order>> GetAsync(RequestParams requestParams = default) => throw new System.NotImplementedException();

		public Task<List<Order>> GetAsync(IEnumerable<string> idList, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public Task<List<Order>> GetAsync(RequestQuery query, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public Task<List<Order>> GetAsync(object queryObject, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public Task<Order> PostAsync(Order entity, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public Task<bool> UpdateAsync(Order entity, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public Task<bool> DeleteAsync(Order entity, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public Task<bool> DeleteAsync(string uniqueId, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public Task<int> CountAsync(RequestQuery query, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public Task<int> CountAsync(object queryObject, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public Task<int> CountAsync() => throw new System.NotImplementedException();

		public Order GetUnique(string id, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public List<Order> Get(RequestParams requestParams = default) => throw new System.NotImplementedException();

		public List<Order> Get(IEnumerable<string> idList, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public List<Order> Get(RequestQuery query, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public List<Order> Get(object queryObject, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public Order Post(Order entity, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public bool Update(Order entity, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public bool Delete(Order entity, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public bool Delete(string uniqueId, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public int Count(RequestQuery query, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public int Count(object queryObject, RequestParams requestParams = default) => throw new System.NotImplementedException();

		public int Count() => throw new System.NotImplementedException();
	}
}