namespace Core.Model.Parameters
{
	public class RequestQuery
	{
		private object _queryData;

		private RequestQuery(object queryData) => _queryData = queryData;
		public void SetQuery(object query) => _queryData = query;

		public T GetQuery<T>() where T: class => _queryData as T;

		public static RequestQuery FromValue(object value) => new RequestQuery(value);
	}
}