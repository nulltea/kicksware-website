using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Gateway;
using Core.Model.Parameters;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Usecase
{
	public class OrderService : IOrderService
	{
		private IOrderRepository _repository;

		public OrderService(IOrderRepository repository) => _repository = repository;

		public Order FetchUnique(string uniqueId, RequestParams requestParams = default) =>
			_repository.GetUnique(uniqueId, requestParams);

		public List<Order> Fetch(RequestParams requestParams = default) => _repository.Get(requestParams);

		public List<Order> Fetch(RequestQuery query, RequestParams requestParams = default) =>
			_repository.Get(query, requestParams);

		public Order Store(string referenceID, string productID = default) =>
			_repository.Post(new Order(referenceID, productID));

		public bool Modify(Order user, RequestParams requestParams = default) =>
			_repository.Update(user, requestParams);

		public bool Remove(string orderID) => _repository.Delete(orderID);

		public int Count(RequestQuery query, RequestParams requestParams = default) =>
			_repository.Count(query, requestParams);

		public int Count(object query = default, RequestParams requestParams = default) =>
			_repository.Count(query, requestParams);

		public Task<Order> FetchUniqueAsync(string uniqueId, RequestParams requestParams = default) =>
			_repository.GetUniqueAsync(uniqueId, requestParams);

		public Task<List<Order>> FetchAsync(RequestParams requestParams = default) =>
			_repository.GetAsync(requestParams);

		public Task<List<Order>> FetchAsync(RequestQuery query, RequestParams requestParams = default) =>
			_repository.GetAsync(query, requestParams);

		public Task<Order> StoreAsync(string referenceID, string productID = default) =>
			_repository.PostAsync(new Order(referenceID, productID));

		public Task<bool> ModifyAsync(Order user, RequestParams requestParams = default) =>
			_repository.UpdateAsync(user, requestParams);

		public Task<bool> RemoveAsync(string orderID, RequestParams requestParams = default) =>
			_repository.DeleteAsync(orderID);

		public Task<int> CountAsync(RequestQuery query, RequestParams requestParams = default) =>
			_repository.CountAsync(query, requestParams);

		public Task<int> CountAsync(object query = default, RequestParams requestParams = default) =>
			_repository.CountAsync(query, requestParams);
	}
}