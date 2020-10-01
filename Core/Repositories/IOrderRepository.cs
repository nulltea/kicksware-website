using Core.Entities;

namespace Core.Repositories
{
	public interface IOrderRepository : IAsyncRepository<Order>, IRepository<Order> { }
}