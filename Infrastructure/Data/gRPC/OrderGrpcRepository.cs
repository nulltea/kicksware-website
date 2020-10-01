using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Model.Parameters;
using Core.Repositories;
using Google.Protobuf.WellKnownTypes;
using Infrastructure.Gateway.gRPC;
using Proto;
using Order = Core.Entities.Order;
using RequestParams = Core.Gateway.RequestParams;

namespace Infrastructure.Data
{
	public class OrderGrpcRepository : IOrderRepository
	{
		private OrderService.OrderServiceClient _client;

		public OrderGrpcRepository(OrderService.OrderServiceClient client) => _client = client;

				#region Sync

		public Order GetUnique(string orderId, RequestParams requestParams = default) =>
			_client.GetOrders(new OrderFilter {OrderID = {orderId}, RequestParams = requestParams?.FromNative()})
				?.Orders.FirstOrDefault()?.ToNative();

		public List<Order> Get(RequestParams requestParams = default) =>
			_client.GetOrders(new OrderFilter {RequestParams = requestParams?.FromNative()})?.Orders.ToList()
				.ToNative();

		public List<Order> Get(IEnumerable<string> orderNames, RequestParams requestParams = default) =>
			_client.GetOrders(new OrderFilter
			{
				OrderID = {orderNames}, RequestParams = requestParams?.FromNative()
			})?.Orders.ToList().ToNative();

		public List<Order> Get(RequestQuery query, RequestParams requestParams = default) =>
			_client.GetOrders(new OrderFilter
			{
				RequestQuery = query.GetQuery<Struct>(), RequestParams = requestParams?.FromNative()
			})?.Orders.ToList().ToNative();

		public List<Order> Get(object queryObject, RequestParams requestParams = default) =>
			_client.GetOrders(new OrderFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			})?.Orders.ToList().ToNative();

		public Order Post(Order order, RequestParams requestParams = default) =>
			_client.AddOrder(new OrderInput
			{
				ReferenceID = order?.ReferenceID, ProductID = order?.ProductID ?? string.Empty
			})?.Orders.FirstOrDefault()?.ToNative();


		public bool Update(Order order, RequestParams requestParams = default) =>
			_client.EditOrder(new OrderInput
			{
				Order = order?.FromNative()
			})?.Count > 0;

		public bool Delete(Order order, RequestParams requestParams = default) =>
			_client.DeleteOrder(new OrderFilter
			{
				OrderID = {order.UniqueID}, RequestParams = requestParams?.FromNative()
			})?.Count > 0;

		public bool Delete(string orderID, RequestParams requestParams = default) =>
			_client.DeleteOrder(new OrderFilter
			{
				OrderID = {orderID}, RequestParams = requestParams?.FromNative()
			})?.Count > 0;

		public int Count(RequestQuery query, RequestParams requestParams = default) =>
			Convert.ToInt32(_client.CountOrders(new OrderFilter
			{
				RequestQuery = query.GetQuery<Struct>(), RequestParams = requestParams?.FromNative()
			})?.Count);

		public int Count(object queryObject, RequestParams requestParams = default) =>
			Convert.ToInt32(_client.CountOrders(new OrderFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			})?.Count);

		public int Count() => Convert.ToInt32(_client.CountOrders(new OrderFilter())?.Count);

		#endregion

		#region Async

		public async Task<Order> GetUniqueAsync(string orderId, RequestParams requestParams = default) =>
			(await _client.GetOrdersAsync(new OrderFilter
			{
				OrderID = {orderId}, RequestParams = requestParams?.FromNative()
			})).Orders.FirstOrDefault()?.ToNative();

		public async Task<List<Order>> GetAsync(RequestParams requestParams = default) =>
			(await _client.GetOrdersAsync(new OrderFilter {RequestParams = requestParams?.FromNative()})).Orders
			.ToList().ToNative();

		public async Task<List<Order>>
			GetAsync(IEnumerable<string> orderNames, RequestParams requestParams = default) =>
			(await _client.GetOrdersAsync(new OrderFilter
			{
				OrderID = {orderNames}, RequestParams = requestParams?.FromNative()
			})).Orders.ToList().ToNative();

		public async Task<List<Order>> GetAsync(RequestQuery query, RequestParams requestParams = default) =>
			(await _client.GetOrdersAsync(new OrderFilter
			{
				RequestQuery = query.GetQuery<Struct>(), RequestParams = requestParams?.FromNative()
			})).Orders.ToList().ToNative();

		public async Task<List<Order>> GetAsync(object queryObject, RequestParams requestParams = default) =>
			(await _client.GetOrdersAsync(new OrderFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			})).Orders.ToList().ToNative();

		public async Task<Order> PostAsync(Order order, RequestParams requestParams = default) =>
			(await _client.AddOrderAsync(new OrderInput
			{
				ReferenceID = order?.ReferenceID, ProductID = order?.ProductID ?? string.Empty
			}))?.Orders.FirstOrDefault()?.ToNative();

		public async Task<bool> UpdateAsync(Order order, RequestParams requestParams = default) =>
			(await _client.EditOrderAsync(new OrderInput
			{
				Order = order?.FromNative()
			}))?.Count > 0;

		public async Task<bool> DeleteAsync(Order order, RequestParams requestParams = default) =>
			(await _client.DeleteOrderAsync(new OrderFilter
			{
				OrderID = {order.UniqueID}, RequestParams = requestParams?.FromNative()
			}))?.Count > 0;

		public async Task<bool> DeleteAsync(string orderID, RequestParams requestParams = default) =>
			(await _client.DeleteOrderAsync(new OrderFilter
			{
				OrderID = {orderID}, RequestParams = requestParams?.FromNative()
			}))?.Count > 0;

		public async Task<int> CountAsync(RequestQuery query, RequestParams requestParams = default) =>
			Convert.ToInt32((await _client.CountOrdersAsync(new OrderFilter
			{
				RequestQuery = query.GetQuery<Struct>(), RequestParams = requestParams?.FromNative()
			}))?.Count);

		public async Task<int> CountAsync(object queryObject, RequestParams requestParams = default) =>
			Convert.ToInt32((await _client.CountOrdersAsync(new OrderFilter
			{
				RequestQuery = queryObject.AsStruct(), RequestParams = requestParams?.FromNative()
			}))?.Count);

		public async Task<int> CountAsync() =>
			Convert.ToInt32((await _client.CountOrdersAsync(new OrderFilter()))?.Count);

		#endregion
	}
}