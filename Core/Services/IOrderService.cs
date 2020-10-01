using System.Threading.Tasks;
using Core.Entities;
using Core.Gateway;

namespace Core.Services
{
	public interface IOrderService : ICommonService<Order>
	{
		#region CRUD Sync

		Order Store(string referenceID, string productID = default);

		bool Modify(Order user, RequestParams requestParams = default);

		bool Remove(string orderID);

		#endregion

		#region CRUD Async

		Task<Order> StoreAsync(string referenceID, string productID = default);

		Task<bool> ModifyAsync(Order user, RequestParams requestParams = default);

		Task<bool> RemoveAsync(string orderID, RequestParams requestParams = default);

		#endregion
	}
}