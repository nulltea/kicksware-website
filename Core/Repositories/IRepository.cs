using System.Collections.Generic;
using Core.Entities;
using Core.Gateway;
using Core.Model.Parameters;

namespace Core.Repositories
{
	public interface IRepository<T> where T : IBaseEntity
	{
		T GetUnique(string id, RequestParams requestParams = default);

		List<T> Get(RequestParams requestParams = default);

		List<T> Get(IEnumerable<string> idList, RequestParams requestParams = default);

		List<T> Get(RequestQuery query, RequestParams requestParams = default);

		List<T> Get(object queryObject, RequestParams requestParams = default);

		T Post(T entity, RequestParams requestParams = default);

		bool Update(T entity, RequestParams requestParams = default);

		bool Delete(T entity, RequestParams requestParams = default);

		bool Delete(string uniqueId, RequestParams requestParams = default);

		int Count(RequestQuery query, RequestParams requestParams = default);

		int Count(object queryObject, RequestParams requestParams = default);

		int Count();
	}
}