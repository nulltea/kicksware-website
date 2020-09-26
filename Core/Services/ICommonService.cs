using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Gateway;
using Core.Model.Parameters;

namespace Core.Services
{
	public interface ICommonService<T> where T : IBaseEntity
	{
		T FetchUnique(string uniqueId, RequestParams requestParams = default);

		List<T> Fetch(RequestParams requestParams = default);

		List<T> Fetch(RequestQuery query, RequestParams requestParams = default);

		int Count(RequestQuery query, RequestParams requestParams = default);

		int Count(object query = default, RequestParams requestParams = default);

		Task<T> FetchUniqueAsync(string uniqueId, RequestParams requestParams = default);

		Task<List<T>> FetchAsync(RequestParams requestParams = default);

		Task<List<T>> FetchAsync(RequestQuery query, RequestParams requestParams = default);

		Task<int> CountAsync(RequestQuery query, RequestParams requestParams = default);

		Task<int> CountAsync(object query = default, RequestParams requestParams = default);
	}
}